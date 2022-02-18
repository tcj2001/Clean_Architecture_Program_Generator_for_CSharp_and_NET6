//////////////////////////////////////////
// generated AddPersistenceExtension.cs //
//////////////////////////////////////////
using Domain.Interfaces;
using Persistence.Context;
using Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions
{
    public static class AddPersistenceExtension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Name=SqliteDb"));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Name=SqlServerDb"));
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
    }
}
