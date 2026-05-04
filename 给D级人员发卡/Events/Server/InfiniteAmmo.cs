using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;

namespace CSARCHsPlugins.Events.Server
{
    public class InfiniteAmmo
    {
        public Config Config;
        public InfiniteAmmo(Config config)
        {
            Config = config;
        }
        public void OnRoundStarted()
        {
            if (!Config.InfiniteAmmo.Enabled)
                return;
            foreach (var pickup in Exiled.API.Features.Pickups.AmmoPickup.List)
            {
                if (pickup.Type == ItemType.Ammo9x19 || 
                    pickup.Type == ItemType.Ammo556x45 || 
                    pickup.Type == ItemType.Ammo762x39 || 
                    pickup.Type == ItemType.Ammo12gauge || 
                    pickup.Type == ItemType.Ammo44cal)
                {
                    pickup.Destroy();
                }
            }
        }

        public void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            if (!Config.InfiniteAmmo.Enabled)
                return;
            ev.Player.SetAmmo(AmmoType.Nato9, 0);
            ev.Player.SetAmmo(AmmoType.Nato556, 0);
            ev.Player.SetAmmo(AmmoType.Nato762, 0);
            ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 0);
            ev.Player.SetAmmo(AmmoType.Ammo44Cal, 0);
        }
        public void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
        {
            if (!Config.InfiniteAmmo.Enabled)
                return;
            ev.IsAllowed = true;
            var p = ev.Player;
            p.SetAmmo(AmmoType.Nato9, 60);
            p.SetAmmo(AmmoType.Nato556, 120);
            p.SetAmmo(AmmoType.Nato762, 100);
            p.SetAmmo(AmmoType.Ammo12Gauge, 16);
            ev.Player.SetAmmo(AmmoType.Ammo44Cal, 8);
        }
        public void OnReloadedWeapon(ReloadedWeaponEventArgs ev)
        {
            if (!Config.InfiniteAmmo.Enabled)
                return;
            var p = ev.Player;
            p.SetAmmo(AmmoType.Nato9, 0);
            p.SetAmmo(AmmoType.Nato556, 0);
            p.SetAmmo(AmmoType.Nato762, 0);
            p.SetAmmo(AmmoType.Ammo12Gauge, 0);
            ev.Player.SetAmmo(AmmoType.Ammo44Cal, 0);
        }
    }
}