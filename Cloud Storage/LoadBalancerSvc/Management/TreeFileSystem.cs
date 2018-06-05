using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancerSvc.Management
{
    public class TreeFileSystem
    {
        public Dictionary<string, TreeFileSystem> Tree { get; set; }
    }
}