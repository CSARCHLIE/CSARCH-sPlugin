using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace CSARCHsPlugins.Events.EventArgs.Player
{
    public class Keycardgive
    {
        private readonly Config config;

        public Keycardgive(Config config)
        {
            this.config = config;
        }

        public void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            if (!config.IsEnabled || ev.Player.Role.Type != RoleTypeId.ClassD)
                return;

            if (config.GiveKeyCard.ClearInventory)
                ev.Player.ClearInventory();

            if (config.GiveKeyCard.GiveKeycard)
                ev.Player.AddItem(config.GiveKeyCard.KeycardType);

            if (!config.GiveKeyCard.BroadcastEnabled)
                return;

            string message = config.GiveKeyCard.BroadcastMessageTemplate;
            if (string.IsNullOrEmpty(message))
                message = config.GiveKeyCard.BroadcastMessage;

            message = message
                .Replace("{prefix}", config.GiveKeyCard.BroadcastPrefix)
                .Replace("{player}", ev.Player.Nickname);
            ev.Player.Broadcast(config.GiveKeyCard.BroadcastDuration, message);
        }
    }
}