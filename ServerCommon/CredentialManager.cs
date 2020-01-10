using ClientCommon;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerCommon
{
    public class CredentialManager : IAccounts
    {

        List<User> dataBaseUser = new List<User>();
        string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";
        

        #region
        public void CreateAccount()
        {
            //ReadFromFile();
            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string password = Console.ReadLine();
            User user = new User(username, password/*, false, false, 0*/);

            if (IsUserExist(user))
            {
                Console.WriteLine("Postoji vec");
            }
            else
            {
                    WriteToFile(user);
                    
                    dataBaseUser.Add(user);
                
            }
        }
#endregion


        public void DeleteAccount()
        {
            //ReadFromFile();

            Console.WriteLine("Unesite korisničko ime:");
            string username = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string password = Console.ReadLine();
            User user = new User(username, password);

            if (IsUserExist(user))
            {
                dataBaseUser.Remove(user);
                WriteList();
            }
            else
            {
                Console.WriteLine("Korisnik ne postoji.");
            }

        }

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

        public bool IsUserExist(User user)
        {
            foreach (User u in dataBaseUser)
            {
                if (u.Username == user.Username)
                   return true;
            }
            return false;
        }


    }
}
