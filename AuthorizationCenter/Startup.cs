using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddIdentityServer()
            //        .AddDeveloperSigningCredential()//添加开发人员签名凭据
            //        .AddInMemoryApiResources(Config.GetResources())//添加内存apiresource
            //        .AddInMemoryClients(Config.GetClients());//添加内存client

            //RSA：证书长度2048以上，否则抛异常
            //配置AccessToken的加密证书
            var rsa = new RSACryptoServiceProvider();
            //从配置文件获取加密证书
            rsa.ImportCspBlob(Convert.FromBase64String(Configuration["SigningCredential"]));
            //配置IdentityServer4
            services.AddSingleton<IClientStore, MyClientStore>();   //注入IClientStore的实现，可用于运行时校验Client
            services.AddSingleton<IResourceStore, MyResourceStore>();    //注入IScopeStore的实现，可用于运行时校验Scope
                                                                   
            //注入IPersistedGrantStore的实现，用于存储AuthorizationCode和RefreshToken等等，默认实现是存储在内存中，
            //如果服务重启那么这些数据就会被清空了，因此可实现IPersistedGrantStore将这些数据写入到数据库或者NoSql(Redis)中
            services.AddSingleton<IPersistedGrantStore, MyPersistedGrantStore>();
            services.AddIdentityServer()
                .AddSigningCredential(new RsaSecurityKey(rsa));
            //.AddTemporarySigningCredential()   //生成临时的加密证书，每次重启服务都会重新生成
            //.AddInMemoryScopes(Config.GetScopes())    //将Scopes设置到内存中
            //.AddInMemoryClients(Config.GetClients())    //将Clients设置到内存中
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
