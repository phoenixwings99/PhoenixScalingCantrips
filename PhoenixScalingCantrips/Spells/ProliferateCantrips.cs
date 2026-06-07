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


            
            if (Settings.IsEnabled("addburningtouch"))
            {
                AbilityConfigurator.For("BurningTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard);
            }
            if (Settings.IsEnabled("addlessercorrosivetouch"))
            {
                AbilityConfigurator.For("LesserCorrosiveTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard);
            }
            if (Settings.IsEnabled("addfrostytouch"))
            {
                AbilityConfigurator.For("FrostyTouch").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard);
            }
            if (Settings.IsEnabled("addlessershockinggrasp"))
            {
                AbilityConfigurator.For("LesserShockingGrasp").AddToSpellLists(0, SpellList.Magus, SpellList.Wizard);
            }
            if (Settings.IsEnabled("joltformagus"))
            {
                AbilityConfigurator.For("16e23c7a8ae53cc42a93066d19766404").AddToSpellLists(0, SpellList.Magus);
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
