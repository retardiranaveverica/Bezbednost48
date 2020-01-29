using ClientCommon;
using Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.Net;

namespace ServerCommon
{
    public class CredentialManager : IAccounts
    {

        List<User> dataBaseUser = new List<User>();
        List<string> dataBaseUser1 = new List<string>();
        string path = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika.txt";
        //string path1 = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika1.txt";
        //string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";
        Cryptograpy cryptograpy = new Cryptograpy();

        #region Create Acoount
        public void CreateAccount()
        {
            //   updateList();
            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();

            if (IsUserExist(username) != 0)
            {
                Console.WriteLine("Korisnik sa tim username vec postoji.");
            }
            else
            {

                Console.WriteLine("Unesite lozinku:");
                string password = Console.ReadLine();
                User user = new User(username, password/*, false, false, 0*/);

                try
                {
                    DirectoryEntry AD = new DirectoryEntry("WinNT://" +
                                        Environment.MachineName + ",computer");
                    DirectoryEntry NewUser = AD.Children.Add(username, "user");
                    NewUser.Invoke("SetPassword", new object[] { password });
                    NewUser.Invoke("Put", new object[] { "Description", "Test User from .NET" });
                    NewUser.CommitChanges();
                    DirectoryEntry grp;

                    grp = AD.Children.Find("User");
                    if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }

                    WriteToFile(user);

                    dataBaseUser.Add(user);

                    Console.WriteLine("Korisnik {0} je kreiran.", username);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();

                }
            }
        }
        #endregion

        # region Delete Account
        public void DeleteAccount()
        {
            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();

            if (IsUserExist(username) != 0)
            {
                Console.WriteLine("Unesite lozinku:");
                string password = Console.ReadLine();
                User user = new User(username, password);

                try
                {
                    int io = IsUserExist(username);
                    dataBaseUser.RemoveAt(io - 1);
                    WriteList();

                    DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    DirectoryEntry userE = localMachine.Children.Find(username, "User");
                    localMachine.Children.Remove(userE);
                    userE.Close();
                    localMachine.Close();
                    Console.WriteLine("Korisnik {0} je obrisan.", username);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: {0}", e.Message);
                }
            }
            else
            {
                Console.WriteLine("Korisnik ne postoji.");
            }

        }
        # endregion

        public void DisableAccount(string username)
        {
            User user = dataBaseUser[(IsUserExist(username))];
            user.AccountDisabled = true;
        }

        public void EnableAccount(string username)
        {
            User user = dataBaseUser[(IsUserExist(username))];
            user.AccountDisabled = false;
        }

        public void LockAccount(string username)
        {
            User user = dataBaseUser[(IsUserExist(username))];
            user.AccountLock = true;
        }

        //kriptovati lozinku pre upisa u fajl
        public void WriteToFile(User user)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append)))
            {
                sw.WriteLine(user.Username + " " + cryptograpy.EncryptString(user.Password));
            }
        }

        public void WriteList()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {

                foreach (User user in dataBaseUser)
                {
                    sw.WriteLine(user.Username + " " + user.Password);
                }

            }
        }

   /*     public void WriteList1()
        {
            using (StreamWriter sw = new StreamWriter(path1))
            {

                foreach (string s in dataBaseUser1)
                {
                    sw.WriteLine(s);
                }

            }
        }*/

        public void ReadFromFile()
        {
            dataBaseUser = new List<User>();
            using (StreamReader sr = new StreamReader(path))
            {
                var lines = System.IO.File.ReadAllLines(path);
                foreach (string item in lines)
                {
                    var values = item.Split(' ');
                    User user = new User();
                    user.Username = values[0];
                    user.Password = values[1];
                    dataBaseUser.Add(user);
                }

            }
        }

        public int IsUserExist(string username)
        {
            int i = 0;
            int indexElementa = 0;
            foreach (User u in dataBaseUser)
            {
                i++;
                if (u.Username == username)
                {
                    indexElementa = i;
                }
            }

            return indexElementa;
        }

        public bool Verife(string username, string password)
        {
            bool valid = false;

            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                valid = context.ValidateCredentials(username, password);
            }

            using (WindowsIdentity.GetCurrent())
            {

            }

            return valid;
        }



        public void updateList()
        {
            var path = string.Format("WinNT://{0},computer", Environment.MachineName);
            using (var computerEntry = new DirectoryEntry(path))
            {
                foreach (DirectoryEntry childEntry in computerEntry.Children)
                    if (childEntry.SchemaClassName == "User")
                    {
                        dataBaseUser1.Add(childEntry.Name);

                        // string s = childEntry.InvokeGet("password").ToString();
                        //Console.WriteLine("{0}", s);
                    }
            }
        }

    }
}
