using Catalog.Application.Services;
using Catalog.CommandProcessors;
using Catalog.EventHandlers;
using Catalog.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Catalog.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetSection("Connection").Value;
            services.AddScoped<IProductRepository>(r => new ProductRepository(connectionString));
            services.AddTransient<CheckoutService>();
            services.AddTransient<OrderCommandHandler>();
            services.AddTransient<OrderService>();
            services.AddTransient<OrderEventHandler>();
            services.AddTransient<OrderSucceededEventService>();
            services.AddTransient<OrderFailedEventService>();
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Catalog API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            const string swaggerUrl = "/swagger/v1/swagger.json";
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerUrl, "Catalog API");
            });
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
