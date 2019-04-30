using Foundation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attribute
{
    public class UserAuthorizeAttribute : System.Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var loginUser = ServiceLocator.Resolve<Service.ApiTokenService.IApiTokenService>().GetUserInfoByToken();
            if (loginUser == null)
            {
                context.Result = new JsonResult(new { status = 0, msg = "登录失效，请重新登录" });
                return;
            }
        }
    }
}
