using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixScalingCantrips.Spells
{
    internal class ProliferateCantrips
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ProliferateCantrips));
        public static void Proliferate()
        {

            BlueprintTool.AddGuidsByName(("RayOfFrost", "9af2ab69df6538f4793b2f9c3cc85603"));
            BlueprintTool.AddGuidsByName(("AcidSplash", "0c852a2405dd9f14a8bbcfaf245ff823"));
            BlueprintTool.AddGuidsByName(("Jolt", "16e23c7a8ae53cc42a93066d19766404"));
            BlueprintTool.AddGuidsByName(("Ignition", "564c2ac83c7844beb1921e69ab159ac6"));
            BlueprintTool.AddGuidsByName(("DivineZap", "8a1992f59e06dd64ab9ba52337bf8cb5"));
            BlueprintTool.AddGuidsByName(("DisruptUndead", "652739779aa05504a9ad5db1db6d02ae"));

            if (Settings.IsEnabled("addburningtouch"))
            {
                AbilityConfigurator.For("BurningTouchCast").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                SpellListConfigurator.For("4d72e1e7bd6bc4f4caaea7aa43a14639").AddToSpellsByLevel(new SpellLevelList(0) { m_Spells = new() { BlueprintTool.GetRef<BlueprintAbilityReference>("BurningTouchCast") } }).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    FeatureConfigurator.For("faa12cd1a5ece2c408d3bd00803fe8a1").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "BurningTouchCast", spellLevel: 0).Configure();
                    
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("3b68909df737cd4458509d7f3a9c3706").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "BurningTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("eac5d0fc2b143c247a42f790230eb778").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "BurningTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("804e1560f21c95b43a45a9825336bd19").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "BurningTouchCast", spellLevel: 0).Configure();
                    
                }
            }
            if (Settings.IsEnabled("addlessercorrosivetouch"))
            {
                AbilityConfigurator.For("LesserCorrosiveTouchCast").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                SpellListConfigurator.For("4d72e1e7bd6bc4f4caaea7aa43a14639").AddToSpellsByLevel(new SpellLevelList(0) { m_Spells = new() { BlueprintTool.GetRef<BlueprintAbilityReference>("LesserCorrosiveTouchCast") } }).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                   
                    FeatureConfigurator.For("5c3ccab7cb27f4a408531197eb2abd3f").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "LesserCorrosiveTouchCast", spellLevel: 0).Configure();
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("7bb4bb3e7fd26f34e8ca035a27e03e85").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserCorrosiveTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("09106dcc03321154f955e2d0296a082e").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserCorrosiveTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("2127c37ccc7986e4083665552d6ecd5c").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserCorrosiveTouchCast", spellLevel: 0).Configure();
                }
               
            }
            if (Settings.IsEnabled("addfrostytouch"))
            {
                AbilityConfigurator.For("FrostyTouchCast").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                SpellListConfigurator.For("4d72e1e7bd6bc4f4caaea7aa43a14639").AddToSpellsByLevel(new SpellLevelList(0) { m_Spells = new() { BlueprintTool.GetRef<BlueprintAbilityReference>("FrostyTouchCast") } }).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    
                    FeatureConfigurator.For("62f408b7561adb34b899363c905fa13a").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "FrostyTouchCast", spellLevel: 0).Configure();
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("e2f8bd7c45dfb954c8c42b168505c783").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "FrostyTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("d91dcec40f768ea47b45bb4c4b2af40d").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "FrostyTouchCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("1267082c536549c4c86f0725b6eea627").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "FrostyTouchCast", spellLevel: 0).Configure();
                }
                //Frosty Touch to Winter (modded)
            }
            if (Settings.IsEnabled("addlessershockinggrasp"))
            {
                AbilityConfigurator.For("LesserShockingGraspCast").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                SpellListConfigurator.For("4d72e1e7bd6bc4f4caaea7aa43a14639").AddToSpellsByLevel(new SpellLevelList(0) { m_Spells = new() { BlueprintTool.GetRef<BlueprintAbilityReference>("LesserShockingGraspCast") } }).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    FeatureConfigurator.For("5d810adea03fb644582eb74de32c75ec").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "LesserShockingGraspCast", spellLevel: 0).Configure();
                    
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("7c1fdd831af747b47bb2cce0051f309b").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserShockingGraspCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("4fb980ffe85793b4194b11d3adb70d9e").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserShockingGraspCast", spellLevel: 0).Configure();
                    FeatureConfigurator.For("3812dcb2fe59ecd449285391db677edc").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserShockingGraspCast", spellLevel: 0).Configure();
                }
            }
            if (Settings.IsEnabled("joltformagus"))
            {
                AbilityConfigurator.For("Jolt").AddToSpellLists(0, SpellList.Magus).Configure();
                AbilityConfigurator.For("Ignition").AddToSpellLists(0, SpellList.Magus).Configure();
                            }
            if (Settings.IsEnabled("adddissonanttouch"))
            {
                if (Settings.IsCharOpPlusEnabled() && Settings.IsEnabled("sonictoarrowsong"))
                {
                    FeatureConfigurator.For("23d3d21793cc4ae6a034860c89561253").AddKnownSpell(archetype: "b704d577abe54873b9228f56c2319b54", characterClass: "772c83a25e2268e448e841dcd548235f", spell: "PainfulNote", 0).Configure(delayed: true);

                }
            }
            if (Settings.IsEnabled("addpainfulnote"))
            {
                if (Settings.IsCharOpPlusEnabled() && Settings.IsEnabled("sonictoarrowsong"))
                {
                    FeatureConfigurator.For("23d3d21793cc4ae6a034860c89561253").AddKnownSpell(archetype: "b704d577abe54873b9228f56c2319b54", characterClass: "772c83a25e2268e448e841dcd548235f", spell: "DissonantTouchCast", 0).Configure(delayed: true);

                }
            }

            if (Settings.IsEnabled("proliferatetoshaman"))
            {
                FeatureConfigurator.For("faa12cd1a5ece2c408d3bd00803fe8a1").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("5c3ccab7cb27f4a408531197eb2abd3f").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "AcidSplash", spellLevel: 0).Configure();
                FeatureConfigurator.For("62f408b7561adb34b899363c905fa13a").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "RayOfFrost", spellLevel: 0).Configure();
                FeatureConfigurator.For("7c1fdd831af747b47bb2cce0051f309b").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Jolt", spellLevel: 0).Configure();
                
            }
            if (Settings.IsEnabled("proliferatetooracle"))
            {
                FeatureConfigurator.For("0b6eeed955daedd4896ef104cfc2afb9").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("0b6eeed955daedd4896ef104cfc2afb9").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", archetype: "c5f6e53e71059fb4d802ce81a277a12d", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("3b68909df737cd4458509d7f3a9c3706").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("eac5d0fc2b143c247a42f790230eb778").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("804e1560f21c95b43a45a9825336bd19").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure();
                FeatureConfigurator.For("7bb4bb3e7fd26f34e8ca035a27e03e85").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "AcidSplash", spellLevel: 0).Configure();
                FeatureConfigurator.For("09106dcc03321154f955e2d0296a082e").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "AcidSplash", spellLevel: 0).Configure();
                FeatureConfigurator.For("2127c37ccc7986e4083665552d6ecd5c").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "AcidSplash", spellLevel: 0).Configure();
                FeatureConfigurator.For("e2f8bd7c45dfb954c8c42b168505c783").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "RayOfFrost", spellLevel: 0).Configure();
                FeatureConfigurator.For("d91dcec40f768ea47b45bb4c4b2af40d").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "RayOfFrost", spellLevel: 0).Configure();
                FeatureConfigurator.For("1267082c536549c4c86f0725b6eea627").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "RayOfFrost", spellLevel: 0).Configure();
                FeatureConfigurator.For("7c1fdd831af747b47bb2cce0051f309b").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Jolt", spellLevel: 0).Configure();
                FeatureConfigurator.For("4fb980ffe85793b4194b11d3adb70d9e").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Jolt", spellLevel: 0).Configure();
                FeatureConfigurator.For("3812dcb2fe59ecd449285391db677edc").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Jolt", spellLevel: 0).Configure();
                //Ray Of Frost to Winter (modded)
            }

            if(Settings.IsEnabled("proliferatetowitch"))
            {
                FeatureConfigurator.For("e98d8d9f907c1814aa7376d6cdaac012").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure();
            }
            if (Settings.IsEnabled("proliferatetowinterwitch"))
            {
                FeatureConfigurator.For("0bb1ebe4749807244ab344e1ed25986f").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "RayOfFrost", spellLevel: 0).Configure();//Shaman
                FeatureConfigurator.For("3473fa3a4f9e8da44a75e884c03b1cbb").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure();//Witch
                FeatureConfigurator.For("86ecd281c3be44a4d841bf2988aeeafd").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure();//Witch (accursed)
                FeatureConfigurator.For("56adf819599827f4695395924a060996").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure();//Witch (Ley Line)
            }

        }

        private static void AddToSpellList(string spell, string spellList)
        {
            SpellListConfigurator.For(spellList).ModifySpellsByLevel(x =>
            {
                if (x.SpellLevel == 0)
                {
                    if (x.m_Spells == null)
                        x.m_Spells = new();
                    x.m_Spells.Add(BlueprintTool.GetRef<BlueprintAbilityReference>(spell));
                    Logger.Log($"Patched {spell} onto spell list: ${BlueprintTool.Get<BlueprintSpellList>(spellList).NameSafe()}");
                }

            }).Configure();
        }

    }
}
