using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadBalancerSvc.Management
{

    public class Account
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
    }

    public class Registration
    {
        public bool Register(Account account)
        {
            //  Action
            return false;
        }
    }

    public class DeletingAccount 
    {
        public bool DeleteAccount(Account account) { return false; }
    }
}