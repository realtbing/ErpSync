using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Logic.Oracle;
using Model;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizeSKUPriceController : ControllerBase
    {
        OrganizeSKULogic organizeSKULogic = new OrganizeSKULogic();

        /// <summary>
        /// 获取ERP系统里门店商品的价格
        /// </summary>
        /// <param name="orgCode">门店Code</param>
        /// <param name="skuCode">商品Code</param>
        /// <returns>返回价格</returns>
        public Models.ReturnModel<decimal> Get(string orgCode, string skuCode)
        {
            Models.ReturnModel<decimal> result = new Models.ReturnModel<decimal>();
            var entity = organizeSKULogic.GetSingleByShopAndSKU1(orgCode, skuCode);
            if (entity == null)
            {
                result.Type = Models.ResultType.Error;
                result.Errorcode = "100001";
                result.Message = "SKU is not exits";
            }
            else
            {
                result.Result = entity.SPRICE.HasValue ? entity.SPRICE.Value : 0;
            }
            return result;
        }
    }
}
