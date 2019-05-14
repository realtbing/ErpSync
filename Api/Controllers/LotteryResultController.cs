using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Models;
using Logic.MsSql;
using Model.Appsettings;
using Model.DTO.MsSql;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryResultController : ControllerBase
    {
        private readonly IOptions<Appsetting> _options;
        WC_CrowdLuckDrawLogic wc_CrowdLuckDrawLogic;

        public LotteryResultController(IOptions<Appsetting> options)
        {
            _options = options;
            wc_CrowdLuckDrawLogic = new WC_CrowdLuckDrawLogic(_options);
        }

        /// <summary>
        /// 抽奖结果显示
        /// </summary>
        /// <param name="openId">用户OpenId</param>
        /// <param name="openGid">群Id</param>
        /// <returns></returns>
        [HttpGet("openid={openId}&opengid={openGid}")]
        //[HttpGet]
        public ReturnModel<WC_CrowdLuckDrawResultDTO> Get(string openId, string openGid)
        {
            ReturnModel<WC_CrowdLuckDrawResultDTO> result = new ReturnModel<WC_CrowdLuckDrawResultDTO>();
            var returnDto = wc_CrowdLuckDrawLogic.GetResult(openId, openGid);
            if (returnDto == null)
            {
                result.Type = ResultType.Error;
                result.Message = "object is not exist";
            }
            else
            {
                result.Result = returnDto;
            }
            return result;
        }
    }
}