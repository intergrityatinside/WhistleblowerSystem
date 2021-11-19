using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhistleblowerSystem.Database.DB;
using WhistleblowerSystem.Database.Interfaces;
using WhistleblowerSystem.Business.Mapping.AutoMapper;
using WhistleblowerSystem.Business.Services;
using WhistleblowerSystem.Database.Repositories;

namespace WhistleblowerSystem.Server.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void Init(IServiceCollection services, string dbName, string dbConnection)
        {
            ConfigureDbContext(services, dbName, dbConnection);
            services.AddSingleton<IDbContext>((serviceProvider) => serviceProvider.GetService<DbContext>()!);
            services.AddSingleton(_ => MapperFactory.Create());

            ConfigureRepositories(services);
            ConfigureBusinessServices(services);
            services.AddScoped<Authentication.UserManager>();
            services.AddScoped<Authentication.WhistleblowerManager>();
        }

        private static void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddScoped<AttachementService>();
            services.AddScoped<CompanyService>();
            services.AddScoped<FormService>();
            services.AddScoped<FormTemplateService>();
            services.AddScoped<UserService>();
            services.AddScoped<WhistleblowerService>();

        }

        private static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<AttachementRepository>();
            services.AddScoped<CompanyRepository>();
            services.AddScoped<FormRepository>();
            services.AddScoped<FormTemplateRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<WhistleblowerRepository>();

        }

        private static void ConfigureDbContext(IServiceCollection services, string dbName, string dbConnection)
        => services.AddSingleton(_ => new DbContext(dbName, dbConnection));

}
}
