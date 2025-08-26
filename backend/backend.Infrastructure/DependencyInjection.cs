using backend.Application.Interfaces.Persistence;
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
            services.AddScoped<IDropdownListRepository, DropdownListRepository>();
            return services;
        }
    }
}