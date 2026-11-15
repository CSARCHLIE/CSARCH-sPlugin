using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CSARCHsPlugins.Events.EventArgs.SCP
{
    public class SCPHealthPlus
    {
        private const double RecoveryDelaySeconds = 30d;
        private const float RecoveryIntervalSeconds = 0.2f;
        private const float MovementThresholdSquared = 0.01f;

        private readonly Dictionary<string, RecoveryState> recoveryStates = new Dictionary<string, RecoveryState>();
        private CoroutineHandle recoveryLoop;

        public void Start()
        {
            recoveryLoop = Timing.RunCoroutine(RecoveryLoop());
        }

        public void Stop()
        {
            if (Timing.IsRunning(recoveryLoop))
                Timing.KillCoroutines(recoveryLoop);

            recoveryStates.Clear();
        }

        public void OnHurt(HurtEventArgs ev)
        {
            if (ev.Player.Role.Team != Team.SCPs)
                return;

            RecoveryState state = GetOrCreateState(ev.Player, DateTime.UtcNow);
            state.LastDamageAt = DateTime.UtcNow;
        }

        private IEnumerator<float> RecoveryLoop()
        {
            while (true)
            {
                DateTime now = DateTime.UtcNow;
                HashSet<string> activePlayers = new HashSet<string>();

                foreach (global::Exiled.API.Features.Player player in global::Exiled.API.Features.Player.List.ToList())
                {
                    if (!player.IsConnected || !player.IsAlive || player.Role.Team != Team.SCPs)
                        continue;

                    activePlayers.Add(player.UserId);
                    RecoveryState state = GetOrCreateState(player, now);
                    Vector3 position = player.Position;

                    if ((position - state.LastPosition).sqrMagnitude > MovementThresholdSquared)
                        state.StationarySince = now;

                    state.LastPosition = position;

                    if (now - state.LastDamageAt < TimeSpan.FromSeconds(RecoveryDelaySeconds) ||
                        now - state.StationarySince < TimeSpan.FromSeconds(RecoveryDelaySeconds) ||
                        player.Health >= player.MaxHealth)
                    {
                        continue;
                    }

                    player.Health = Math.Min(player.Health + 1f, player.MaxHealth);
                }

                foreach (string userId in recoveryStates.Keys.Where(userId => !activePlayers.Contains(userId)).ToList())
                    recoveryStates.Remove(userId);

                yield return Timing.WaitForSeconds(RecoveryIntervalSeconds);
            }
        }

        private RecoveryState GetOrCreateState(global::Exiled.API.Features.Player player, DateTime now)
        {
            if (!recoveryStates.TryGetValue(player.UserId, out RecoveryState state))
            {
                state = new RecoveryState(now, player.Position);
                recoveryStates.Add(player.UserId, state);
            }

            return state;
        }

        private sealed class RecoveryState
        {
            public DateTime LastDamageAt { get; set; }
            public DateTime StationarySince { get; set; }
            public Vector3 LastPosition { get; set; }

            public RecoveryState(DateTime now, Vector3 position)
            {
                LastDamageAt = now;
                StationarySince = now;
                LastPosition = position;
            }
        }
    }
}
