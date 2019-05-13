using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Logic.MsSql;
using Model.ViewModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="model">抽奖视图模型</param>
        /// <returns></returns>
        [HttpPost("{openId}, {openGid}")]
        public ReturnModel<dynamic> Post(LotteryVM model)
        {
            return new ReturnModel<dynamic>();
        }
    }
}