using Funda.Assignment.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Funda.Assignment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddApiControllers()
                .AddVersioning()
                .AddSwagger()
                .AddMediatR()
                .AddFundaPartnerApi(Configuration)
                .AddUseCases();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseAuthorization();
            app.UseSwaggerDocumentation();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}