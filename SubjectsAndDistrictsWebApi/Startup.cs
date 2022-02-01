using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SubjectsAndDistrictsDbContext;
using SubjectsAndDistrictsDbContext.Connection;
using SubjectsAndDistrictsDbContext.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubjectsAndDistrictsWebApi.BL.Services;
using SubjectsAndDistrictsWebApi.BL.Auth;
using Microsoft.OpenApi.Models;

namespace SubjectsAndDistrictsWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connStr = string.Format(Configuration.GetConnectionString("SaConnection"),
                        Configuration["SNDDb:Server"],
                        Configuration["SNDDb:UserId"],
                        Configuration["SNDDb:Password"])
                        ?? Environment.GetEnvironmentVariable("SNDDb_ConnectionString");

            services.AddDbContext<SubjectsAndDistrictsContext>(options =>
                options.UseSqlServer(connStr));

            services.AddIdentity<UserDbDTO, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            })
                .AddEntityFrameworkStores<SubjectsAndDistrictsContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<UserDbDTO>, AdditionalUserClaimsPrincipalFactory>();

            services.AddRepositories();
            services.AddServices();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "SND API" });
            });

            services.AddScoped<IdentityDataInitializer>();
            services.AddHostedService<SetupIdentityDataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "SND API V1");
                    s.RoutePrefix = string.Empty;
                });*/
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
