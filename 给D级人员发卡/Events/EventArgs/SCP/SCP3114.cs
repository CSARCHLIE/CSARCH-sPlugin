using CSARCHsPlugins.API.Enums;
using Exiled.API.Features;
using PlayerRoles;
using System.Linq;

namespace CSARCHsPlugins.Events.EventArgs.SCP
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

            if (global::Exiled.API.Features.Player.Count < config.SCP3114.MinPlayersToReplace)
                return;

            var scpPlayers = global::Exiled.API.Features.Player.List
            .Where(p => p.Role.Team == Team.FoundationForces
             || p.Role.Team == Team.ClassD
             || p.Role.Team == Team.ChaosInsurgency)
                .ToList();

            if (scpPlayers.Count == 0)
                return;

            var randomIndex = UnityEngine.Random.Range(0, scpPlayers.Count);
            var selectedPlayer = scpPlayers[randomIndex];

            selectedPlayer.Role.Set(RoleTypeId.Scp3114, Exiled.API.Enums.SpawnReason.RoundStart);

            if (config.SCP3114.BroadcastDuration > 0)
            {
                Map.Broadcast(config.SCP3114.BroadcastDuration, config.SCP3114.BroadcastMessage);
            }
        }
    }
}