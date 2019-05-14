using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Appsettings;
using Model.DTO.MsSql;
using Model.Profiles.MsSql;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api
{
    public class Startup
    {
        //private AutoMapper.IConfigurationProvider _mapperConfiguration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //_mapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<WC_CrowdProfile>();
            //});
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<WC_CrowdProfile>();
                cfg.AddProfile<WC_CrowdLuckDrawProfile>();
            });
        }

        public IConfiguration Configuration { get; }
        private const string _Project_Name = "Api";//nameof(AspNetCoreSwaggerDemo);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton(new ApiTokenConfig("A3FFB16D-D2C0-4F25-BACE-1B9E5AB614A6"));
            //services.AddScoped<IApiTokenService, ApiTokenService>();

            //https://www.cnblogs.com/morang/p/8325729.html
            //https://www.cnblogs.com/shy-huang/p/9102214.html
            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Swashbuckle.AspNetCore.Swagger.Info
                    {
                        Version = version,
                        Title = $"{_Project_Name} 接口文档",
                        Description = $"{_Project_Name} HTTP API " + version,
                        TermsOfService = "None"
                    });
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{_Project_Name}.xml");
                c.IncludeXmlComments(xmlPath);
                //c.OperationFilter<AssignOperationVendorExtensions>();
                //c.DocumentFilter<ApplyTagDescriptions>();
                c.DescribeAllEnumsAsStrings();
            });

            //https://www.jb51.net/article/132815.htm
            services.AddMvcCore()
                    ////.AddAuthorization()
                    .AddJsonFormatters();
            ////services.AddAuthentication("Bearer").AddIdentityServerAuthentication(Options =>
            ////{
            ////    Options.Authority = "http://localhost:11000";
            ////    Options.RequireHttpsMetadata = false;
            ////    Options.ApiName = "Api";
            ////});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var csredis = new CSRedis.CSRedisClient(Configuration["ConnectionStrings:RedisDatabase"]);
            RedisHelper.Initialization(csredis);
            services.AddSingleton<IDistributedCache>(new Microsoft.Extensions.Caching.Redis.CSRedisCache(RedisHelper.Instance));

            //AutoMapper.IConfigurationProvider _mapperConfiguration = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<WC_CrowdProfile>();
            //});
            //services.AddSingleton(_mapperConfiguration);
            //services.AddScoped<IMapper, Mapper>();

            services.AddOptions();
            services.Configure<Appsetting>(Options =>
            {
                Options.IsCheckUserAndCrowdRelation = bool.Parse(Configuration["IsCheckUserAndCrowdRelation"]);
                Options.LotteryMinute = int.Parse(Configuration["LotteryMinute"]);
                Options.CrowdUserCount = int.Parse(Configuration["CrowdUserCount"]);
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //ApiVersions为自定义的版本枚举
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    //版本控制
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{_Project_Name} {version}");
                });
                //注入汉化脚本(3.0后不支持汉化)
                //c.InjectOnCompleteJavaScript($"/swagger_translator.js");
            });
            ////app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    ////添加标签
    //public class ApplyTagDescriptions : IDocumentFilter
    //{
    //    public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
    //    {
    //        swaggerDoc.Tags = new List<Tag>
    //        {
    //            //添加对应的控制器描述 这个是好不容易在issues里面翻到的
    //            new Tag { Name = "OrganizeSKUPrice", Description = "获取SKU售价" },
    //            new Tag { Name = "RealStock", Description = "获取SKU真实库存" }
    //        };
    //    }
    //}

    ////添加通用参数，若in='header'则添加到header中,默认query参数
    //public class AssignOperationVendorExtensions : IOperationFilter
    //{
    //    public void Apply(Operation operation, OperationFilterContext context)
    //    {
    //        operation.Parameters = operation.Parameters ?? new List<IParameter>();
    //        //MemberAuthorizeAttribute 自定义的身份验证特性标记
    //        var isAuthor = operation != null && 
    //            context != null && 
    //            context.ApiDescription.ControllerAttributes().Any(e => e.GetType() == typeof(MemberAuthorizeAttribute)) || context.ApiDescription.ActionAttributes().Any(e => e.GetType() == typeof(MemberAuthorizeAttribute));
    //        if (isAuthor)
    //        {
    //            //in query header 
    //            operation.Parameters.Add(new NonBodyParameter()
    //            {
    //                Name = "x-token",
    //                In = "header", //query formData ..
    //                Description = "身份验证票据",
    //                Required = false,
    //                Type = "string"
    //            });
    //        }
    //    }
    //}
}
