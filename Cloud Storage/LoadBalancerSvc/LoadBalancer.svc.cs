using LoadBalancerSvc.Management;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LoadBalancerSvc
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class LoadBalancer : ILoadBalancer
    {

        bool ILoadBalancer.CreateFile(string file)
        {
            var result = FtpOperations.CreateFile(file);
            if (result[0] == '*') { return false; }
            return true;
        }

        bool ILoadBalancer.CreateFolder(string localPath)
        {
            var result = FtpOperations.CreateFolder('#' + localPath);
            if (result[0] == '*') { return false; }
            return true;
        }

        int ILoadBalancer.DeleteFile(string localPath)
        {
            throw new NotImplementedException();
        }

        bool ILoadBalancer.DeleteFiles(string localPath)
        {
            throw new NotImplementedException();
        }

        bool ILoadBalancer.DeleteUser(string mail)
        {
            var result = FtpOperations.DeleteFolder(mail);
            if (result[0] == '*') { return false; }
            return true;
        }

        List<string> ILoadBalancer.DisplayFiles(string path)
        {
            return FtpOperations.GetFilesAndDirectories(path);
        }

        Stream ILoadBalancer.DonwloadFile(string localPath)
        {
            return FtpOperations.DownloadFile(localPath).GetResponseStream();
        }

        Stream ILoadBalancer.DonwloadFiles(string localPath)
        {
            throw new NotImplementedException();
        }

        bool ILoadBalancer.RegisterUser(string mail)
        {
            var result = FtpOperations.CreateFolder(mail);
            if (result[0] == '*') { return false; }
            return true;
        }

        bool ILoadBalancer.UploadFile(Stream stream)
        {
            //var result = FtpOperations.UploadFile(stream);
            //if (result[0] == '*') { return false; }
            return true;
        }

        int ILoadBalancer.UploadFiles(string localPath)
        {
            throw new NotImplementedException();
        }

        bool ILoadBalancer.ExpUpld(string localPath)
        {
            using (var fs = System.IO.File.OpenRead(@"C:\Users\Иван\Desktop\script_mem.txt"))
            {
                var result = FtpOperations.UploadFile(fs, localPath);
                if (result[0] == '*') { return false; }
                return true;
            }
        }
    }
}
