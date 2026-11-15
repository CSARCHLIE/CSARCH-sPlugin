using System;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Utilities;
using MEC;
using PlayerRoles;

namespace CSARCHsPlugins.API.UI
{
    public class PlayerNameDisplay
    {
        private const string HintId = "player_name_display";
        private readonly Config _config;

        public PlayerNameDisplay(Config config)
        {
            _config = config;
        }

        public void Start()
        {
            Timing.CallDelayed(1f, () =>
            {
                foreach (Player player in Player.List.Where(player => player.IsConnected))
                    CreateDisplay(player);
            });
        }

        public void Stop()
        {
            foreach (Player player in Player.List)
                RemoveDisplay(player);
        }

        public void OnVerified(VerifiedEventArgs ev)
        {
            Timing.CallDelayed(1f, () =>
            {
                if (ev.Player != null && ev.Player.IsConnected)
                    CreateDisplay(ev.Player);
            });
        }

        public void OnLeft(LeftEventArgs ev)
        {
            RemoveDisplay(ev.Player);
        }

        private void CreateDisplay(Player player)
        {
            if (player == null || !player.IsConnected)
                return;

            PlayerDisplay display = PlayerDisplay.Get(player);
            if (display.HasHint(HintId))
                return;

            HintServiceMeow.Core.Models.Hints.Hint hint = new HintServiceMeow.Core.Models.Hints.Hint
            {
                Id = HintId,
                Alignment = HintAlignment.Center,
                XCoordinate = _config.PlayerNameDisplay.XPosition,
                YCoordinate = _config.PlayerNameDisplay.YPosition,
                SyncSpeed = HintSyncSpeed.Fast,
                AutoText = _ => GetPlayerName(player),
            };

            display.AddHint(hint);
        }

        private void RemoveDisplay(Player player)
        {
            if (player == null)
                return;

            PlayerDisplay display = PlayerDisplay.Get(player);
            if (display.HasHint(HintId))
                display.RemoveHint(HintId);
        }

        private string GetPlayerName(Player player)
        {
            if (player == null || !player.IsConnected || player.Role.Type == RoleTypeId.None)
                return string.Empty;

            string color = string.IsNullOrWhiteSpace(_config.PlayerNameDisplay.Color)
                ? "#FFFFFF"
                : _config.PlayerNameDisplay.Color;
            return $"<color={color}>{player.Nickname}</color>";
        }
    }
}
