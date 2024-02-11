using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PatientManagement.Startup))]
namespace PatientManagement
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
