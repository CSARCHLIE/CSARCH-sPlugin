namespace CSARCHsPlugins.API.Enums
{
    /// <summary>
    /// 广播消息类型枚举
    /// </summary>
    public enum BroadcastMessageType
    {
        /// <summary>
        /// 普通消息
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 成功消息
        /// </summary>
        Success = 1,

        /// <summary>
        /// 警告消息
        /// </summary>
        Warning = 2,

        /// <summary>
        /// 错误消息
        /// </summary>
        Error = 3,

        /// <summary>
        /// 信息消息
        /// </summary>
        Info = 4,

        /// <summary>
        /// 系统消息
        /// </summary>
        System = 5,

        /// <summary>
        /// 游戏事件消息
        /// </summary>
        GameEvent = 6
    }
}
