using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RongRental.Startup))]
namespace RongRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
