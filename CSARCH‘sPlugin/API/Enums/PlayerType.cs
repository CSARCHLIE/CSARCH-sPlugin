namespace CSARCHsPlugins.API.Enums
{
    /// <summary>
    /// 玩家类型枚举
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// 未分配
        /// </summary>
        Unassigned = 0,

        /// <summary>
        /// D级人员
        /// </summary>
        ClassD = 1,

        /// <summary>
        /// 研究员
        /// </summary>
        Scientist = 2,



        /// <summary>
        /// MTF特工（列兵）
        /// </summary>
        MTFPrivate = 3,

        /// <summary>
        /// MTF特工（中士）
        /// </summary>
        MTFSergeant = 4,

        /// <summary>
        /// MTF特工（队长）
        /// </summary>
        MTFCaptain = 5,

        /// <summary>
        /// 混沌反叛分子
        /// </summary>
        ChaosInsurgent = 6,

        /// <summary>
        /// SCP-106
        /// </summary>
        SCP106 = 7,

        /// <summary>
        /// SCP-173
        /// </summary>
        SCP173 = 8,

        /// <summary>
        /// SCP-3114
        /// </summary>
        SCP3114 = 9,

        /// <summary>
        /// 守卫
        /// </summary>
        FacilityGuard = 10,

        /// <summary>
        /// 管理员/服务器
        /// </summary>
        Admin = 11
    }
}
