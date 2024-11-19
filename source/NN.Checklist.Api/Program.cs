using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NN.Checklist.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NN.Checklist.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = Startup.Start(builder, true, "v1", "checklist", "Novo Nordisk Checklist", "Novo Nordisk Checklist Application API v1", true);
            app.UseStaticFiles();
            app.Run();

        }
    }
}
