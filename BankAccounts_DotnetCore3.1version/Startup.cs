using BankAccounts_DotnetCore3._1version.DbConnect;
using BankAccounts_DotnetCore3._1version.Interfaces;
using BankAccounts_DotnetCore3._1version.Respository;
using BankAccounts_DotnetCore3._1version.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Formatting.Compact;
using BankAccounts_DotnetCore3._1version.CustomMiddlewares;

namespace BankAccounts_DotnetCore3._1version
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        //Section1: Add services to the DepencyInjection container.
        public void ConfigureServices(IServiceCollection services)
        {//In ConfigureServices method conatins inbuilt depencyInjection(DI) container.
            //All Project level dependencies we need to register here.
            services.AddControllers();
            //Register the interfacename,interface implemented class.
            //Then only it will wotk in dotnet core.
            //otherwise it will throw the  dependency resove error.
            services.AddScoped<IBank,BankRepository>();
            services.AddScoped<IBankAccount,BankAccountRepository>();
            services.AddScoped<IDepartementService,DepartementService>();
            services.AddScoped<IDepartmentRepository,DepartementRepository>();
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            services.AddSingleton<IConnectionFactory,ConnectionFactory>();
            //without these 2 lines serilog will not work.
            var Configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("Serilog-Logs.json")
                            .Build();
            services.AddLogging();

          
            services.AddDbContext<BankAccountContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        //All Middlewares we need to register in configure method.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //add the custom middleware to application pipeline.
            //based on order you register into pipeline.same order middlewares will execute.
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
