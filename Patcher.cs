﻿using HarmonyLib;

namespace StreamlinedToolbar
{
    public static class Patcher
    {
        private const string HarmonyId = "WQ.StreamlinedToolbar";
        private static bool patched = false;

        public static void PatchAll()
        {
            if (patched) return;

            //Harmony.DEBUG = true;

            patched = true;
            var harmony = new Harmony(HarmonyId);
            harmony.PatchAll();
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);
            patched = false;
        }
    }
}
