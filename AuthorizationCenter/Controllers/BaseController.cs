using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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