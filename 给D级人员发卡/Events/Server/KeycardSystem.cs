using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSARCHsPlugins.Events.Server
{
    public class KeycardSystem
    {
        public Config Config;

        public KeycardSystem(Config config)
        {
            Config = config;
        }

        public void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (ev.IsAllowed)
                return;

            Item originalItem = ev.Player.CurrentItem;

            bool isGate = IsGate(ev.Door);

            Log.Debug($"[KeycardSystem] 是否Gate: {isGate}");

            foreach (var item in ev.Player.Items)
            {
                if (!item.Type.IsKeycard())
                    continue;

                bool canOpen;

                if (isGate)
                {
                    canOpen = CanOpenGate(ev.Door, item.Type);
                }
                else
                {
                    KeycardPermissions required =
                        (KeycardPermissions)(int)ev.Door.RequiredPermissions;

                    canOpen = CanCardOpenDoor(item.Type, required);
                }

                if (!canOpen)
                    continue;

                ev.Player.CurrentItem = item;
                ev.IsAllowed = true;

                Timing.CallDelayed(0.01f, () =>
                {
                    if (ev.Player == null || !ev.Player.IsAlive)
                        return;

                    if (originalItem != null && ev.Player.Items.Contains(originalItem))
                        ev.Player.CurrentItem = originalItem;
                    else
                        ev.Player.CurrentItem = null;
                });

                return;
            }
        }

        private bool IsGate(Exiled.API.Features.Doors.Door door)
        {
            string name = door.Name.ToLower();

            return name.Contains("gatea")
                || name.Contains("gateb");
        }

        private bool CanOpenGate(
            Exiled.API.Features.Doors.Door door,
            ItemType cardType)
        {
            KeycardPermissions gatePerms =
                KeycardPermissions.Checkpoints |
                KeycardPermissions.Intercom;

            if (!TryGetCardPermissions(cardType, out KeycardPermissions cardPerms))
                return false;

            return (cardPerms & gatePerms) == gatePerms;
        }

        private bool CanCardOpenDoor(
            ItemType cardType,
            KeycardPermissions requiredPermissions)
        {
            if (requiredPermissions == KeycardPermissions.None)
                return true;

            if (!TryGetCardPermissions(cardType, out KeycardPermissions cardPerms))
                return false;

            return (cardPerms & requiredPermissions) == requiredPermissions;
        }

        private static bool TryGetCardPermissions(
            ItemType cardType,
            out KeycardPermissions permissions)
        {
            permissions = KeycardPermissions.None;

            try
            {
                foreach (var prefab in InventorySystem.InventoryItemLoader.AvailableItems.Values)
                {
                    if ((ItemType)prefab.ItemTypeId != cardType)
                        continue;

                    object[] details = prefab.GetType()
                        .GetProperty("Details")?
                        .GetValue(prefab) as object[];

                    if (details == null)
                        return false;

                    foreach (var detail in details)
                    {
                        if (detail == null)
                            continue;

                        Type type = detail.GetType();

                        if (type.Name != "PredefinedPermsDetail")
                            continue;

                        int containment =
                            (int)(type.GetField("_containmentLevel",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.GetValue(detail) ?? 0);

                        int armory =
                            (int)(type.GetField("_armoryLevel",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.GetValue(detail) ?? 0);

                        int admin =
                            (int)(type.GetField("_adminLevel",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                            ?.GetValue(detail) ?? 0);

                        if (containment >= 1)
                            permissions |= KeycardPermissions.ContainmentLevelOne;
                        if (containment >= 2)
                            permissions |= KeycardPermissions.ContainmentLevelTwo;
                        if (containment >= 3)
                            permissions |= KeycardPermissions.ContainmentLevelThree;

                        if (armory >= 1)
                            permissions |= KeycardPermissions.ArmoryLevelOne;
                        if (armory >= 2)
                            permissions |= KeycardPermissions.ArmoryLevelTwo;
                        if (armory >= 3)
                            permissions |= KeycardPermissions.ArmoryLevelThree;

                        if (admin >= 1)
                            permissions |= KeycardPermissions.Checkpoints;
                        if (admin >= 2)
                            permissions |= KeycardPermissions.Intercom;
                        if (admin >= 3)
                            permissions |= KeycardPermissions.ScpOverride;

                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}