using Bezbednost48;
using Common;
using ServerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/CredentialManager";
            EndpointAddress endpointAddress = new EndpointAddress(new Uri(address));
            ServiceHost host = new ServiceHost(typeof(CredentialManager));
            host.AddServiceEndpoint(typeof(IAccounts), binding, address);

            //autorizacija
            //host.Authorization.ServiceAuthorizationManager = new MyAutorizationManager();

            ServiceSecurityAuditBehavior newAudit = new ServiceSecurityAuditBehavior();
            newAudit.AuditLogLocation = AuditLogLocation.Application;
            newAudit.ServiceAuthorizationAuditLevel = AuditLevel.SuccessOrFailure;
            newAudit.SuppressAuditFailure = true;

            host.Description.Behaviors.Remove<ServiceSecurityAuditBehavior>();
            host.Description.Behaviors.Add(newAudit);


            host.Open();
            //Host open
            using (WcfServer server = new WcfServer(binding, endpointAddress))
            {
                while (true)
                {
                    server.ReadFromFile();
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
                                    server.CreateAccount();
                                }
                                catch
                                {
                                    Console.WriteLine("Greska prilikom kreiranja naloga!\n");
                                }
                                break;
                            case 2:
                                try
                                {
                                    server.DeleteAccount();
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
            }

            //st.Close();
           



        }
    }
}
