using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FileApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(!Directory.Exists(Directory.GetCurrentDirectory()+ "/Great"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Great");
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
