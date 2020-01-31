using ClientCommon;
using Common;
using Manager;
using ServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string srvCertCN = "wcfs";

            NetTcpBinding binding = new NetTcpBinding();

            binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;
            X509Certificate2 srvCert = CertManager.GetCertificateFromStorage(StoreName.TrustedPeople, StoreLocation.LocalMachine, srvCertCN);

            //konekcija sa serverom
            string address = "net.tcp://localhost:9999/CredentialManager";
            EndpointAddress endpointAdress = new EndpointAddress(new Uri(address), 
                                                                    new X509CertificateEndpointIdentity(srvCert));
            ServiceHost host = new ServiceHost(typeof(CredentialManager));
            host.AddServiceEndpoint(typeof(IAccounts), binding, address);

            // WcfClient wcfClient = new WcfClient(binding, endpointAdress);
            AuthenticateService authenticateService = new AuthenticateService();
            
            

            using (WcfClient proxy = new WcfClient(binding, endpointAdress))
            {
                Console.WriteLine("Uspesno ste konektovani koristeci sertifikat {0}!\n", srvCertCN);

                while (true)
                {
                    Console.WriteLine("Unesite zeljenu akciju:");
                    Console.WriteLine("*************************");
                    Console.WriteLine("1. LogIn");
                    Console.WriteLine("2. LogOut");
                    Console.WriteLine("*************************");
                    try
                    {
                        int akcija = Int32.Parse(Console.ReadLine());

                        switch (akcija)
                        {
                            case 1:

                                /* Console.WriteLine("Unesite username:");
                                 string name = Console.ReadLine();
                                 Console.WriteLine("Unesite lozinku:");
                                 string pass = Console.ReadLine();*/
                                //proxy.LogIn(name, pass);
                                authenticateService.LogIn(/*name, pass*/);

                                break;
                            case 2:
                                authenticateService.LogOut();
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
            }

            //Console.ReadLine();
        }
    }
}
