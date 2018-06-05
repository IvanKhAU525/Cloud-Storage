using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LoadBalancerSvc
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface ILoadBalancer
    {
        [OperationContract()]
        Stream DonwloadFile(string localPath);

        [OperationContract()]
        Stream DonwloadFiles(string localPath);

        [OperationContract()]
        bool UploadFile(Stream stream);

        [OperationContract()]
        int UploadFiles(string localPath);

        [OperationContract()]
        List<string> DisplayFiles(string localPath);

        [OperationContract()]
        int DeleteFile(string localPath);

        [OperationContract()]
        bool DeleteFiles(string localPath);

        [OperationContract()]
        bool CreateFile(string localPath);

        [OperationContract()]
        bool CreateFolder(string localPath);

        [OperationContract()]
        bool RegisterUser(string localPath);

        [OperationContract()]
        bool DeleteUser(string localPath);

        [OperationContract()]
        bool ExpUpld(string localPath);
    }
}
