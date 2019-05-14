using System;
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
    public class LotteryHistoryController : ControllerBase
    {
        private readonly IOptions<Appsetting> _options;
        WC_CrowdLuckDrawLogic wc_CrowdLuckDrawLogic;

        public LotteryHistoryController(IOptions<Appsetting> options)
        {
            _options = options;
            wc_CrowdLuckDrawLogic = new WC_CrowdLuckDrawLogic(_options);
        }

        /// <summary>
        /// 抽奖历史结果显示
        /// </summary>
        /// <param name="lotteryDate">抽奖日期</param>
        /// <param name="openGid">群Id</param>
        /// <returns></returns>
        [HttpGet("openid={openId}&opengid={openGid}")]
        public ReturnModel<WC_CrowdLuckDrawHistoryResultDTO> Get(DateTime lotteryDate, string openGid)
        {
            ReturnModel<WC_CrowdLuckDrawHistoryResultDTO> result = new ReturnModel<WC_CrowdLuckDrawHistoryResultDTO>();
            var dto = wc_CrowdLuckDrawLogic.GetHistoryResult(lotteryDate, openGid);
            if (dto == null)
            {
                result.Type = ResultType.Error;
                result.Message = "Not found lottery record";
            }
            else
            {
                result.Type = ResultType.Success;
                result.Result = dto;
            }
            return result;
        }
    }
}