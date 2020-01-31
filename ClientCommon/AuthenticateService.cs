using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace ClientCommon
{   
    public class AuthenticateService : ILog
    {
        public List<User> ulogovaniKorisnici = new List<User>();
        CredentialsStore credentialsStore = new CredentialsStore();

        string path = @"C:\Users\a\Desktop\Bezbednost - Projekat\Bezbednost48\BazaKorisnika.txt";
        int numLog = 0;
        
        #region LogIn
        public void LogIn(/*string username, string password*/)
        {
            X509Certificate2 clientCertificate = null;

            Console.WriteLine("Unesite username:");
            string name = Console.ReadLine();
            Console.WriteLine("Unesite lozinku:");
            string pass = Console.ReadLine();

            

                if (credentialsStore.UserNameExist(name) && credentialsStore.UserPassExist(pass))
                {
                    if (!isLogged(name))
                    {
                        
                        DateTime logDate = DateTime.Now;
                        User user = new User(name, pass, logDate);
                        ulogovaniKorisnici.Add(user);
                        Console.WriteLine("Uspesno ste ulogovani");
                        
                        
                    }
                    else
                        Console.WriteLine("Korisnik je vec ulogovan!");


                }
                else
                {
                    if (!credentialsStore.UserNameExist(name))
                        Console.WriteLine("Ne postoji korisnik!");

                    else if (credentialsStore.UserNameExist(name) && !credentialsStore.UserPassExist(pass))
                    {
                        Console.WriteLine("Neispravna lozinka!");
                        numLog++;

                        if (numLog == credentialsStore.Rules(1))
                        {
                              Console.WriteLine("Nalog je blokiran");
                              //Nekako pozvati Majinu funkciju
                              numLog = 0;
                        }
                    }
                }
            
        }
        #endregion

        #region LogOut
        public void LogOut(/*string username*/)
        {
            #region pokusaj necega
            //dobijamo trenutnog korisnika
            /*string name = WindowsIdentity.GetCurrent().Name;
            string[] tekst = name.Split('\\');
            string userName = tekst[1];
            

            foreach(User user in ulogovaniKorisnici)
            {
                if(user.Username == userName)
                {
                    
                    string get_pass = credentialsStore.GetPassword(name);
                    Console.WriteLine("Unesite lozinku:");
                    string pass = Console.ReadLine();
                    if(get_pass == pass)
                    {
                        Console.WriteLine("Uspesno ste se izlogovali!");
                        ulogovaniKorisnici.Remove(user);
                    } else
                    {
                        Console.WriteLine("Pogresna lozinka!");
                    }
                }
                
            }*/

            #endregion

            X509Certificate2 clientCertificate = null;
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


        #region CheckIsLogged

        private bool isLogged(string username)
        {

            foreach(User user in ulogovaniKorisnici)
            
                if (user.Username == username)
                    return true;

            return false;
            
        }

        #endregion
    
    }
}
