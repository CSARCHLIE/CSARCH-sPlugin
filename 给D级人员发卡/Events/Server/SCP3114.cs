using Exiled.API.Features;
using PlayerRoles;
using System.Linq;

namespace CSARCHsPlugins.Events.Server
{
    public class SCP3114
    {
        private readonly Config config;

        public SCP3114(Config config)
        {
            this.config = config;
        }

        public void OnRoundStarted()
        {
            if (!config.SCP3114.Enabled)
                return;

            if (global::Exiled.API.Features.Player.Count <= config.SCP3114.MinPlayersToReplace)
                return;

            var scpPlayers = global::Exiled.API.Features.Player.List.Where(p => p.Role.Team != Team.Dead).ToList();

            if (scpPlayers.Count == 0)
                return;

            var randomIndex = UnityEngine.Random.Range(0, scpPlayers.Count);
            var selectedPlayer = scpPlayers[randomIndex];

            selectedPlayer.Role.Set(RoleTypeId.Scp3114);

            if (config.SCP3114.BroadcastDuration > 0)
            {
                foreach (var player in global::Exiled.API.Features.Player.List)
                {
                    player.Broadcast(config.SCP3114.BroadcastDuration, config.SCP3114.BroadcastMessage);
                }
            }
        }
    }
}