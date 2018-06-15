using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace LoadBalancerSvc.Management
{
    static public class FtpOperations
    {
        static private FtpWebRequest _request;
        static private Uri _server = new FtpServers().GetFree()._uri;
        private const int _bufferSize = 4096;
        
        #region Folder Operations

        static public string CreateFolder(string name)
        {
            if (name != string.Empty)
            {
                try
                {
                    _request = (FtpWebRequest)WebRequest.Create(_server + name + "/");
                    return ExecuteRequest(WebRequestMethods.Ftp.MakeDirectory).StatusDescription;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                    return "*Some request error. More information -> Debug Console";
                }
                
            }
            return "*Invalid input";
        }   

        static public string DeleteFolder(string name)
        {
            if (name != string.Empty)
            {
                try
                {
                    _request = (FtpWebRequest)WebRequest.Create(_server + name + "/");
                    return ExecuteRequest(WebRequestMethods.Ftp.RemoveDirectory).StatusDescription;

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                    return "*Some request error. More information -> Debug Console";
                }

            }
            return "Invalid input";
        }

        static public string RenameFolder(string nameSourceFolder, string nameDestinationFolder)
        {
            if (nameSourceFolder != string.Empty || nameDestinationFolder != string.Empty)
            {
                _request = (FtpWebRequest)WebRequest.Create(_server + nameSourceFolder + "/");
                return ExecuteRequest(WebRequestMethods.Ftp.Rename, _request.RequestUri, nameDestinationFolder).StatusDescription;
            }
            return "Invalid input";
        }

        #endregion

        #region File Operations

        static public FtpWebResponse DownloadFile(string localPath)
        {
            if (localPath != string.Empty)
            {
                _request = (FtpWebRequest)WebRequest.Create(_server + localPath);
                return ExecuteRequest(WebRequestMethods.Ftp.DownloadFile);
            }
            return null;
        }

        static public string UploadFile(Stream stream, string localPath)
        {
            if (stream != null)
            {
                try
                {
                    _request = (FtpWebRequest)WebRequest.Create(_server + localPath);
                    return ExecuteRequest(stream, WebRequestMethods.Ftp.UploadFile).StatusDescription;
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                    return "*Some request error. More information -> Debug Console";
                }
            }
            return "*Stream is empty!";
        }

        static public string CreateFile(string localPath)
        {
            if (localPath != string.Empty)
            {
                try
                {
                    _request = (FtpWebRequest)WebRequest.Create(_server + localPath);
                    return ExecuteRequest(WebRequestMethods.Ftp.AppendFile).StatusDescription;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);

                    return "*Some request error. More information -> Debug Console";
                }
            }
            return "*Invalid input";
        }

        static public void DeleteFile(File file) { }

        static public void MoveFileToFolder(File file, Folder folder) { }

        static public void RenameFile(File file) { }

        #endregion

        #region Displaying F & D

        static public List<string> GetFilesAndDirectories(string localPath)
        {
            if (localPath != string.Empty)
            {
                var listOfFiles = new List<string>();
                _request = (FtpWebRequest)WebRequest.Create(_server + localPath);
                try
                {
                    var response = ExecuteRequest(WebRequestMethods.Ftp.ListDirectory);
                    var reader = new StreamReader(response.GetResponseStream());

                    while (!reader.EndOfStream) { listOfFiles.Add(reader.ReadLine()); }

                    foreach (var item in listOfFiles)
                    {
                        System.Diagnostics.Debug.WriteLine(item);
                    }

                    return listOfFiles;
                }
                catch (System.Net.WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    return null;
                }
            }
            return null;
        }

        #endregion

        #region Executing

        static private FtpWebResponse ExecuteRequest(string method)
        {
            if (method != string.Empty)
            {
                _request.Method = method;
                return (FtpWebResponse)_request.GetResponse();
            }
            return null;
        }

        //  for renaming and moving file/directry
        static private FtpWebResponse ExecuteRequest(string method, Uri sourceFolder, string nameDestinationFolder)
        {
            if (method                  != string.Empty     &&
                sourceFolder            != null             &&
                nameDestinationFolder   != string.Empty     &&
                nameDestinationFolder   != string.Empty)
            {
                Uri uriDestinationFolder = new Uri(nameDestinationFolder);
                Uri uriRelativeDestinationFolder = sourceFolder.MakeRelativeUri(uriDestinationFolder);

                _request.RenameTo = Uri.UnescapeDataString(uriRelativeDestinationFolder.OriginalString);
                _request.Method = method;

                return (FtpWebResponse)_request.GetResponse();
            }
            return null;
        }

        //  for uploading file
        static private FtpWebResponse ExecuteRequest(Stream stream, string method)
        {
            if (method != string.Empty && stream != null)
            {
                _request.Method = method;
                var ftpStream = _request.GetRequestStream();
                var buffer = new byte[_bufferSize];
                int sizePackage = 0;

                while ((sizePackage = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, buffer.Length);
                }
                ftpStream.Close();

                return (FtpWebResponse)_request.GetResponse();
            }
            return null;
        }

        #endregion
    }
}