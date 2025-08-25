using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Repositories;
using backend.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentRepository, AppointmentService>();
            return services;
        }
    }
}