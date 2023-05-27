using ColossalFramework;
using HarmonyLib;
using System;
using System.Reflection;
using static GeneratedGroupPanel;

namespace StreamlinedToolbar
{
    // These patches skip creation of empty toolbar tabs.
    // (Only needed for those tabs which are created "forcefully". In most cases tabs are created based on there being at least 1 item in a given category.)

    [HarmonyPatch(typeof(BeautificationGroupPanel), "AddCustomGroups")]
    class AddCustomGroupsPatch
    {
        // Need access to this protected method
        private static MethodInfo createGroupMethod = AccessTools.Method(typeof(GeneratedGroupPanel), "CreateGroupInfo", new Type[] { typeof(string), typeof(PrefabInfo) });

        [HarmonyPrefix]
        static bool Prefix(PoolList<GroupInfo> groupItems, BeautificationGroupPanel __instance)
        {
            // Same as default implementation, except we skip P&P Plazas (we empty that tab) and Hotels (it seems to be created OK from props).

            // These two need to be created explicitly, but not sure why:
            if (!ToolsModifierControl.toolController.m_mode.IsFlagSet(ItemClass.Availability.AssetEditor))
            {
                groupItems.Add((GroupInfo)createGroupMethod.Invoke(__instance, new[] { "BeautificationProps", null }));
                if (SteamHelper.IsDLCOwned(SteamHelper.DLC.PlazasAndPromenadesDLC))
                {
                    groupItems.Add((GroupInfo)createGroupMethod.Invoke(__instance, new[] { "BeautificationPedestrianZoneEssentials", null }));
                }
            }

            return false; // skip default implementation
        }
    }

    [HarmonyPatch(typeof(GeneratedGroupPanel), "CreateGroupItem")]
    class CreateGroupItemPatch
    {
        [HarmonyPrefix]
        static bool Prefix(GroupInfo info, string localeID)
        {
            if (Mod.IsInGame())
            {
                if (info.name == "MonumentModderPack" || info.name == "LandscapingModderPackTrees")
                {
                    return false; // skip creation of these tab
                }
            }

            return true;
        }
    }
}
