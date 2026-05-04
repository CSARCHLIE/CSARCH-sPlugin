using Exiled.Events.EventArgs.Player;
using PlayerRoles;
namespace CSARCHsPlugins.Events.Player
{
    public class Keycardgive
    {
        public Config Config;
        public Keycardgive(Config config)
        {
            Config = config;
        }
        public void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            if (ev.Player.Role.Type != RoleTypeId.ClassD) return;
            if (ev.Player.Role.Type == RoleTypeId.ClassD)
            {
                if (Config.IsEnabled)
                {
                    if (Config.GiveKeyCard.ClearInventory)
                    {
                        ev.Player.ClearInventory();
                    }
                    if (Config.GiveKeyCard.GiveKeycard)
                    {
                        ev.Player.AddItem(Config.GiveKeyCard.KeycardType);
                    }
                    ev.Player.Broadcast(Config.GiveKeyCard.BroadcastDuration, Config.GiveKeyCard.BroadcastMessage);
                }
            }
        }
    }
}