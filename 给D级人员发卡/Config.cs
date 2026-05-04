using Exiled.API.Interfaces;
using System.ComponentModel;
using System.Collections.Generic;
namespace CSARCHsPlugins
{
    public class Config : IConfig
    {
        [Description("是否启用插件")]
        public bool IsEnabled { get; set; } = true;
        [Description("是否启用调试模式")]
        public bool Debug { get; set; } = false;
        [Description("D级人员发卡系统配置")]
        public GiveKeyCardConfig GiveKeyCard { get; set; } = new GiveKeyCardConfig();
        [Description("无限子弹系统配置")]
        public InfiniteAmmoConfig InfiniteAmmo { get; set; } = new InfiniteAmmoConfig();
        [Description("清洁工系统配置")]
        public CleanerConfig Cleaner { get; set; } = new CleanerConfig();
        [Description("感应卡系统配置")]
        public KeycardSystemConfig KeycardSystem { get; set; } = new KeycardSystemConfig();
        [Description("SCP-207不掉血")]
        public Scp207Config SCP207 { get; set; } = new Scp207Config();

        [Description("MTF 列兵钥匙卡修正配置（将特工卡替换为列兵卡）")]
        public MTFKeycardConfig MTFKeycard { get; set; } = new MTFKeycardConfig();

        [Description("SCP-3114 替换配置")]
        public SCP3114Config SCP3114 { get; set; } = new SCP3114Config();

    }

    public class Scp207Config
    {
        [Description("是否启用 SCP-207 无扣血")]
        public bool Enabled { get; set; } = true;
    }
    public class MTFKeycardConfig
    {
        [Description("是否启用 MTF 列兵钥匙卡修正（将 KeycardMTFOperative 替换为 KeycardMTFPrivate）")]
        public bool Enabled { get; set; } = true;
    }
    public class GiveKeyCardConfig
    {
        [Description("是否给予D级人员钥匙卡")]
        public bool GiveKeycard { get; set; } = true;
        [Description("广播持续时间（秒）")]
        public ushort BroadcastDuration { get; set; } = 10;
        [Description("广播消息内容")]
        public string BroadcastMessage { get; set; } = "<color=orange>你已被发放D级人员卡，请注意保管好您的钥匙卡。</color>";
        [Description("给予D级人员的钥匙卡类型")]
        public ItemType KeycardType { get; set; } = ItemType.KeycardJanitor;
        [Description("是否清空D级人员的物品栏")]
        public bool ClearInventory { get; set; } = true;
        [Description("是否在发卡时广播（可关闭）")]
        public bool BroadcastEnabled { get; set; } = true;
        [Description("广播前缀（可包含富文本颜色标签）")]
        public string BroadcastPrefix { get; set; } = "<color=orange>[发卡]</color> ";
        [Description("发卡广播消息模板（可使用 {prefix} 和 {player} 占位）")]
        public string BroadcastMessageTemplate { get; set; } = "{prefix}你已被发放D级人员卡，请注意保管好您的钥匙卡。";
    }
    public class InfiniteAmmoConfig
    {
        [Description("是否启用无限子弹")]
        public bool Enabled { get; set; } = true;
        [Description("排除无限子弹效果的武器类型（空则不排除）\n可填值如下（枚举示例）：\nGunE11SR, GunCrossvec, GunFSP9, GunLogicer, GunRevolverChip, GunAK, GunShotgun, Jailbird, Flashlight, Lantern, Medkit, Adrenaline, Coin, KeycardJanitor, KeycardScientist, KeycardResearchCoordinator 等\n示例配置: - GunAK\n          - GunShotgun\n          - Jailbird")]
        public List<ItemType> ExcludedWeapons { get; set; } = new List<ItemType>();
    }
    public class CleanerConfig
    {
        [Description("是否启用清洁工")]
        public bool Enabled { get; set; } = true;
        [Description("清洁频率（秒）")]
        public float Interval { get; set; } = 180f;
        [Description("是否清理尸体")]
        public bool CleanRagdolls { get; set; } = true;
        [Description("是否清理弹药")]
        public bool CleanAmmo { get; set; } = true;
        [Description("广播持续时间（秒）")]
        public ushort BroadcastDuration { get; set; } = 8;
        [Description("广播消息模板（可使用 {rag} 和 {ammo} 占位）")]
        public string BroadcastMessage { get; set; } = "<color=yellow>清理完成 | 尸体:{rag} 弹药:{ammo}</color>";
        [Description("是否在清理完成时显示广播（可关闭）")]
        public bool AnnounceOnClean { get; set; } = true;
        [Description("广播前缀（可包含富文本颜色标签）")]
        public string BroadcastPrefix { get; set; } = "<color=yellow>[清洁]</color> ";
    }
    public class KeycardSystemConfig
    {
        [Description("是否启用感应卡系统")]
        public bool Enabled { get; set; } = true;
        [Description("为特定身份自定义钥匙卡（键 = 身份名，值 = 钥匙卡类型）")]
        public Dictionary<string, ItemType> RoleKeycardOverrides { get; set; } = new Dictionary<string, ItemType>();
    }

    public class PersonalizationConfig
    {
        [Description("语言代码，例如 zh-cn、en-us")]
        public string Language { get; set; } = "zh-cn";
        [Description("是否启用附加调试信息（仅供开发时使用）")]
        public bool ShowDebugInfo { get; set; } = false;
    }

    public class SCP3114Config
    {
        [Description("是否启用 SCP-3114 替换功能")]
        public bool Enabled { get; set; } = true;
        [Description("触发替换的最小玩家人数")]
        public int MinPlayersToReplace { get; set; } = 20;
        [Description("广播持续时间（秒）")]
        public ushort BroadcastDuration { get; set; } = 10;
        [Description("广播消息")]
        public string BroadcastMessage { get; set; } = "";
    }
}