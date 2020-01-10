using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WcfClient : ChannelFactory<ILog>, IDisposable

    {
        ILog factory;

        public WcfClient(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //-------------------------------------------------------------------------------------------------//
            //proveriti sta radi ovo
            //predstavljanje usera
            Credentials.Windows.AllowedImpersonationLevel =
              System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

        public void LogIn(string username, string password)
        {
            try
            {
                factory.LogIn(username, password);
            }
            catch (Exception e)
            {
                Console.WriteLine("[LogIn] ERROR = {0}", e.Message);
            }
        }


        public void LogOut(string username)
        {
            try
            {
                factory.LogOut(username);
            }
            catch (Exception e)
            {
                Console.WriteLine("[LogOut] ERROR = {0}", e.Message);
            }
        }
    }
}
