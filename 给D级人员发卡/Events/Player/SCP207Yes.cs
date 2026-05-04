using Exiled.Events.EventArgs.Player;

namespace CSARCHsPlugins.Events.Player
{
    public class SCP207Yes
    {
        private readonly Config config;
        public SCP207Yes(Config config)
        {
            this.config = config;
        }
        public void OnHurting(HurtingEventArgs ev)
        {
            if (!config.SCP207.Enabled)
                return;
            var evType = ev.GetType();
            object damageTypeValue = null;
            var prop = evType.GetProperty("DamageType") ?? evType.GetProperty("Type") ?? evType.GetProperty("AttackType") ?? evType.GetProperty("DamageHandler");
            if (prop != null)
            {
                damageTypeValue = prop.GetValue(ev);
            }
            bool isScp207 = false;
            if (damageTypeValue != null)
            {
                var s = damageTypeValue.ToString();
                if (s.IndexOf("207", System.StringComparison.OrdinalIgnoreCase) >= 0 || s.IndexOf("Scp207", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    isScp207 = true;
            }
            if (!isScp207)
            {
                var dhProp = evType.GetProperty("DamageHandler") ?? evType.GetProperty("DamageSource");
                if (dhProp != null)
                {
                    var dh = dhProp.GetValue(ev);
                    if (dh != null && dh.GetType().Name.IndexOf("Scp207", System.StringComparison.OrdinalIgnoreCase) >= 0)
                        isScp207 = true;
                }
            }
            if (isScp207)
            {
                ev.Amount = 0f;
                ev.IsAllowed = false;
            }
        }
    }
}