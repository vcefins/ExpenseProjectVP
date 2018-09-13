using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExpenseV2.Startup))]
namespace ExpenseV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
