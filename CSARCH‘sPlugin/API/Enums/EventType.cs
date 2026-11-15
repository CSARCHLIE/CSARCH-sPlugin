namespace CSARCHsPlugins.API.Enums
{
    /// <summary>
    /// 事件类型枚举
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 玩家生成事件
        /// </summary>
        PlayerSpawned = 0,

        /// <summary>
        /// 玩家死亡事件
        /// </summary>
        PlayerDied = 1,

        /// <summary>
        /// 玩家获得物品事件
        /// </summary>
        PlayerItemAdded = 2,

        /// <summary>
        /// 玩家失去物品事件
        /// </summary>
        PlayerItemRemoved = 3,

        /// <summary>
        /// 玩家改变角色事件
        /// </summary>
        PlayerRoleChanged = 4,

        /// <summary>
        /// 玩家伤害事件
        /// </summary>
        PlayerDamaged = 5,

        /// <summary>
        /// 玩家治疗事件
        /// </summary>
        PlayerHealed = 6,

        /// <summary>
        /// 轮次重启事件
        /// </summary>
        RoundRestart = 7,

        /// <summary>
        /// 轮次结束事件
        /// </summary>
        RoundEnded = 8,

        /// <summary>
        /// 触发器触发事件
        /// </summary>
        TriggerActivated = 9,

        /// <summary>
        /// 物品生成事件
        /// </summary>
        ItemSpawned = 10
    }
}
