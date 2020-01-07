using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ILog
    {
        [OperationContract]
        void LogIn(string username, string password);

        [OperationContract]
        void LogOut(string username);
    }
}
