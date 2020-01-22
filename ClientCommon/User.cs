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
        //  private bool accountDisabled = false;
        //private bool accountLock = false;
        // private DateTime lastLogIn = DateTime.Now;
        //private int numOfLog = 0;


        public User()
        {

        }

        public User(string username, string password/*, bool accountDisabled, bool accountLock, int numOfLog*/)
        {
            this.username = username;
            this.password = password;
            /*this.accountDisabled = accountDisabled;
            this.accountLock = accountLock;
            this.numOfLog = numOfLog;*/
        }


        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        // public bool AccountDisabled { get => accountDisabled; set => accountDisabled = value; }
        //public bool AccountLock { get => accountLock; set => accountLock = value; }
        // public int NumOfLog { get => numOfLog; set => numOfLog = value; }
    }
}
