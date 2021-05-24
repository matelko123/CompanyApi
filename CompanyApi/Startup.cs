using CompanyApi.Entities;
using CompanyApi.Helpers;
using CompanyApi.Middleware;
using CompanyApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanyApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("BasicAuthentication", null);

            services.AddDbContext<CompanyDbContext>();
            services.AddScoped<CompanySeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CompanySeeder seeder)
        {
            seeder.Seed();

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // My Middlewares
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

            // Authorize
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
