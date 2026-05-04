using Exiled.API.Features;
using Exiled.API.Features.Pickups;
using MEC;
using System.Collections.Generic;

namespace CSARCHsPlugins.Events.Server
{
    public class Cleaner
    {
        public Config Config;
        private CoroutineHandle _loop;
        public Cleaner(Config config)
        {
            Config = config;
        }
        public void Start()
        {
            if (!Config.Cleaner.Enabled)
                return;
            _loop = Timing.RunCoroutine(CleanLoop());
            Log.Info("[Cleaner] 已启动");
        }
        public void Stop()
        {
            if (Timing.IsRunning(_loop))
                Timing.KillCoroutines(_loop);
            Log.Info("[Cleaner] 已停止");
        }
        private IEnumerator<float> CleanLoop()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(Config.Cleaner.Interval);
                CleanMap();
            }
        }
        public void CleanMap()
        {
            int rag = 0;
            int ammo = 0;
            if (Config.Cleaner.CleanRagdolls)
            {
                foreach (var r in Ragdoll.List)
                {
                    if (r.Owner == null)
                        continue;
                    r.Destroy();
                    rag++;
                }
            }
            if (Config.Cleaner.CleanAmmo)
            {
                foreach (var p in Pickup.List)
                {
                    var ammoPickup = p as Exiled.API.Features.Pickups.AmmoPickup;
                    if (ammoPickup == null)
                        continue;
                    if (ammoPickup.InUse)
                        continue;
                    ammoPickup.Destroy();
                    ammo++;
                }
            }
            string msg = Config.Cleaner.BroadcastMessage.Replace("{rag}", rag.ToString()).Replace("{ammo}", ammo.ToString());
            Log.Info($"[Cleaner] 清理完成 | 尸体:{rag} 弹药:{ammo}");
            try
            {
                Map.Broadcast(Config.Cleaner.BroadcastDuration, msg);
            }
            catch
            {
                Log.Error("[Cleaner] 广播失败，请检查广播消息模板是否正确");
            }
        }
    }
}