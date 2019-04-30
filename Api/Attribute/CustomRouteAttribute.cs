using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Api.Models;

namespace Api.Attribute
{
    /// <summary>
    /// 自定义路由 /api/{version=v1}/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {
        /// <summary>
        /// 自定义路由构造函数
        /// </summary>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(string actionName = "[action]") : base("/api/{X-Version}/[controller]/" + actionName)
        {
        }
        /// <summary>
        /// 自定义路由构造函数
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="version"></param>
        public CustomRouteAttribute(ApiVersions version, string actionName = "[action]") : base($"/api/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }
    }    
}
