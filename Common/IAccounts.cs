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
        void LockAccount();

        [OperationContract]
        void EnableAccount();

        [OperationContract]
        void DisableAccount();

        [OperationContract]
        void ReadFromFile();

    }
}
