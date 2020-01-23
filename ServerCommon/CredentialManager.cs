using ClientCommon;
using Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommon
{
    public class CredentialManager : IAccounts
    {

        List<User> dataBaseUser;
        string path = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika.txt";
        //string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";

        #region Create Acoount
        public void CreateAccount()
        {
            //ReadFromFile();
            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string password = Console.ReadLine();
            User user = new User(username, password/*, false, false, 0*/);

            if (IsUserExist(user) != 0)
            {
                Console.WriteLine("Postoji vec");
            }
            else
            {
                WriteToFile(user);

                dataBaseUser.Add(user);

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
                    Console.WriteLine("Account Created Successfully");
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
            //ReadFromFile();

            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string password = Console.ReadLine();
            User user = new User(username, password);

            if (IsUserExist(user) != 0)
            {
                try
                {
                    int io = IsUserExist(user);
                    dataBaseUser.RemoveAt(io - 1);
                    WriteList();

                    DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
                    DirectoryEntry userE = localMachine.Children.Find(username, "User");
                    localMachine.Children.Remove(userE);
                    userE.Close();
                    localMachine.Close();
                }
                catch(Exception e)
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

        public void DisableAccount()
        {
            throw new NotImplementedException();
        }

        public void EnableAccount()
        {
            throw new NotImplementedException();
        }

        public void LockAccount()
        {
            throw new NotImplementedException();
        }

        //kriptovati lozinku pre upisa u fajl
        public void WriteToFile(User user)
        {
            using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append)))
            {
                    sw.WriteLine(user.Username + " " + user.Password);
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
        /*
        public bool IsUserExist(User user)
        {
            foreach (User u in dataBaseUser)
            {
                if (u.Username == user.Username)
                   return true;
            }
            return false;
        }
        */
        public int IsUserExist(User user)
        {
            int i = 0;
            int j = 0; ;
            //bool retVal=false;
            foreach (User u in dataBaseUser)
            {
                i++;
                if (u.Username == user.Username)
                {
                    j = i;
                    //retVal = true;
                }
            }
            // return retVal;
            return j;
        }

    }
}
