using Foundation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Api.Service
{
    public class ApiTokenService
    {        
        /// <summary>
        /// api接口服务
        /// </summary>
        public interface IApiTokenService
        {
            /// <summary>
            /// 转换成token
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="userName"></param>
            /// <returns></returns>
            string ConvertLoginToken(int userId, string userName);

            /// <summary>
            /// 根据token解密信息
            /// </summary>
            /// <returns></returns>
            UserInfo GetUserInfoByToken();
        }
        public class UserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
        }
        /// <summary>
        /// apiToken_唯一key
        /// </summary>
        public class ApiTokenConfig
        {
            public ApiTokenConfig(string key)
            {
                ApiToken = key;
            }
            public string ApiToken { get; set; }
        }

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _apiToken;

        public ApiTokenService(ApiTokenConfig token, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiToken = token.ApiToken;
        }
        /// <summary>
        /// 换取登录token 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string ConvertLoginToken(int userId, string userName)
        {
            string userJson = JsonConvert.SerializeObject(new UserInfo() { UserId = userId, UserName = userName });
            return Utils.ToBase64hmac(userJson.ToString());
        }
        private UserInfo _cacheUserInfo;

        /// <summary>
        /// 获取登录信息 
        /// </summary>
        /// <remarks>
        /// 获取header或者参数携带的token参数
        /// </remarks>
        /// <returns></returns>
        public UserInfo GetUserInfoByToken()
        {
            if (_cacheUserInfo != null)
            {
                return _cacheUserInfo;
            }
            var token = _httpContextAccessor.HttpContext.Request.Headers["Token"];
            //header或者query带有x-token参数
            token = string.IsNullOrEmpty(token) ? _httpContextAccessor.HttpContext.Request.Query["token"] : token;
            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            //_cacheUserInfo = JwtHelper.Decode<UserApiTokenPayload>(token, _api_key_token);
            return _cacheUserInfo;
        }
    }
}
