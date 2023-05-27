using HarmonyLib;
using System;
using UnityEngine;

namespace StreamlinedToolbar
{
    // These patches control which assets appear in which toolbar panel.
    // They are supplemented by 'IsCategoryValidForPatches'.
    //
    // Notes:
    // A "service" is generally higher level, and typically shared by all panels within a group
    // (a group is e.g. "Parks and plazas", a panel is e.g. "Zoo"). 
    // A "category" is generally lower level, and typically unique to each panel.
    // In practice, the CS source code is full of different panel-specific customisations (to which we add our own),
    // which make this distinction a bit blured. 
    // 

    [HarmonyPatch(typeof(GeneratedScrollPanel), "IsServiceValid", new Type[] { typeof(BuildingInfo) })]
    class IsServiceValidForBuildingPatch
    {
        [HarmonyPostfix]
        public static void Postfix(BuildingInfo info, GeneratedScrollPanel __instance, ref bool __result, ref string ___m_Category)
        {
            if (!Mod.IsInGame())
            {
                return;
            }

            if (__instance is RoadsPanel && info.GetService() == ItemClass.Service.Beautification)
            {
                // Some of these are car parks
                __result = true;
            }
        }
    }

    [HarmonyPatch(typeof(PublicTransportPanel), "IsServiceValid", new Type[] { typeof(NetInfo) })]
    class IsServiceValidForNetPatch
    {
        [HarmonyPostfix]
        public static void Postfix(NetInfo info, ref bool __result, ref string ___m_Category)
        {
            if (!Mod.IsInGame())
            {
                return;
            }

            if (info.category == "PublicTransportModderPack")
            {
                // Normally, the game excludes DLC train tracks from the "Trains" tab, and places them in the CCP tab.
                if ((info.m_vehicleTypes & VehicleInfo.VehicleType.Train) != 0)
                {
                    __result = (___m_Category == "PublicTransportTrain");
                }
            }
        }
    }

    [HarmonyPatch(typeof(BeautificationPanel), "IsServiceValid", new Type[] { typeof(PropInfo) })]
    class IsServiceValidForPropPatch
    {
        [HarmonyPostfix]
        public static void Postfix(PropInfo info, ref bool __result, ref string ___m_Category)
        {
            if (!Mod.IsInGame())
            {
                return;
            }

            if (ToolsModifierControl.toolController.m_mode.IsFlagSet(ItemClass.Availability.Game))
            {
                if (info.category == "BeautificationModderPack")
                {
                    // Normally, the game places these props in the CCP tab. Move them to the Props tab.
                    // The filtering that restricts them to the 'Props' tab occurs in BeautificationPanel.IsCategoryValid.
                    __result = true;
                }
            }
        }
    }
}
