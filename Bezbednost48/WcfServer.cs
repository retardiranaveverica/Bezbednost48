using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Bezbednost48
{
    public class WcfServer : ChannelFactory<IAccounts>, IDisposable
    {
        IAccounts factory;

        public WcfServer(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //-------------------------------------------------------------------------------------------------//
            //proveriti sta radi ovo
            //predstavljanje usera
            Credentials.Windows.AllowedImpersonationLevel =
              System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public void CreateAccount()
        {
            try
            {
                factory.CreateAccount();
            }
            catch (Exception e)
            {
                Console.WriteLine("[CreateAccount] ERROR = {0}", e.Message);
            }
        }

          public void DeleteAccount()
        {
            try
            {
                factory.DeleteAccount();
            }
            catch (Exception e)
            {
                Console.WriteLine("[DeleteAccount] ERROR = {0}", e.Message);
            }
        }

        public void ReadFromFile()
        {
            try
            {
                factory.ReadFromFile();
            }
            catch (Exception e)
            {
                Console.WriteLine("[ReadFromFile] ERROR = {0}", e.Message);
            }
        }


    }
}
