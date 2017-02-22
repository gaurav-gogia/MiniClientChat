using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MultiClientChatDemo.Web.Startup))]

namespace MultiClientChatDemo.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
