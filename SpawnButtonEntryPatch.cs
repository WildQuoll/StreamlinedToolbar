using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace StreamlinedToolbar
{
    // This patch assigns icons and tooltips to our custom tabs.
    [HarmonyPatch(typeof(GeneratedGroupPanel), "SpawnButtonEntry",
        new Type[] { typeof(UITabstrip), typeof(string), typeof(string), typeof(bool),
                     typeof(string), typeof(string), typeof(string), typeof(bool), typeof(bool) })]
    class SpawnButtonEntryPatch
    {
        private static Dictionary<string, Dictionary<string, string>> translations = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "DistrictSpecializationOffice", new Dictionary<string, string>
                {
                    {"en", "Office and High-Rise Buildings"},
                    {"de", "Büro- und Hochhäuser"},
                    {"es", "Edificios de oficinas y de gran altura"},
                    {"fr", "Bureaux et immeubles de grande hauteur"},
                    {"ko", "사무실 및 고층 건물"},
                    {"pl", "Biurowce i wieżowce"},
                    {"pt", "Edifícios de escritórios e arranha-céus"},
                    {"ru", "Офисные и многоэтажные здания"},
                    {"zh", "办公楼和高层建筑"},
                }
            },
            {
                "DistrictSpecializationCommercial", new Dictionary<string, string>
                {
                    {"en", "Retail Buildings"},
                    {"de", "Einzelhandelsgebäude"},
                    {"es", "Edificios comerciales"},
                    {"fr", "Bâtiments commerciaux"},
                    {"ko", "소매 건물"},
                    {"pl", "Budynki handlowe"},
                    {"pt", "Edifícios comerciais"},
                    {"ru", "Торговые здания"},
                    {"zh", "零售楼宇"},
                }
            },
            {
                "BeautificationOthers", new Dictionary<string, string>
                {
                    {"en", "Sports Grounds"},
                    {"de", "Freiluft-Sportstätten"},
                    {"es", "Instalaciones deportivas"},
                    {"fr", "Installations sportives"},
                    {"ko", "야외 스포츠 시설"},
                    {"pl", "Obiekty sportowe"},
                    {"pt", "Instalações desportivas"},
                    {"ru", "Спортивные площадки"},
                    {"zh", "户外运动设施"},
                }
            },
            {
                "MonumentFootball", new Dictionary<string, string>
                {
                    {"en", "Sports Grounds"},
                    {"de", "Freiluft-Sportstätten"},
                    {"es", "Instalaciones deportivas"},
                    {"fr", "Installations sportives"},
                    {"ko", "야외 스포츠 시설"},
                    {"pl", "Obiekty sportowe"},
                    {"pt", "Instalações desportivas"},
                    {"ru", "Спортивные площадки"},
                    {"zh", "户外运动设施"},
                }
            }
        };

        private static void SetTooltip(string category, string fallbackLocaleID, ref UIButton button)
        {
            string language = SingletonLite<LocaleManager>.instance.language;
            if (translations.ContainsKey(category) && translations[category].ContainsKey(language))
            {
                button.tooltip = translations[category][language];
            }
            else
            {
                // Fallback in case we don't have a translation. These shoul be good enough
                button.tooltip = Locale.Get(fallbackLocaleID, category);
            }
        }

        private static void SetIconAndTooltip(string spriteBase, string category, string fallbackLocaleID, ref UIButton button)
        {
            string buttonBaseName = spriteBase + category;
            button.normalFgSprite = buttonBaseName;
            button.focusedFgSprite = buttonBaseName + "Focused";
            button.hoveredFgSprite = buttonBaseName + "Hovered";
            button.pressedFgSprite = buttonBaseName + "Pressed";
            button.disabledFgSprite = buttonBaseName + "Disabled";

            SetTooltip(category, fallbackLocaleID, ref button);
        }

        [HarmonyPostfix]
        static void Postfix(UITabstrip strip, string name, string category,
                            bool isDefaultCategory, string localeID, string unlockText,
                            string spriteBase, bool enabled, bool forceFillContainer,
                            ref UIButton __result)
        {
            switch (category)
            {
                // Custom tabs
                case "BeautificationWaterStructures":
                    SetIconAndTooltip(spriteBase, "LandscapingWaterStructures", localeID, ref __result);
                    break;
                case "MonumentsCommercial":
                    SetIconAndTooltip(spriteBase, "DistrictSpecializationCommercial", "DISTRICT_CATEGORY", ref __result);
                    break;
                case "MonumentsOffice":
                    SetIconAndTooltip(spriteBase, "DistrictSpecializationOffice", "DISTRICT_CATEGORY", ref __result);
                    break;

                // Renamed vanilla tabs
                case "BeautificationOthers":
                case "MonumentFootball":
                    SetTooltip(category, localeID, ref __result);
                    break;
                default:
                    return;
            }
        }
    }
}
