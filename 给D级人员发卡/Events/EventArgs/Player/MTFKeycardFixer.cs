using CSARCHsPlugins.API.Enums;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System.Linq;

namespace CSARCHsPlugins.Events.EventArgs.Player
{
    public class MTFKeycardFixer
    {
        private readonly Config _config;

        public MTFKeycardFixer(Config config)
        { 
            _config = config; 
        }
        public void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            if (ev.Player.Role.Type != RoleTypeId.NtfPrivate)
                return;
            var items = ev.Player.Items.ToArray();
            foreach (var it in items)
            {
                if (it.Type == ItemType.KeycardMTFOperative)
                {
                    ev.Player.RemoveItem(it);
                    ev.Player.AddItem(ItemType.KeycardMTFPrivate);
                }
            }
        }
    }
}