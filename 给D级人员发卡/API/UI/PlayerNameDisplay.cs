using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using MEC;
using PlayerRoles;

namespace CSARCHsPlugins.API.UI
{
    public class PlayerNameDisplay
    {
        private readonly Config _config;

        public PlayerNameDisplay(Config config)
        {
            _config = config;
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

            var display = PlayerDisplay.Get(player);

            if (display.HasHint("player_name_display"))
                return;

            HintServiceMeow.Core.Models.Hints.Hint hint = new HintServiceMeow.Core.Models.Hints.Hint
            {
                Id = "player_name_display",

                Alignment = HintAlignment.Center,

                XCoordinate = 0,
                YCoordinate = 930,

                SyncSpeed = HintSyncSpeed.Fast,

                AutoText = _ =>
                {
                    if (player == null || !player.IsConnected)
                        return string.Empty;

                    if (player.Role.Type == RoleTypeId.None)
                        return string.Empty;

                    return $"<color=#FFFFFF>{player.Nickname}</color>";
                }
            };

            display.AddHint(hint);
        }

        private void RemoveDisplay(Player player)
        {
            if (player == null)
                return;

            var display = PlayerDisplay.Get(player);

            if (display.HasHint("player_name_display"))
                display.RemoveHint("player_name_display");
        }
    }
}