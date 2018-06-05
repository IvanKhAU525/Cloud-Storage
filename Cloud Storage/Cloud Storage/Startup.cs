using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Cloud_Storage.Startup))]
namespace Cloud_Storage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
