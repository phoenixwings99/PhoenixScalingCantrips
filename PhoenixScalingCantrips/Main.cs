using BlueprintCore.Utils;
using HarmonyLib;
using TabletopTweaks.Core.NewEvents;
using UnityModManagerNet;
using Kingmaker.PubSubSystem;
using System;
using PhoenixScalingCantrips.Spells;


namespace PhoenixScalingCantrips;
public static class Main
{
    private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(Main));

    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        try
        {
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            EventBus.Subscribe(new BlueprintCacheInitHandler());

            Logger.Log("Finished patching.");
        }
        catch (Exception e)
        {
            Logger.LogException("Failed to patch", e);
        }
        return true;
    }

    class BlueprintCacheInitHandler : IBlueprintCacheInitHandler
    {
        private static bool Initialized = false;
        private static bool InitializeDelayed = false;

        public void AfterBlueprintCachePatches()
        {
            try
            {
                if (InitializeDelayed)
                {
                    Logger.Log("Already initialized blueprints cache.");
                    return;
                }
                InitializeDelayed = true;


            }
            catch (Exception e)
            {
                Logger.LogException("Delayed blueprint configuration failed.", e);
            }
        }

        public void BeforeBlueprintCacheInit() { }

        public void BeforeBlueprintCachePatches() { }

        public void AfterBlueprintCacheInit()
        {
            try
            {
                if (Initialized)
                {
                    Logger.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                Logger.Log("Loading Strings");
                //First strings
                LocalizationTool.LoadEmbeddedLocalizationPacks(
                  "PhoenixScalingCantrips.LocalizedStrings.json"
                  );

                Logger.Log("Building Settings");
                // Then settings
                Settings.Init();
                Logger.Log("Creating spells");
                CreateSpells();
                Logger.Log("Scaling old spells");
                ScaleSpells();
                Logger.Log("Proliferating spells");
                ProliferateSpells();


            }
            catch (Exception e)
            {
                Logger.LogException("Failed to initialize.", e);
            }
        }

        private void CreateSpells()
        {
            CreateRayCantrips.CreateFirebolt();
            CreateRayCantrips.CreateDissonantNote();
            CreateTouchCantrips.CreateBurningTouch();
            CreateTouchCantrips.CreateFrostyTouch();
            CreateTouchCantrips.CreateLesserCorrosiveTouch();
            CreateTouchCantrips.CreateLesserShockingGrasp();
            CreateTouchCantrips.CreateDissonantTouch();
        }

        private  void ScaleSpells()
        {
            
            EditCantrips.Edit();

        }

        private void ProliferateSpells()
        {
            
            ProliferateCantrips.Proliferate();
        }


    }

}

