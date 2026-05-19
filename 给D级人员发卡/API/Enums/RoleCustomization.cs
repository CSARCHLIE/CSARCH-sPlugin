namespace CSARCHsPlugins.API.Enums
{
    /// <summary>
    /// 角色定制化选项枚举
    /// </summary>
    public enum RoleCustomization
    {
        /// <summary>
        /// 无定制
        /// </summary>
        None = 0,

        /// <summary>
        /// 启用清空物品栏
        /// </summary>
        ClearInventory = 1,

        /// <summary>
        /// 启用自动发卡
        /// </summary>
        AutoKeycard = 2,

        /// <summary>
        /// 启用广播提示
        /// </summary>
        EnableBroadcast = 4,

        /// <summary>
        /// 启用无限弹药
        /// </summary>
        UnlimitedAmmo = 8,

        /// <summary>
        /// 启用生命值保护（无限生命值）
        /// </summary>
        HealthProtection = 16,

        /// <summary>
        /// 启用位置锁定
        /// </summary>
        PositionLock = 32,

        /// <summary>
        /// 启用隐身模式
        /// </summary>
        InvisibilityMode = 64
    }
}
