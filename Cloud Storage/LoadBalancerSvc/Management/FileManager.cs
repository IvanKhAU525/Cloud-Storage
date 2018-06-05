using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// All operations with files
/// </summary>

namespace LoadBalancerSvc.Management
{
    public class File
    {
        string Name { get; set; }
        string Extension { get; set; }
        string Path { get; set; }
        double Size { get; set; }


    }

    public class Files
    {
        List<File> FileCollection { get; set; }
    }

    #region File Operation

    public class FileToSave
    {
        string SaveFile(File file)
        {
            return null;
        }
    }

    public class FileToDelete
    {
        bool DeleteFile(File file) { return false; }
    }

    public class FileToDownload
    {
        File DownloadFile(File file) { return null; }
    }

    #endregion

    #region Files Operation

    public class FilesToSave
    {
        string SaveFilel(Files files) { return null; }
    }

    public class FilesToDelete
    {
        bool DeleteFiles(File files) { return false; }
    }

    public class FilesToDownload
    {
        Files DownloadFile(Files files) { return null; }
    }

    public class FilesToDisplay
    {
        Files DisplayFiles() { return null; }
    } 

    #endregion
}