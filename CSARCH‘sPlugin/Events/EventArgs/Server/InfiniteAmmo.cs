using Exiled.API.Enums;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;

namespace CSARCHsPlugins.Events.EventArgs.Server
{
    public class InfiniteAmmo
    {
        private readonly Config _config;

        public InfiniteAmmo(Config config)
        {
            _config = config;
        }
        public void OnRoundStarted()
        {
            if (!_config.InfiniteAmmo.Enabled)
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
            if (!_config.InfiniteAmmo.Enabled)
                return;
            if (!IsAmmoTypeExcluded(ev.Player, AmmoType.Nato9))
                ev.Player.SetAmmo(AmmoType.Nato9, 0);
            if (!IsAmmoTypeExcluded(ev.Player, AmmoType.Nato556))
                ev.Player.SetAmmo(AmmoType.Nato556, 0);
            if (!IsAmmoTypeExcluded(ev.Player, AmmoType.Nato762))
                ev.Player.SetAmmo(AmmoType.Nato762, 0);
            if (!IsAmmoTypeExcluded(ev.Player, AmmoType.Ammo12Gauge))
                ev.Player.SetAmmo(AmmoType.Ammo12Gauge, 0);
            if (!IsAmmoTypeExcluded(ev.Player, AmmoType.Ammo44Cal))
                ev.Player.SetAmmo(AmmoType.Ammo44Cal, 0);
        }
        public void OnReloadingWeapon(ReloadingWeaponEventArgs ev)
        {
            if (!_config.InfiniteAmmo.Enabled)
                return;

            Firearm firearm = ev.Player.CurrentItem as Firearm;
            if (firearm == null)
                return;

            if (_config.InfiniteAmmo.ExcludedWeapons.Contains(firearm.Type))
                return;

            if (firearm.MagazineAmmo >= firearm.MaxMagazineAmmo &&
                firearm.BarrelAmmo >= firearm.MaxBarrelAmmo)
                return;

            ev.IsAllowed = true;
            ev.Player.SetAmmo(firearm.AmmoType, GetReserveAmmo(firearm.AmmoType));
        }
        public void OnReloadedWeapon(ReloadedWeaponEventArgs ev)
        {
            if (!_config.InfiniteAmmo.Enabled)
                return;
            Firearm firearm = ev.Player.CurrentItem as Firearm;
            if (firearm == null || _config.InfiniteAmmo.ExcludedWeapons.Contains(firearm.Type))
                return;
            ev.Player.SetAmmo(firearm.AmmoType, 0);
        }

        private static ushort GetReserveAmmo(AmmoType ammoType)
        {
            switch (ammoType)
            {
                case AmmoType.Nato9:
                    return 60;
                case AmmoType.Nato556:
                    return 120;
                case AmmoType.Nato762:
                    return 100;
                case AmmoType.Ammo12Gauge:
                    return 16;
                case AmmoType.Ammo44Cal:
                    return 8;
                default:
                    return 0;
            }
        }

        private bool IsAmmoTypeExcluded(Exiled.API.Features.Player player, AmmoType ammoType)
        {
            foreach (var item in player.Items)
            {
                Firearm firearm = item as Firearm;
                if (firearm != null && firearm.AmmoType == ammoType &&
                    _config.InfiniteAmmo.ExcludedWeapons.Contains(firearm.Type))
                    return true;
            }

            return false;
        }
    }
}