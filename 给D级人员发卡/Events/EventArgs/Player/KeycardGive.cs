using CSARCHsPlugins.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
namespace CSARCHsPlugins.Events.EventArgs.Player
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
                    if (Config.GiveKeyCard.BroadcastEnabled)
                    {
                        string message = Config.GiveKeyCard.BroadcastMessageTemplate;
                        if (string.IsNullOrEmpty(message))
                            message = Config.GiveKeyCard.BroadcastMessage;
                        message = message
                            .Replace("{prefix}", Config.GiveKeyCard.BroadcastPrefix)
                            .Replace("{player}", ev.Player.Nickname);
                        ev.Player.Broadcast(Config.GiveKeyCard.BroadcastDuration, message);
                    }
                }
            }
        }
    }
}