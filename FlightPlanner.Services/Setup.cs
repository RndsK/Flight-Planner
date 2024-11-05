using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlightPlanner.Services
{
    public static class Setup
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDbService, DbService>();
            services.AddScoped<IDbClearingService, DbClearingService>();
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped<IFlightService, FlightService>();

            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(executingAssembly);

            services.AddAutoMapper(executingAssembly);

            
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(executingAssembly));
        }
    }
}
