﻿using HarmonyLib;
using System;

namespace StreamlinedToolbar
{
    // These patches control which assets appear in which toolbar panel.
    // They are supplemented by 'IsServiceValidForPatches'.
    //
    // See notes in IsServiceValidForPatches.

    [HarmonyPatch(typeof(GeneratedScrollPanel), "IsCategoryValid", new Type[] { typeof(BuildingInfo), typeof(bool) })]
    class IsCategoryValidForBuildingPatch
    {
        [HarmonyPostfix]
        public static void Postfix(BuildingInfo info, bool ignore, GeneratedScrollPanel __instance, ref bool __result, ref string ___m_Category)
        {
            if (ignore || !Mod.IsInGame())
            {
                return;
            }

            string categoryOverride = Utils.GetBuildingCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(PublicTransportPanel), "IsCategoryValid", new Type[] { typeof(NetInfo), typeof(bool) })]
    class IsCategoryValidForNetworkPatch2
    {
        [HarmonyPostfix]
        public static void Postfix(NetInfo info, bool ignoreCategories, ref bool __result, ref string ___m_Category)
        {
            if (ignoreCategories || !Mod.IsInGame())
            {
                return;
            }

            string categoryOverride = Utils.GetNetCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(GeneratedScrollPanel), "IsCategoryValid", new Type[] { typeof(PropInfo), typeof(bool) })]
    class IsCategoryValidForPropPatch
    {
        [HarmonyPostfix]
        public static void Postfix(PropInfo info, bool ignore, ref bool __result, ref string ___m_Category)
        {
            if (ignore || !Mod.IsInGame())
            {
                return;
            }

            string categoryOverride = Utils.GetPropCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(BeautificationPanel), "IsCategoryValid", new Type[] { typeof(PropInfo), typeof(bool) })]
    class IsCategoryValidForBeautificationPropPatch
    {
        [HarmonyPostfix]
        public static void Postfix(PropInfo info, bool ignore, ref bool __result, ref string ___m_Category)
        {
            if (ignore || !Mod.IsInGame())
            {
                return;
            }

            string categoryOverride = Utils.GetPropCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(BeautificationPanel), "IsCategoryValid", new Type[] { typeof(BuildingInfo), typeof(bool) })]
    class IsCategoryValidForBeautificationBuildingPatch
    {
        [HarmonyPostfix]
        public static void Postfix(BuildingInfo info, bool ignoreCategories, ref bool __result, ref string ___m_Category)
        {
            if (ignoreCategories || !Mod.IsInGame())
            { 
                return;
            }

            string categoryOverride = Utils.GetBuildingCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(BeautificationPanel), "IsCategoryValid", new Type[] { typeof(NetInfo), typeof(bool) })]
    class IsCategoryValidForBeautificationNetworkPatch
    {
        [HarmonyPostfix]
        public static void Postfix(NetInfo info, bool ignoreCategories, ref bool __result, ref string ___m_Category)
        {
            if (ignoreCategories || !Mod.IsInGame())
            {
                return;
            }

            string categoryOverride = Utils.GetNetCategoryOverride(info);

            if (categoryOverride != null)
            {
                __result = ___m_Category == categoryOverride;
            }
        }
    }

    [HarmonyPatch(typeof(BeautificationPanel), "IsCategoryValid", new Type[] { typeof(TreeInfo), typeof(bool) })]
    class IsCategoryValidForTreePatch
    {
        [HarmonyPostfix]
        public static void Postfix(TreeInfo info, bool ignore, ref bool __result, ref string ___m_Category)
        {
            if (ignore || !Mod.IsInGame())
            {
                return;
            }

            __result = ___m_Category == Utils.GetTreeCategoryOverride(info);
        }
    }
}