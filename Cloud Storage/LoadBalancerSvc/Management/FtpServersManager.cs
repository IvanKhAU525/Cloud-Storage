using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancerSvc.Management
{
    public class FtpServer
    {
        public readonly Uri _uri;

        public FtpServer(string IP, int port)
        {
            _uri = new UriBuilder(Uri.UriSchemeFtp, IP, port).Uri;
        }
    }

    public class FtpServers
    {
        public FtpServers()
        {
            this.Servers = new List<FtpServer> { new FtpServer("192.168.1.6", 2122) };
        }

        private List<FtpServer> Servers { get; set; }

        public FtpServer GetFree()
        {
            //  logic of selecting ftp-server
            return this.Servers[0];
        }
    }
}