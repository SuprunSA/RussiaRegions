using Microsoft.Extensions.DependencyInjection;
using SubjectsAndDistrictsDbContext.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectsAndDistrictsWebApi.BL.Services
{
    public static class ServiceRepositoryAdder
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<DistrictRepository>();// при каждом обращении созд. новый объект(при одном запросе мб несколько обращений!!!!!)
            services.AddTransient<SubjectRepository>();// singleton - один forall // // scoped - один на запрос
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<DistrictsService>();
            services.AddTransient<SubjectsService>();
            services.AddScoped<UserService>();
            ///////
        }
    }
}
