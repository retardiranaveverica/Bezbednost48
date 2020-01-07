using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            string address = "net.tcp://localhost:9999/CredentialManager";
            EndpointAddress endpointAdress = new EndpointAddress(new Uri(address));


            using (WcfClient proxy = new WcfClient(binding, endpointAdress))
            {
                Console.WriteLine("Uspeno ste konektovani!\n");
            }

            Console.ReadLine();
        }
    }
}
