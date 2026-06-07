using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Localization;
using ModMenu.Settings;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityModManagerNet;
using Menu = ModMenu.ModMenu;

namespace PhoenixScalingCantrips
{
    public class Settings
    {
        private static readonly string RootKey = "psc.settings";
        private static readonly string RootStringKey = "PSC.Settings";
        private const string VerboseLoggingKey = "enable-verbose-logs";

        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Settings));

        internal static bool IsEnabled(string key)
        {
            return Menu.GetSettingValue<bool>(GetKey(key));
        }

        internal static bool IsTTTBaseEnabled()
        {
            return UnityModManager.modEntries.Where(
                mod => mod.Info.Id.Equals("TabletopTweaks-Base") && mod.Enabled && !mod.ErrorOnLoading)
              .Any();
        }
        internal static bool IsCharOpPlusEnabled()
        {
            return UnityModManager.modEntries.Where(
                mod => mod.Info.Id.Equals("CharacterOptionsPlus") && mod.Enabled && !mod.ErrorOnLoading)
              .Any();
        }

        internal static void Init()
        {
            Logger.Log("Initializing settings.");
            SettingsBuilder settings =
              //SettingsBuilder.New(RootKey, LocalizationTool.CreateString(RootStringKey + "Title", "Phoenix's Scaling Cantrips"))
              SettingsBuilder.New(RootKey, GetString("Title"))
                .AddToggle(
                  Toggle.New(GetKey(VerboseLoggingKey), defaultValue: false, GetString("VerboseLogging"))
                    .WithLongDescription(GetString("VerboseLogging.Description"))
                    .OnValueChanged(Logging.EnableVerboseLogging))
                .AddDefaultButton(OnDefaultsApplied);
            
            settings.AddToggle(new Toggle("psc.settings.addscaling", true, GetString("EnableScaling")));
            settings.AddToggle(new Toggle("psc.settings.addfirebolt", true, GetString("AddFirebolt")));
            settings.AddToggle(new Toggle("psc.settings.addburningtouch", true, GetString("AddBurningTouch")));
            settings.AddToggle(new Toggle("psc.settings.addfrostytouch", true, GetString("AddFrostyTouch")));
            settings.AddToggle(new Toggle("psc.settings.addlessershockinggrasp", true, GetString("AddLesserShockingGrasp")));
            settings.AddToggle(new Toggle("psc.settings.addlessercorrosivetouch", true, GetString("AddLesserCorrosiveTouch")));

            settings.AddToggle(new Toggle("psc.settings.addpainfulnote", true, GetString("AddPainfulNote")));

            settings.AddToggle(new Toggle("psc.settings.adddissonanttouch", true, GetString("AddDissonantTouch")));
            settings.AddToggle(new Toggle("psc.settings.joltformagus", true, GetString("JoltForMagus")));
            settings.AddToggle(new Toggle("psc.settings.sonictoarrowsong", true, GetString("SonicToArrowsong")));
           
           
            

            Menu.AddSettings(settings);
            Logging.EnableVerboseLogging(IsEnabled(VerboseLoggingKey));
        }

        private static void OnDefaultsApplied()
        {
            Logger.Log($"Default settings restored.");
        }

        private static LocalizedString GetString(string key, bool usePrefix = true)
        {
            var fullKey = usePrefix ? $"{RootStringKey}.{key}" : key;
            return LocalizationTool.GetString(fullKey);
        }

        private static string GetKey(string partialKey)
        {
            return $"{RootKey}.{partialKey}";
        }

        private static readonly List<string> DefaultDisabled = new() {};
        private static bool GetDefault(string key)
        {
            return !DefaultDisabled.Contains(key);
        }
    }
}
