namespace Model
{
    #region Oracle
    /// <summary>
    /// 触发类型
    /// /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// 商品子码价格更新(ORGANIZESKU-SPRICE:PRICE_TRIGGER)
        /// 直接更新到数据
        /// </summary>
        SKUChildPrice = 11,

        /// <summary>
        /// 商品母码价格更新(ORGANIZESKU-SPRICE:PRICE_TRIGGER)
        /// 记录到表，只记录最后一次变化的数据
        /// </summary>
        SKUMotherPrice = 12,

        /// <summary>
        /// 商品状态变更(SKU-STATUS:STATUS_TRIGGER)
        /// 淘汰和录入状态都是下架
        /// </summary>
        SKUStatus = 13,

        /// <summary>
        /// 配送入库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 16, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.ORGCODE = 'CS0003' AND o.ORDERTYPE = 16 ORDER BY o.CREATEON DESC;
        /// </summary>
        DistributeInStock = 21,

        /// <summary>
        /// 调拨入库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 5, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.REFORGCODE = 'CS0003' AND o.ORDERTYPE = 5 ORDER BY o.CREATEON DESC;
        /// </summary>
        AllotInStock = 22,

        /// <summary>
        /// 送货入库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 3, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.ORGCODE = 'CS0003' AND o.ORDERTYPE = 3 AND STATUS = 5 ORDER BY o.CREATEON DESC;
        /// </summary>
        DeliveryInStock = 23,

        /// <summary>
        /// 收银台退货入库库存更新(POSV2_SALEORDERDETAIL:SALEORCANCELQTY_TRIGGER)
        /// POSV2_SALEORDER:SALEORDERTYPE=2
        /// </summary>
        POSCancelInStock = 24,

        /// <summary>
        /// 收银台销售出库库存更新(POSV2_SALEORDERDETAIL:SALEORCANCELQTY_TRIGGER)
        /// POSV2_SALEORDER:SALEORDERTYPE=1
        /// </summary>
        POSSaleOutStock = 31,

        /// <summary>
        /// 报损出库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 13, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.ORGCODE = 'CS0003' AND o.ORDERTYPE = 13 ORDER BY o.CREATEON DESC;
        /// </summary>
        LossOutStock = 32,

        /// <summary>
        /// 调拨出库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 6, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.ORGCODE = 'CS0003' AND o.ORDERTYPE = 6 ORDER BY o.CREATEON DESC;
        /// </summary>
        AllotOutStock = 33,

        /// <summary>
        /// 退货出库库存更新(STOCKORDERDETAIL:QTYCHANGE_TRIGGER)
        /// STOCKORDER:ORDERTYPE = 4, STATUS = 5)
        /// SELECT o.CREATEON, o.ORDERTYPE, o.status, o.ORGCODE, o.REFORGCODE, d.GOODSCODE, d.QTY FROM STOCKORDER o INNER JOIN STOCKORDERDETAIL d ON o."ID" = d.ORDERID 
        /// WHERE o.ORGCODE = 'CS0003' AND o.ORDERTYPE = 4 ORDER BY o.CREATEON DESC;
        /// </summary>
        DeliveryCancelOutStock = 34,

        /// <summary>
        /// 商品子母码绑定转换变更库存更新(SKUTRANSFORM:TRANSFORM_TRIGGER)
        /// 先删除原有记录
        /// </summary>
        SKUTransformStock = 41,

        /// <summary>
        /// 商品转换率变库存更更新(SKUTRANSFORM:TRANSFORM_TRIGGER)
        /// </summary>
        SKUPerStock = 42
    }

    public enum TriggerDataStatus
    {
        /// <summary>
        /// 处理
        /// </summary>
        Process = 1,

        /// <summary>
        /// 处理成功
        /// </summary>
        ProcessSuccess = 2
    }

    public enum SKUStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 休眠
        /// </summary>
        Sleep = 2,

        /// <summary>
        /// 待淘汰
        /// </summary>
        WaitEliminate = 3,

        /// <summary>
        /// 淘汰
        /// </summary>
        Eliminate = 4,

        /// <summary>
        /// 录入
        /// </summary>
        NotAudit = 9
    }

    public enum POSV2_SaleOrderType
    {
        /// <summary>
        /// 销售
        /// </summary>
        Sale = 1,

        /// <summary>
        /// 退货
        /// </summary>
        Cancel = 2
    }

    public enum StockOrderType
    {
        /// <summary>
        /// 批发销售退货
        /// </summary>
        WholeSaleCancel = 1,

        /// <summary>
        /// 批发销售
        /// </summary>
        WholeSale = 2,

        /// <summary>
        /// 进货(供应商)
        /// </summary>
        Delivery = 3,

        /// <summary>
        /// 退货(供应商)
        /// </summary>
        DeliveryCancel = 4,

        /// <summary>
        /// 调入
        /// </summary>
        AllotIn = 5,

        /// <summary>
        /// 调出
        /// </summary>
        AllotOut = 6,

        /// <summary>
        /// 盘盈
        /// </summary>
        InventoryCheckSurplus = 7,

        /// <summary>
        /// 盘亏
        /// </summary>
        InventoryCheckLoss = 8,

        /// <summary>
        /// 转移
        /// </summary>
        Transfer = 9,

        /// <summary>
        /// 配送出库
        /// </summary>
        DistributeOut = 10,

        /// <summary>
        /// 返配
        /// </summary>
        DistributeBack = 11,

        /// <summary>
        /// 报溢
        /// </summary>
        ReportSurplus = 12,

        /// <summary>
        /// 报损
        /// </summary>
        ReportLoss = 13,

        /// <summary>
        /// 加工入库
        /// </summary>
        ProcessIn = 14,

        /// <summary>
        /// 加工出库
        /// </summary>
        ProcessOut = 15,

        /// <summary>
        /// 配送入库
        /// </summary>
        DistributeIn = 16,

        /// <summary>
        /// 退货入库
        /// </summary>
        ReturnIn = 17,

        /// <summary>
        /// 库间调入
        /// </summary>
        StockCallIn = 111,

        /// <summary>
        /// 库间调出
        /// </summary>
        StockCallOut = 112,

        /// <summary>
        /// 转码入库
        /// </summary>
        TransferCodeIn = 400,

        /// <summary>
        /// 转码出库
        /// </summary>
        TransferCodeOut = 401,

        /// <summary>
        /// 领用
        /// </summary>
        Receive = 500,

        /// <summary>
        /// 退领
        /// </summary>
        ReceiveCancel = 501
    }

    public enum StockOrderStatus
    {
        /// <summary>
        /// 初始化
        /// </summary>
        Init = 1,

        /// <summary>
        /// 审核
        /// </summary>
        Audit = 5,

        /// <summary>
        /// 审批
        /// </summary>
        Approval = 6,

        /// <summary>
        /// 完成
        /// </summary>
        Complete = 8,

        /// <summary>
        /// 取消
        /// </summary>
        Cancel = 9
    }
    #endregion

    #region MsSql
    public enum GoodsStatus
    {
        /// <summary>
        /// 上架
        /// </summary>
        PutOn = 1,

        /// <summary>
        /// 下架
        /// </summary>
        PullOff = 2
    }
    #endregion

    public enum EncryptType
    {
        /// <summary>
        /// 解密用户信息
        /// </summary>
        User = 1,

        /// <summary>
        /// 解密手机信息
        /// </summary>
        Mobile = 2,

        /// <summary>
        /// 解密登录信息
        /// </summary>
        Login = 3,

        /// <summary>
        /// 解密群信息
        /// </summary>
        OpenGid = 4
    }
}
