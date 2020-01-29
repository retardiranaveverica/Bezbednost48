using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace ClientCommon
{
    public class CredentialsStore
    {
        Cryptograpy cryptograpy = new Cryptograpy();
        string path = @"C:\Users\a\Desktop\Bezbednost - Projekat\Bezbednost48\BazaKorisnika.txt";
        //string path = @"\BazaKorisnika.txt";

        string path_pravila = @"C:\Users\a\Desktop\Bezbednost - Projekat\Bezbednost48\BazaPravila.txt";
        //string path_pravila = @"\BazaPravila.txt";


        //Ove dve funkcije se koriste kod LogIn-a
        //pretrazivanje imena
        #region Looking for username
        public bool UserNameExist(string username)
        {

            using (StreamReader streamReader = new StreamReader(path))
            {

                while (!streamReader.EndOfStream)
                {
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

        //pretrazivanje passworda
        #region Looking for password
        public bool UserPassExist(string password)
        {

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');
                    string decrypt_pass = cryptograpy.DecryptString(tekst[1]);
                    string userPass = decrypt_pass;

                    if (password == userPass)
                        return true;
                }

                return false;
            }
        }
        #endregion


        //Trazenje lozinke za odredjenog user-a
        #region Get pass for user

        public string GetPassword(string username)
        {
            string pass = "";

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');
                    string name = tekst[0];
                    string decrypt_pass = cryptograpy.DecryptString(tekst[1]);
                    string password = decrypt_pass;

                    if (username == name)
                    {
                        pass = password;
                    }
                }
            }

            return pass;
        }

        #endregion

        //Provera da li je korisnik ulogovan
        #region CheckIsLogged
   
        /*
        public bool isLogged(string username)
        {
            string name = WindowsIdentity.GetCurrent().Name;
            //Console.WriteLine(name);
            string[] names = name.Split('\\');
            string this_user = names[1];
            //Console.WriteLine(this_user);

            foreach (User user in ulogovaniKorisnici)

                 if (user.Username == username)
                     return true;

             return false;

            

            if (this_user == username)
                return true;
            else
                return false;

        }
        */
        #endregion





        //U zavisnoti od toga koje nam pravilo treba, vraca dozvoljeni broj
        #region Rules
        public int Rules(int pravilo)
        {
           
            int dozvoljen_br_neuspesnih;
            int vreme_za_otkljucavanje;
            int vreme_aktivnosti;

            using (StreamReader streamReader = new StreamReader(path_pravila))
            {
                while (!streamReader.EndOfStream)
                {
                    string text = streamReader.ReadLine();
                    string[] tekst = text.Split(' ');

                    dozvoljen_br_neuspesnih = Int32.Parse(tekst[0]);
                    vreme_za_otkljucavanje = Int32.Parse(tekst[1]);
                    vreme_aktivnosti = Int32.Parse(tekst[2]);

                    if (pravilo == 1)
                        return dozvoljen_br_neuspesnih;
                    else if (pravilo == 2)
                        return vreme_za_otkljucavanje;
                    else
                        return vreme_aktivnosti;
                }
            }

            return -1;
        }

        #endregion
    }
}
