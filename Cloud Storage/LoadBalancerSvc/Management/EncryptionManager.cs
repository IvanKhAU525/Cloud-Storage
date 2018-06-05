using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancerSvc.Management
{
    public class Encryptor
    {
        public virtual File EncryptFile(File file) { return null; }
        public virtual File EncryptFiles(Files files) { return null; }
    }

    public class Decryptor
    {
        public virtual File DecryptFile(File file) { return null; }
        public virtual Files DecryptFiles(Files files) { return null; }
    }

    public class FileEncryptionMD5 : Encryptor 
    {
        public override File EncryptFile(File file)
        {
            return base.EncryptFile(file);
        }

        public override File EncryptFiles(Files files)
        {
            return base.EncryptFiles(files);
        }
    }

    public class FileEncryptionDES : Encryptor
    {
        public override File EncryptFile(File file)
        {
            return base.EncryptFile(file);
        }

        public override File EncryptFiles(Files files)
        {
            return base.EncryptFiles(files);
        }
    }

    public class FileDecryptionMD5 : Decryptor
    {
        public override File DecryptFile(File file)
        {
            return base.DecryptFile(file);
        }

        public override Files DecryptFiles(Files files)
        {
            return base.DecryptFiles(files);
        }
    }

    public class FileDecryptionDES : Decryptor
    {
        public override File DecryptFile(File file)
        {
            return base.DecryptFile(file);
        }

        public override Files DecryptFiles(Files files)
        {
            return base.DecryptFiles(files);
        }
    }

}