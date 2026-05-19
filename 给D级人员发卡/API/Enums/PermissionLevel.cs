namespace CSARCHsPlugins.API.Enums
{
    /// <summary>
    /// 权限等级枚举
    /// </summary>
    public enum PermissionLevel
    {
        /// <summary>
        /// 无权限
        /// </summary>
        None = 0,

        /// <summary>
        /// 普通玩家
        /// </summary>
        Player = 1,

        /// <summary>
        /// 版主
        /// </summary>
        Moderator = 2,

        /// <summary>
        /// 管理员
        /// </summary>
        Administrator = 3,

        /// <summary>
        /// 高级管理员
        /// </summary>
        SeniorAdministrator = 4,

        /// <summary>
        /// 所有者/服务器主人
        /// </summary>
        Owner = 5
    }
}
