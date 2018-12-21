using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lowner.Context;
using Lowner.Repositories;
using Lowner.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lowner
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<LoanValidator>();
            services.AddTransient<UserValidator>();
            services.AddEntityFrameworkSqlServer().AddDbContext<LoanContext>(options => options.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Trusted_Connection=True;Database=Lowner"));
            services.AddTransient<ILoanRepository, DbLoanRepository>();
            services.AddTransient<IUserRepository, DbUserRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
