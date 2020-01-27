using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientCommon
{   //samo ideja: ako je neko ulogovan, ne moze opet da se loguje(isto i za izlogovane)
    public class AuthenticateService : ILog
    {
        public List<User> ulogovaniKorisnici = new List<User>();
        CredentialsStore credentialsStore = new CredentialsStore();
        // string path = @"C:\Users\a\Desktop\Bezbednost - Projekat\Bezbednost48\BazaKorisnika.txt";

        #region LogIn
        public void LogIn(/*string username, string password*/)
        {
            Console.WriteLine("Unesite username:");
            string name = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string pass = Console.ReadLine();

            
            if (credentialsStore.UserNameExist(name) && credentialsStore.UserPassExist(pass))
            {
                if (!credentialsStore.isLogged(name))
                {
                    ulogovaniKorisnici.Add(new User(name, pass));
                    Console.WriteLine("Uspesno ste ulogovani");
                } else
                    Console.WriteLine("Korisnik je vec ulogovan!");
                  
                
            } else
            {
                if (!credentialsStore.UserNameExist(name))
                    Console.WriteLine("Ne postoji korisnik!");
                else if (credentialsStore.UserNameExist(name) && !credentialsStore.UserPassExist(pass))
                {
                    Console.WriteLine("Neispravna lozinka!");

                }
            }
        }
        #endregion

        #region LogOut
        public void LogOut(/*string username*/)
        {
            /*string name = WindowsIdentity.GetCurrent().Name;
            Console.WriteLine(name);
            string get_pass = GetPassword(name);*/

            Console.WriteLine("Unesite username:");
            string name = Console.ReadLine();

            foreach(var user in ulogovaniKorisnici)
            {
                if(user.Username == name)
                {
                    string pass = user.Password;
                    ulogovaniKorisnici.Remove(user);
                    Console.WriteLine("Uspesno ste se izlogovali!");
                    break;
                }
            }

        }
        #endregion

        // prebaceno u CredentialStore
       /* #region Looking for username
        private bool UserNameExist(string username)
        {
            //Snezana putanja
            //string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";
            //Maja putanja
            string path = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika.txt";

            using (StreamReader streamReader = new StreamReader(path))
            {

                while (!streamReader.EndOfStream) {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');
                    string userName = tekst[0];

                    if (username == userName)
                        return true;
                }

                return false;
            }
        }
        #endregion

        #region Looking for password
        private bool UserPassExist(string password)
        {
            //Snezana putanja
            //string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";
            //Maja putanja
            string path = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika.txt";

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream) {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');
                    string userPass = tekst[1];

                    if (password == userPass)
                        return true;
                }

                return false;
            }
        }
        #endregion

        #region Get pass for user

        private string GetPassword(string username)
        {
            string pass = "";
            //string path = @"C:\Users\a\Desktop\Bezbednost48\BazaKorisnika.txt";
            string path = @"C:\Users\acer\source\repos\retardiranaveverica\Bezbednost48\BazaKorisnika.txt";

            using (StreamReader streamReader  = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');
                    string name = tekst[0];
                    string password = tekst[1];

                    if(username == name)
                    {
                        pass = password;
                    }
                }
            }

            return pass;
        }

        #endregion

        #region CheckIsLogged

        private bool isLogged(string username)
        {
            foreach(User user in ulogovaniKorisnici)
            
                if (user.Username == username)
                    return true;

            return false;
            
        }

        #endregion
    */
    }
}
