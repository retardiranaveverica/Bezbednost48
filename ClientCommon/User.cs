using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCommon
{
    public class User
    {
        private string username;
        private string password;
        private bool accountDisabled = false;
        private bool accountLock = false;
        private DateTime dateTime;
        // private DateTime lastLogIn = DateTime.Now;
        


        public User()
        {

        }

        public User(string username, string password/*, bool accountDisabled, bool accountLock*/)
        {
            this.username = username;
            this.password = password;
            /*this.accountDisabled = accountDisabled;
            this.accountLock = accountLock;*/
        }

        public User(string username, string password, DateTime dateTime/*, bool accountDisabled, bool accountLock*/)
        {
            this.username = username;
            this.password = password;
            this.DateTime = dateTime;
            /*this.accountDisabled = accountDisabled;
            this.accountLock = accountLock;*/
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool AccountDisabled { get => accountDisabled; set => accountDisabled = value; }
        public bool AccountLock { get => accountLock; set => accountLock = value; }
        public DateTime DateTime { get => dateTime; set => dateTime = value; }
    }
}
