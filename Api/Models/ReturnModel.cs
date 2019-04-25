namespace Api.Models
{
    /// <summary>
    /// APP接口统一标准模型
    /// </summary>
    public class ReturnModel<T>
    {
        public ReturnModel()
        {
            Type = ResultType.Success;
            Message = string.Empty;
        }

        /// <summary>
        /// 结果类型
        /// </summary>
        public ResultType Type { get; set; }

        /// <summary>
        /// 错误编码
        /// </summary>
        public string Errorcode { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }
    }

    /// <summary>
    /// APP接口统一标准模型，分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnModelPage<T> : ReturnModel<T>
    {
    }
}
