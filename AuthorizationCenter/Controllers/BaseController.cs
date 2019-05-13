using Microsoft.AspNetCore.Mvc;
using NLog;

namespace AuthorizationCenter.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;
        public BaseController(ILogger logger)
        {
            _logger = logger;
        }
    }
}