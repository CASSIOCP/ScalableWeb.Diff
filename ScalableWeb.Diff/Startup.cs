using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Promob.Manager.Api.Extensions;
using ScalableWeb.Diff.Api.Helpers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace ScalableWeb.Diff.Api
{
    public class Startup
    {
        private const string Title = "Diff Comparer";
        private const string Version = "v1";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));
            services.AddServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Version, new Info
                {
                    Title = Title,
                    Version = Version,
                    Description = "A scalable .NET Core web Api to check diff content.",
                    Contact = new Contact
                    {
                        Name = "Cássio C. Perin",
                        Email = "cassioperin@yahoo.com.br",
                        Url = $"https://www.linkedin.com/in/cassioperin/"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Title} {Version}");                
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}