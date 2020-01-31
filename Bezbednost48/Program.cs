using Bezbednost48;
using Common;
using Manager;
using ServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Policy;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            //string srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name); 
            string srvCertCN = "wcfs";

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

            string address = "net.tcp://localhost:9999/AuthenticationService";
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));
            ServiceHost host = new ServiceHost(typeof(CredentialManager));
            host.AddServiceEndpoint(typeof(IAccounts), binding, address);
            
            host.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            host.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();
            host.Credentials.ClientCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
            host.Credentials.ServiceCertificate.Certificate = CertManager.GetCertificateFromStorage(System.Security.Cryptography.X509Certificates.StoreName.My, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine, srvCertCN);

            //autorizacija
           /* host.Authorization.ServiceAuthorizationManager = new MyAuthorizationManager();

            host.Authorization.PrincipalPermissionMode = PrincipalPermissionMode.Custom;
            List<IAuthorizationPolicy> policies = new List<IAuthorizationPolicy>();
            policies.Add(new CustomAuthorizationPolicy());
            host.Authorization.ExternalAuthorizationPolicies = policies.AsReadOnly();
            */
            host.Open();
            //Host open
            CredentialManager credentialManager = new CredentialManager();
            credentialManager.ReadFromFile();
                while (true)
                {
                //    server.ReadFromFile();
                    Console.WriteLine("\n*************************");
                    Console.WriteLine("1.Kreiraj nalog");
                    Console.WriteLine("2.Obrisi nalog");
                    Console.WriteLine("*************************");
                    try
                    {
                        int num = Int32.Parse(Console.ReadLine());

                        switch (num)
                        {
                            case 1:
                                try
                                {
                                    credentialManager.CreateAccount();
                                }
                                catch
                                {
                                    Console.WriteLine("Greska prilikom kreiranja naloga!\n");
                                }
                                break;
                            case 2:
                                try
                                {
                                    credentialManager.DeleteAccount();
                                }
                                catch
                                {
                                    Console.WriteLine("Greska prilikom brisanja naloga!\n");
                                }
                                break;

                            default:
                                Console.WriteLine("Unesite 1 ili 2!");
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Unesite 1 ili 2!");
                    }
                }
            

            //st.Close();
           



        }
    }
}
