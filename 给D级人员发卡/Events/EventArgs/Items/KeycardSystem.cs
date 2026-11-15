using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Linq;

namespace CSARCHsPlugins.Events.EventArgs.Items
{
    /// <summary>
    /// Allows a player to use any qualifying keycard in their inventory without holding it.
    /// </summary>
    public class KeycardSystem
    {
        public KeycardSystem(Config config)
        {
            Config = config;
        }

        public Config Config { get; }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.IsAllowed || ev.Door.IsLocked)
                return;

            Keycard keycard = FindMatchingKeycard(ev.Player, ev.Door.KeycardPermissions);

            if (keycard == null)
                return;

            Item originalItem = ev.Player.CurrentItem;
            ev.Player.CurrentItem = keycard;
            ev.IsAllowed = true;

            Timing.CallDelayed(0.01f, () =>
            {
                if (ev.Player == null || !ev.Player.IsAlive)
                    return;

                ev.Player.CurrentItem = originalItem != null && ev.Player.Items.Contains(originalItem)
                    ? originalItem
                    : null;
            });
        }

        public void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (ev.IsAllowed)
                return;

            if (FindMatchingKeycard(ev.Player, ev.InteractingChamber.RequiredPermissions) != null)
                ev.IsAllowed = true;
        }

        private static Keycard FindMatchingKeycard(Exiled.API.Features.Player player, KeycardPermissions requiredPermissions)
        {
            return player.Items
                .OfType<Keycard>()
                .FirstOrDefault(card => HasRequiredPermissions(card.Permissions, requiredPermissions));
        }

        private static bool HasRequiredPermissions(
            KeycardPermissions cardPermissions,
            KeycardPermissions requiredPermissions)
        {
            return (cardPermissions & requiredPermissions) == requiredPermissions;
        }
    }
}
