using CSARCHsPlugins.API.Enums;
using CSARCHsPlugins.API.UI;
using CSARCHsPlugins.Events.EventArgs.Items;
using CSARCHsPlugins.Events.EventArgs.Player;
using CSARCHsPlugins.Events.EventArgs.SCP;
using CSARCHsPlugins.Events.EventArgs.Server;
using Exiled.Events.Handlers;

namespace CSARCHsPlugins.Events.Handlers
{
    public class EventRegistrar
    {
        private readonly Keycardgive keycardgive;
        private readonly InfiniteAmmo infiniteAmmo;
        private readonly Cleaner cleaner;
        private readonly SCP207Yes scp207Yes;
        private readonly MTFKeycardFixer mtfKeycardFixer;
        private readonly KeycardSystem keycardSystem;
        private readonly SCP3114 scp3114;
        private readonly PlayerNameDisplay playernameDisplay;
        private readonly Config config;
        public EventRegistrar
            (
            Config config, 
            Keycardgive keycardgive, 
            InfiniteAmmo infiniteAmmo, 
            Cleaner cleaner, 
            SCP207Yes scp207Yes, 
            MTFKeycardFixer mtfKeycardFixer, 
            KeycardSystem keycardSystem, 
            SCP3114 scp3114, 
            PlayerNameDisplay playernameDisplay
            = null
            )
        {
            this.config = config;
            this.keycardgive = keycardgive;
            this.infiniteAmmo = infiniteAmmo;
            this.cleaner = cleaner;
            this.scp207Yes = scp207Yes;
            this.mtfKeycardFixer = mtfKeycardFixer;
            this.keycardSystem = keycardSystem;
            this.scp3114 = scp3114;
            this.playernameDisplay = playernameDisplay;
        }
        public void Register()
        {
            if (keycardgive != null && config.GiveKeyCard.GiveKeycard)
                Player.Spawned += keycardgive.OnPlayerSpawned;
            if (infiniteAmmo != null && config.InfiniteAmmo.Enabled)
            {
                Server.RoundStarted += infiniteAmmo.OnRoundStarted;
                Player.Spawned += infiniteAmmo.OnPlayerSpawned;
                Player.ReloadingWeapon += infiniteAmmo.OnReloadingWeapon;
                Player.ReloadedWeapon += infiniteAmmo.OnReloadedWeapon;
            }
            if (mtfKeycardFixer != null && config.MTFKeycard.Enabled)
                Player.Spawned += mtfKeycardFixer.OnPlayerSpawned;
            if (cleaner != null && config.Cleaner.Enabled)
                Server.RoundStarted += cleaner.Start;
            if (scp207Yes != null && config.SCP207.Enabled)
                Player.Hurting += scp207Yes.OnHurting;
            if (keycardSystem != null && config.KeycardSystem.Enabled)
                Player.InteractingDoor += keycardSystem.OnInteractingDoor;
            if (scp3114 != null && config.SCP3114.Enabled)
                Server.RoundStarted += scp3114.OnRoundStarted;
            if (playernameDisplay != null && config.PlayerNameDisplay.Enabled)
            {
                Player.Verified += playernameDisplay.OnVerified;
                Player.Left += playernameDisplay.OnLeft;
            }

        }
        public void Unregister()
        {
            if (keycardgive != null && config.GiveKeyCard.GiveKeycard)
                Player.Spawned -= keycardgive.OnPlayerSpawned;
            if (infiniteAmmo != null && config.InfiniteAmmo.Enabled)
            {
                Server.RoundStarted -= infiniteAmmo.OnRoundStarted;
                Player.Spawned -= infiniteAmmo.OnPlayerSpawned;
                Player.ReloadingWeapon -= infiniteAmmo.OnReloadingWeapon;
                Player.ReloadedWeapon -= infiniteAmmo.OnReloadedWeapon;
            }
            if (mtfKeycardFixer != null && config.MTFKeycard.Enabled)
                Player.Spawned -= mtfKeycardFixer.OnPlayerSpawned;
            if (cleaner != null && config.Cleaner.Enabled)
                Server.RoundStarted -= cleaner.Stop;
            if (scp207Yes != null && config.SCP207.Enabled)
                Player.Hurting -= scp207Yes.OnHurting;
            if (keycardSystem != null && config.KeycardSystem.Enabled)
                Player.InteractingDoor -= keycardSystem.OnInteractingDoor;
            if (scp3114 != null && config.SCP3114.Enabled)
                Server.RoundStarted -= scp3114.OnRoundStarted;
            if (playernameDisplay != null && config.PlayerNameDisplay.Enabled)
            {
                Player.Verified -= playernameDisplay.OnVerified;
                Player.Left -= playernameDisplay.OnLeft;
            }
        }
    }
}