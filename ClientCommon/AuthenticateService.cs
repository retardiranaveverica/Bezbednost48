using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommon
{
    public class AuthenticateService : ILog
    {
        public List<User> ulogovaniKorisnici = new List<User>();

        public void LogIn(string username, string password)
        {
            if(UserNameExist(username) && UserPassExist(password))
            {
                ulogovaniKorisnici.Add(new User(username, password));
                Console.WriteLine("Uspesno ste ulogovani");
            } else
            {
                if(!UserNameExist(username))
                    Console.WriteLine("Ne postoji korisnik!");
                else if(UserNameExist(username) && !UserPassExist(password))
                    Console.WriteLine("Neispravna lozinka!");
            }
        }

        public void LogOut(string username)
        {
            throw new NotImplementedException();
        }

        #region trazenje username-a
        private bool UserNameExist(string username)
        {
            //Snezana putanja
            string path = @"C:\Users\a\Desktop\Bezbednost48";
            //Maja putanja
            //string path = @"";
            
            using (StreamReader streamReader = new StreamReader(path))
            {
                string text = streamReader.ReadLine();
                string[] tekst = text.Split(' ');
                string userName = tekst[0];

                if (username == userName)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region trazenje lozinke
        private bool UserPassExist(string password)
        {
            //Snezana putanja
            string path = @"C:\Users\a\Desktop\Bezbednost48";
            //Maja putanja
            //string path = @"";

            using (StreamReader streamReader = new StreamReader(path))
            {
                string text = streamReader.ReadLine();
                string[] tekst = text.Split(' ');
                string userPass = tekst[1];

                if (password == userPass)
                    return true;
                else
                    return false;
            }
        }
        #endregion
    }
}
