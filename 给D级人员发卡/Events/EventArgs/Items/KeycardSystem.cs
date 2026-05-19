using CSARCHsPlugins.API.Enums;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace CSARCHsPlugins.Events.EventArgs.Items
{ 
    public class KeycardSystem 
    { 
        public Config Config; 
        private static readonly Dictionary
            <ItemType, KeycardPermissions> 
            PermissionCache = new Dictionary
            <ItemType, KeycardPermissions>(); 
        public KeycardSystem(Config config) 
        { 
            Config = config; 
        } 
        public void Initialize() 
        { 
            PermissionCache.Clear(); 
            Log.Info("[KeycardSystem] 已初始化"); 
        } 
        public void Shutdown() 
        { 
            PermissionCache.Clear(); 
            Log.Info("[KeycardSystem] 已关闭"); 
        } 
        public void OnInteractingDoor
            (InteractingDoorEventArgs ev) 
        { 
            if 
                (ev.IsAllowed) 
                return; 
            Item originalItem = ev.Player.CurrentItem; 
            KeycardPermissions requiredPermissions = (KeycardPermissions)(int)
                ev.Door.RequiredPermissions; 
            foreach 
                (var item in ev.Player.Items) 
            { 
                if 
                    (!item.Type.IsKeycard()) 
                    continue; 
                if 
                    (!CanCardOpenDoor(item.Type, requiredPermissions)) 
                    continue; 
                ev.Player.CurrentItem = item; 
                ev.IsAllowed = true; 
                Timing.CallDelayed(0.01f, () => 
                { 
                    if 
                        (ev.Player == null || !ev.Player.IsAlive) 
                        return; 
                    if 
                        (originalItem == null) 
                    { 
                        ev.Player.CurrentItem = null; 
                        return; 
                    } 
                    if 
                        (!ev.Player.Items.Contains(originalItem)) 
                        return; 
                    ev.Player.CurrentItem = originalItem; 
                }
                ); 
                return; 
            } 
        } 
        private static readonly 
            KeycardPermissions ValidKeycardPermissions = 
            KeycardPermissions.Checkpoints | 
            KeycardPermissions.Intercom | 
            KeycardPermissions.ContainmentLevelOne | 
            KeycardPermissions.ContainmentLevelTwo | 
            KeycardPermissions.ContainmentLevelThree | 
            KeycardPermissions.ArmoryLevelOne | 
            KeycardPermissions.ArmoryLevelTwo | 
            KeycardPermissions.ArmoryLevelThree | 
            KeycardPermissions.ExitGates |
            KeycardPermissions.ScpOverride; 
        private static bool CanCardOpenDoor
            (ItemType cardType, KeycardPermissions requiredPermissions) 
        { 
            requiredPermissions &= ValidKeycardPermissions; 
            if 
                (requiredPermissions == KeycardPermissions.None) 
            { 
                Log.Debug("[KeycardSystem] 门无需钥匙卡权限"); 
                return 
                    true; 
            } 
            if 
                (!TryGetCardPermissions(cardType, out KeycardPermissions cardPermissions)) 
            { 
                Log.Debug($"[KeycardSystem] 无法读取卡权限: {cardType}"); 
                return 
                    false; 
            } 
            Log.Debug($"[KeycardSystem] 过滤后门权限: {requiredPermissions}"); 
            Log.Debug($"[KeycardSystem] 卡权限: {cardPermissions}"); 
            return 
                (cardPermissions & requiredPermissions) == requiredPermissions; 
        } 
        private static bool TryGetCardPermissions
            (ItemType cardType, out KeycardPermissions permissions) 
        { 
            permissions = KeycardPermissions.None; 
            if 
                (PermissionCache.TryGetValue(cardType, out permissions)) 
                return true; 
            try 
            { 
                foreach 
                    (var prefab in InventoryItemLoader.AvailableItems.Values) 
                { 
                    if 
                        ((ItemType)prefab.ItemTypeId != cardType) continue; 
                        object[] details = prefab.GetType().GetProperty("Details")?.GetValue(prefab) as object[]; 
                    if 
                        (details == null) 
                        return false; 
                    foreach 
                        (var detail in details) 
                    { 
                        if 
                            (detail == null) 
                            continue; 
                        Type type = detail.GetType(); 
                        if 
                            (type.Name != "PredefinedPermsDetail") 
                            continue; 
                        int containment = (int)
                            (type.GetField("_containmentLevel", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(detail) ?? 0); 
                        int armory = (int)
                            (type.GetField("_armoryLevel", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(detail) ?? 0); 
                        int admin = (int)
                            (type.GetField("_adminLevel", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(detail) ?? 0); 
                        permissions = KeycardPermissions.None; 
                        if 
                            (containment >= 1) permissions |= KeycardPermissions.ContainmentLevelOne; 
                        if 
                            (containment >= 2) permissions |= KeycardPermissions.ContainmentLevelTwo; 
                        if 
                            (containment >= 3) permissions |= KeycardPermissions.ContainmentLevelThree; 
                        if 
                            (armory >= 1) permissions |= KeycardPermissions.ArmoryLevelOne; 
                        if 
                            (armory >= 2) permissions |= KeycardPermissions.ArmoryLevelTwo; 
                        if 
                            (armory >= 3) permissions |= KeycardPermissions.ArmoryLevelThree; 
                        if 
                            (admin >= 1) permissions |= KeycardPermissions.Checkpoints; 
                        if 
                            (admin >= 2) permissions |= KeycardPermissions.ExitGates; 
                        if 
                            (admin >= 3) permissions |= KeycardPermissions.Intercom; 
                        PermissionCache[cardType] = permissions; 
                        Log.Debug($"[KeycardSystem] {cardType} 权限解析成功: {permissions}"); 
                        return 
                            true; 
                    } 
                    return 
                        false; 
                } 
                return 
                    false; 
            } 
            catch 
                (Exception ex) 
            { 
                Log.Error($"[KeycardSystem] 读取钥匙卡权限失败 ({cardType}): {ex}"); 
                return 
                    false; 
            } 
        } 
        private static void DumpProperties
            (Type type) 
        { 
            Log.Debug($"[KeycardSystem] Dump属性: {type.FullName}"); 
            foreach 
                (var prop in type.GetProperties()) 
            { 
                Log.Debug($"[KeycardSystem] 属性: {prop.Name}"); 
            } 
        } 
    } 
}