using System;

namespace Model.Entities.Oracle
{
    public class PP_TriggerDataBak
    {
        /// <summary>
        /// 序号(GUID)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 对象类型(1:商品价格更新;2:入库后库存更新;3:销售后库存更新;4:退货后库存更新;5:报损后库存更新;6:商品销售状态变更)
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 触发类型(1:insert;2:update;)
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        public decimal ChangeValue { get; set; }

        /// <summary>
        /// 数据状态(第1位:是否处理;第2位:处理是否成功)
        /// </summary>
        public int DataStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
