using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Term_Project.Data;
using Term_Project.Models;

[assembly: HostingStartup(typeof(Term_Project.Areas.Identity.IdentityHostingStartup))]
namespace Term_Project.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddIdentity<MIU_IdentityUser, IdentityRole>()
                //.AddRoles<IdentityRole>()
                //.AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}