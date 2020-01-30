using Common;
using Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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
            Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            string cltCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            this.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
            this.Credentials.ServiceCertificate.Authentication.CustomCertificateValidator = new ClientCertValidator();
            this.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;

            this.Credentials.ClientCertificate.Certificate = CertManager.GetCertificateFromStorage(System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, cltCertCN);

            factory = this.CreateChannel();
        }

        public void LogIn(/*string username, string password*/)
        {
            try
            {
                factory.LogIn(/*username, password*/);
            }
            catch (Exception e)
            {
                Console.WriteLine("[LogIn] ERROR = {0}", e.Message);
            }
        }


        public void LogOut(/*string username*/)
        {
            try
            {
                factory.LogOut(/*username*/);
            }
            catch (Exception e)
            {
                Console.WriteLine("[LogOut] ERROR = {0}", e.Message);
            }
        }
    }
}
