using CSARCHsPlugins.Events.Player;
using CSARCHsPlugins.Events.Server;

namespace CSARCHsPlugins.Events.Handlers
{
    public class Handlers
    {
        private readonly Keycardgive keycardgive;
        private readonly InfiniteAmmo infiniteAmmo;
        private readonly Cleaner cleaner;
        private readonly SCP207Yes scp207Yes;
        private readonly MTFKeycardFixer mtfKeycardFixer;
        private readonly KeycardSystem keycardSystem;
        private readonly SCP3114 scp3114;
        private readonly Config config;
        public Handlers(Config config, Keycardgive keycardgive, InfiniteAmmo infiniteAmmo, Cleaner cleaner, SCP207Yes scp207Yes, MTFKeycardFixer mtfKeycardFixer, KeycardSystem keycardSystem = null, SCP3114 scp3114 = null)
        {
            this.config = config;
            this.keycardgive = keycardgive;
            this.infiniteAmmo = infiniteAmmo;
            this.cleaner = cleaner;
            this.scp207Yes = scp207Yes;
            this.mtfKeycardFixer = mtfKeycardFixer;
            this.keycardSystem = keycardSystem;
            this.scp3114 = scp3114;
        }
        public void Register()
        {
            if (keycardgive != null && config.GiveKeyCard.GiveKeycard)
                Exiled.Events.Handlers.Player.Spawned += keycardgive.OnPlayerSpawned;
            if (infiniteAmmo != null && config.InfiniteAmmo.Enabled)
            {
                Exiled.Events.Handlers.Server.RoundStarted += infiniteAmmo.OnRoundStarted;
                Exiled.Events.Handlers.Player.Spawned += infiniteAmmo.OnPlayerSpawned;
                Exiled.Events.Handlers.Player.ReloadingWeapon += infiniteAmmo.OnReloadingWeapon;
                Exiled.Events.Handlers.Player.ReloadedWeapon += infiniteAmmo.OnReloadedWeapon;
            }
            if (mtfKeycardFixer != null && config.MTFKeycard.Enabled)
                Exiled.Events.Handlers.Player.Spawned += mtfKeycardFixer.OnPlayerSpawned;
            if (cleaner != null && config.Cleaner.Enabled)
            {
                cleaner.Start();
            }
            if (scp207Yes != null && config.SCP207.Enabled)
                Exiled.Events.Handlers.Player.Hurting += scp207Yes.OnHurting;
            if (keycardSystem != null && config.KeycardSystem.Enabled)
                Exiled.Events.Handlers.Player.InteractingDoor += keycardSystem.OnInteractingDoor;
            if (scp3114 != null && config.SCP3114.Enabled)
                Exiled.Events.Handlers.Server.RoundStarted += scp3114.OnRoundStarted;
        }
        public void Unregister()
        {
            if (keycardgive != null && config.GiveKeyCard.GiveKeycard)
                Exiled.Events.Handlers.Player.Spawned -= keycardgive.OnPlayerSpawned;
            if (infiniteAmmo != null && config.InfiniteAmmo.Enabled)
            {
                Exiled.Events.Handlers.Server.RoundStarted -= infiniteAmmo.OnRoundStarted;
                Exiled.Events.Handlers.Player.Spawned -= infiniteAmmo.OnPlayerSpawned;
                Exiled.Events.Handlers.Player.ReloadingWeapon -= infiniteAmmo.OnReloadingWeapon;
                Exiled.Events.Handlers.Player.ReloadedWeapon -= infiniteAmmo.OnReloadedWeapon;
            }
            if (mtfKeycardFixer != null && config.MTFKeycard.Enabled)
                Exiled.Events.Handlers.Player.Spawned -= mtfKeycardFixer.OnPlayerSpawned;
            if (cleaner != null && config.Cleaner.Enabled)
                cleaner.Stop();
            if (scp207Yes != null && config.SCP207.Enabled)
                Exiled.Events.Handlers.Player.Hurting -= scp207Yes.OnHurting;
            if (keycardSystem != null && config.KeycardSystem.Enabled)
                Exiled.Events.Handlers.Player.InteractingDoor -= keycardSystem.OnInteractingDoor;
            if (scp3114 != null && config.SCP3114.Enabled)
                Exiled.Events.Handlers.Server.RoundStarted -= scp3114.OnRoundStarted;
        }
    }
}