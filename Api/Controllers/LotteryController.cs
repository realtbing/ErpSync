using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Api.Models;
using Logic.MsSql;
using Model.Appsettings;
using Model.ViewModel;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        private readonly IOptions<Appsetting> _options;
        WC_CrowdLuckDrawLogic wc_CrowdLuckDrawLogic;

        public LotteryController(IOptions<Appsetting> options)
        {
            _options = options;
            wc_CrowdLuckDrawLogic = new WC_CrowdLuckDrawLogic(_options);
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="model">抽奖视图模型</param>
        /// <returns></returns>
        [HttpPost]
        public ReturnModel<int> Post(LotteryVM model)
        {
            ReturnModel<int> result = new ReturnModel<int>();
            var returnVal = wc_CrowdLuckDrawLogic.Lottery(model);
            result.Type = ResultType.Success;
            result.Result = returnVal;
            return result;
        }
    }
}