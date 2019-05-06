using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api
{
    public class Startup
    {
        private MapperConfiguration _mapperConfiguration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile(new OrganizeSKUProfile());
                //cfg.AddProfile(new StockProfile());
            });
        }

        public IConfiguration Configuration { get; }
        private const string _Project_Name = "AspNetCoreSwaggerDemo";//nameof(AspNetCoreSwaggerDemo);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton(new ApiTokenConfig("A3FFB16D-D2C0-4F25-BACE-1B9E5AB614A6"));
            //services.AddScoped<IApiTokenService, ApiTokenService>();

            //services.AddSwaggerGen(c =>
            //{
            //    typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
            //    {
            //        c.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info
            //        {
            //            Version = version,
            //            Title = $"{_Project_Name} 接口文档",
            //            Description = $"{_Project_Name} HTTP API " + version,
            //            TermsOfService = "None"
            //        });
            //    });
            //    var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
            //    var xmlPath = System.IO.Path.Combine(basePath, $"{_Project_Name}.xml");
            //    c.IncludeXmlComments(xmlPath);
            //    c.OperationFilter<AssignOperationVendorExtensions>();
            //    c.DocumentFilter<ApplyTagDescriptions>();
            //});

            services.AddMvcCore().AddAuthorization().AddJsonFormatters();
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication(Options =>
            {
                Options.Authority = "http://localhost:11000";
                Options.RequireHttpsMetadata = false;
                Options.ApiName = "Api";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IMapper>(s => _mapperConfiguration.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    //ApiVersions为自定义的版本枚举
                //    typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                //    {
                //        //版本控制
                //        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{_Project_Name} {version}");
                //    });
                //    //注入汉化脚本
                //    c.InjectOnCompleteJavaScript($"/swagger_translator.js");
                //});
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
