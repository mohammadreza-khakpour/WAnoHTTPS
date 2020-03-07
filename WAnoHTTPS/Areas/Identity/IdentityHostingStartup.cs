using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WAnoHTTPS.Areas.Identity.Data;
using WAnoHTTPS.Data;

[assembly: HostingStartup(typeof(WAnoHTTPS.Areas.Identity.IdentityHostingStartup))]
namespace WAnoHTTPS.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WAnoHTTPSContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WAnoHTTPSContextConnection")));

                services.AddDefaultIdentity<WAnoHTTPSUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<WAnoHTTPSContext>();
            });
        }
    }
}