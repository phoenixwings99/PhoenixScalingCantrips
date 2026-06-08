using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Spells;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixScalingCantrips.Spells
{
    internal class ProliferateCantrips
    {
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
                AbilityConfigurator.For("BurningTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    AddToSpellList("BurningTouch", "659fbc54fc519b44dacacc78e7d46dec");
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("efe346f6fec1ea84d84daa9eefdef204").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "BurningTouch", spellLevel: 0).Configure(delayed: true);
                }
            }
            if (Settings.IsEnabled("addlessercorrosivetouch"))
            {
                AbilityConfigurator.For("LesserCorrosiveTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    AddToSpellList("LesserCorrosiveTouch", "87a3e296757412e45910493e5fed1417");
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("210fd7d1314eabb45b8b51b41937d315").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserCorrosiveTouch", spellLevel: 0).Configure(delayed: true);
                }
               
            }
            if (Settings.IsEnabled("addfrostytouch"))
            {
                AbilityConfigurator.For("FrostyTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    AddToSpellList("FrostyTouch", "bbae401660bbad94c865d71029d8439e");
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("9a70e449c1f5c7548ab210a40c5f1890").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "FrostyTouch", spellLevel: 0).Configure(delayed: true);
                }
            }
            if (Settings.IsEnabled("addlessershockinggrasp"))
            {
                AbilityConfigurator.For("LesserShockingGrasp").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard).Configure();
                if (Settings.IsEnabled("proliferatetoshaman"))
                {
                    AddToSpellList("LesserShockingGrasp", "0bf6f90fdcb864b4486344100391b478");
                }
                if (Settings.IsEnabled("proliferatetooracle"))
                {
                    FeatureConfigurator.For("f482b5b69aaab72489d1f0da74743106").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "LesserShockingGrasp", spellLevel: 0).Configure(delayed: true);
                }
            }
            if (Settings.IsEnabled("joltformagus"))
            {
                AbilityConfigurator.For("16e23c7a8ae53cc42a93066d19766404").AddToSpellLists(0, SpellList.Magus).Configure();
                AbilityConfigurator.For("564c2ac83c7844beb1921e69ab159ac6").AddToSpellLists(0, SpellList.Magus).Configure();
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
                    FeatureConfigurator.For("23d3d21793cc4ae6a034860c89561253").AddKnownSpell(archetype: "b704d577abe54873b9228f56c2319b54", characterClass: "772c83a25e2268e448e841dcd548235f", spell: "DissonantTouch", 0).Configure(delayed: true);

                }
            }

            if (Settings.IsEnabled("proliferatetoshaman"))
            {
                AddToSpellList("RayOfFrost", "bbae401660bbad94c865d71029d8439e");
                AddToSpellList("AcidSplash", "87a3e296757412e45910493e5fed1417");
                AddToSpellList("Jolt", "0bf6f90fdcb864b4486344100391b478");
                AddToSpellList("Ignition", "659fbc54fc519b44dacacc78e7d46dec");
            }
            if (Settings.IsEnabled("proliferatetooracle"))
            {
                FeatureConfigurator.For("0b6eeed955daedd4896ef104cfc2afb9").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure(delayed:true);
                FeatureConfigurator.For("0b6eeed955daedd4896ef104cfc2afb9").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", archetype: "c5f6e53e71059fb4d802ce81a277a12d", spell: "Ignition", spellLevel: 0).Configure(delayed:true);
                FeatureConfigurator.For("efe346f6fec1ea84d84daa9eefdef204").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Ignition", spellLevel: 0).Configure(delayed:true);
                FeatureConfigurator.For("210fd7d1314eabb45b8b51b41937d315").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "AcidSplash", spellLevel: 0).Configure(delayed: true);
                FeatureConfigurator.For("9a70e449c1f5c7548ab210a40c5f1890").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);
                FeatureConfigurator.For("f482b5b69aaab72489d1f0da74743106").AddKnownSpell(characterClass: "20ce9bf8af32bee4c8557a045ab499b1", spell: "Jolt", spellLevel: 0).Configure(delayed: true);
            }

            if(Settings.IsEnabled("proliferatetowitch"))
            {
                FeatureConfigurator.For("fb5ae7deb7cbfd94a85c986df8f47c20").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);
            }
            if (Settings.IsEnabled("proliferatetowinterwitch"))
            {
                FeatureConfigurator.For("0bb1ebe4749807244ab344e1ed25986f").AddKnownSpell(characterClass: "145f1d3d360a7ad48bd95d392c81b38e", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);//Shaman
                FeatureConfigurator.For("3473fa3a4f9e8da44a75e884c03b1cbb").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);//Witch
                FeatureConfigurator.For("86ecd281c3be44a4d841bf2988aeeafd").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);//Witch (accursed)
                FeatureConfigurator.For("56adf819599827f4695395924a060996").AddKnownSpell(characterClass: "1b9873f1e7bfe5449bc84d03e9c8e3cc", spell: "RayOfFrost", spellLevel: 0).Configure(delayed: true);//Witch (Ley Line)
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
                    Logging.GetLogger("PSC").Log($"Patched {spell} onto spell list");
                }

            }).Configure();
        }

    }
}
