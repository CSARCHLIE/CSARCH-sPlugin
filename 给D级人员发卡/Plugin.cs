using CSARCHsPlugins.API.Enums;
using CSARCHsPlugins.API.UI;
using CSARCHsPlugins.Events.EventArgs.Items;
using CSARCHsPlugins.Events.EventArgs.Player;
using CSARCHsPlugins.Events.EventArgs.SCP;
using CSARCHsPlugins.Events.EventArgs.Server;
using CSARCHsPlugins.Events.Handlers;
using Exiled.API.Features;
using System;
namespace CSARCHsPlugins
{
    public class Plugin : Plugin<Config>
    {
        public override string Author => "CSARCH";
        public override string Name => "CSARCH's Plugin";
        public override string Prefix => "CSARCH's Plugin";
        public override Version Version => new Version(7, 1, 0);
        public override Version RequiredExiledVersion => new Version(9, 13, 2);
        private Keycardgive keycardgive;
        private InfiniteAmmo infiniteAmmo;
        private Cleaner cleaner;
        private KeycardSystem keycardSystem;
        private SCP207Yes scp207Yes;
        private EventRegistrar handlers;
        private PlayerNameDisplay playernameDisplay;
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
                Log.Info($"[{SystemType.KeycardDistribution}] Loaded");
            }

            if (Config.InfiniteAmmo.Enabled)
            {
                infiniteAmmo = new InfiniteAmmo(Config);
                Log.Info($"[{SystemType.InfiniteAmmo}] Loaded");
            }

            if (Config.Cleaner.Enabled)
            {
                cleaner = new Cleaner(Config);
                Log.Info($"[{SystemType.Cleaner}] Loaded");
            }

            if (Config.SCP207.Enabled)
            {
                scp207Yes = new SCP207Yes(Config);
                Log.Info($"[{SystemType.SCP207System}] Loaded");
            }

            if (Config.MTFKeycard.Enabled)
            {
                mtfKeycardFixer = new MTFKeycardFixer(Config);
                Log.Info($"[{SystemType.MTFKeycardFixer}] Loaded");
            }

            if (Config.KeycardSystem.Enabled)
            {
                keycardSystem = new KeycardSystem(Config);
                Log.Info($"[{SystemType.KeycardSystem}] Loaded");
            }

            if (Config.SCP3114.Enabled)
            {
                scp3114 = new SCP3114(Config);
                Log.Info($"[{SystemType.SCP3114Generator}] Loaded");
            }

            if (Config.PlayerNameDisplay.Enabled) 
            {
                playernameDisplay = new PlayerNameDisplay(Config);
                Log.Info($"[{SystemType.PlayerNameDisplay}] Loaded");
            }

            Log.Warn("CSARCH's Plugin initialization completed.");
        
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

            handlers = new EventRegistrar
                (
                Config, 
                keycardgive, 
                infiniteAmmo, 
                cleaner, 
                scp207Yes, 
                mtfKeycardFixer, 
                keycardSystem, 
                scp3114,
                playernameDisplay
                );
            handlers.Register();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Log.Info("插件已卸载，感谢使用");
            handlers?.Unregister();
            base.OnDisabled();
        }
    }
}