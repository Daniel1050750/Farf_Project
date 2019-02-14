﻿using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Farf_Project.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(serviceCollection =>
                {
                    serviceCollection.AddSingleton(new ResourceManager(
                        "Farf_Project.Web.LocalizedResources.LocalizedResources", 
                        typeof(Startup).GetTypeInfo().Assembly)
                    );
                })
                .UseStartup<Startup>();
    }
}