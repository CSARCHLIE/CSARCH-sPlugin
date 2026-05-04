using CSARCHsPlugins.Events.Handlers;
using CSARCHsPlugins.Events.Player;
using CSARCHsPlugins.Events.Server;
using Exiled.API.Features;
using System;
namespace CSARCHsPlugins
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "CSARCH";
        public override string Name => "CSARCH's Plugin";
        public override string Prefix => "CSARCH's Plugin";
        public override Version Version => new Version(7, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 13, 2);
        private Keycardgive keycardgive;
        private InfiniteAmmo infiniteAmmo;
        private Cleaner cleaner;
        private KeycardSystem keycardSystem;
        private SCP207Yes scp207Yes;
        private Handlers handlers;

        private MTFKeycardFixer mtfKeycardFixer;
        private SCP3114 scp3114;

        public override void OnEnabled()
        {
            Log.Info($@"
╔══════════════════════════════════════════════════════╗
                     CSARCH's Plugin
╠══════════════════════════════════════════════════════╣
  Version : {Version}
  Author  : {Author}
  Loaded  : {DateTime.Now:yyyy-MM-dd HH:mm:ss}
╠══════════════════════════════════════════════════════╣
");

            if (!Config.IsEnabled)
            {
                Log.Warn("Plugin disabled by configuration.");
                base.OnEnabled();
                return;
            }

            if (Config.GiveKeyCard.GiveKeycard)
            {
                keycardgive = new Keycardgive(Config);
                Log.Info("[Module] Auto Keycard Loaded");
            }

            if (Config.InfiniteAmmo.Enabled)
            {
                infiniteAmmo = new InfiniteAmmo(Config);
                Log.Info("[Module] Infinite Ammo Loaded");
            }

            if (Config.Cleaner.Enabled)
            {
                cleaner = new Cleaner(Config);
                Log.Info("[Module] Cleaner Loaded");
            }

            if (Config.SCP207.Enabled)
            {
                scp207Yes = new SCP207Yes(Config);
                Log.Info("[Module] SCP-207 Loaded");
            }

            if (Config.MTFKeycard.Enabled)
            {
                mtfKeycardFixer = new MTFKeycardFixer(Config);
                Log.Info("[Module] MTF Keycard Fix Loaded");
            }

            if (Config.KeycardSystem.Enabled)
            {
                keycardSystem = new KeycardSystem(Config);
                Log.Info("[Module] Sensor Keycard System Loaded");
            }

            if (Config.SCP3114.Enabled)
            {
                scp3114 = new SCP3114(Config);
                Log.Info("[Module] SCP-3114 Replacement Loaded");
            }

            Log.Info("CSARCH's Plugin initialization completed.");
        
        Log.Info(@"
Welcome use:
   ██████╗███████╗ █████╗ ██████╗  ██████╗██╗  ██╗
  ██╔════╝██╔════╝██╔══██╗██╔══██╗██╔════╝██║  ██║
  ██║     ███████╗███████║██████╔╝██║     ███████║
  ██║     ╚════██║██╔══██║██╔══██╗██║     ██╔══██║
  ╚██████╗███████║██║  ██║██║  ██║╚██████╗██║  ██║
   ╚═════╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝

        Copyright ©  2025-2026 CSARCH. All rights reserved.
╚══════════════════════════════════════════════════════╝
");

            handlers = new Handlers(Config, keycardgive, infiniteAmmo, cleaner, scp207Yes, mtfKeycardFixer, keycardSystem, scp3114);
            handlers.Register();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Log.Info("插件已卸载，感谢使用");
            if (handlers != null)
                handlers.Unregister();
            base.OnDisabled();
        }
    }
}