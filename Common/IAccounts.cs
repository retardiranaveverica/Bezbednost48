using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IAccounts
    {
        //morace da se salje stringovi zbog referenci
        //pravi se ona cirkularna nesto

        [OperationContract]
        void CreateAccount();

        [OperationContract]
        void DeleteAccount();

        [OperationContract]
        void LockAccount(string username);

        [OperationContract]
        void EnableAccount(string username);

        [OperationContract]
        void DisableAccount(string username);

        [OperationContract]
        void ReadFromFile();

    }
}
