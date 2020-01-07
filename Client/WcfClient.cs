using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class WcfClient : ChannelFactory<IAccounts>, IDisposable

    {
        IAccounts factory;

        public WcfClient(NetTcpBinding binding, EndpointAddress address) : base(binding, address)
        {
            //-------------------------------------------------------------------------------------------------//
            //proveriti sta radi ovo
            //predstavljanje usera
            Credentials.Windows.AllowedImpersonationLevel =
              System.Security.Principal.TokenImpersonationLevel.Impersonation;
            factory = this.CreateChannel();
        }

    
    }
}
