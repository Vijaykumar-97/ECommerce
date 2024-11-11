using eCommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInFrastructureService(this IServiceCollection services, IConfiguration config)
        {
            //Add database connectivity
            //Add authentication Scheme
            SharedServiceContainer.AddSharedServices<ProductDBcontext>(services, config, config["MySerilog:FileName"]!);

            //Create Dependency Injection (DI)
            services.AddScoped<IProduct, ProductRepository>();

            return services;
        }

        public static IApplicationBuilder useInfarstructurePolicy(this IApplicationBuilder app)
        {
            {
                //Register middleware such as:
                //Global Exception: handles  external errors.
                //Listen to only Api gateway: blocks the outsider calls
                SharedServiceContainer.UseSharedPolicies(app);

                return app;
            }
        }
    }
}
