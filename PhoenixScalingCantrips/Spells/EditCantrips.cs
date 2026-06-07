using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabletopTweaks.Core.Utilities;

namespace PhoenixScalingCantrips.Spells
{
    internal class EditCantrips
    {
        public static void Edit()
        {
            if (Settings.IsEnabled("addscaling"))
            {
                
                var RayOfFrost = BlueprintTool.GetRef<BlueprintAbilityReference>("9af2ab69df6538f4793b2f9c3cc85603");
                EditCantrip(RayOfFrost);
                AbilityConfigurator.For(RayOfFrost).SetDescription("RayOfFrost.Desc2").Configure();

                var AcidSplash = BlueprintTool.GetRef<BlueprintAbilityReference>("0c852a2405dd9f14a8bbcfaf245ff823");
                EditCantrip(AcidSplash);
                AbilityConfigurator.For(AcidSplash).SetDescription("AcidSplash.Desc2").Configure();

                var Jolt = BlueprintTool.GetRef<BlueprintAbilityReference>("16e23c7a8ae53cc42a93066d19766404");
                EditCantrip(Jolt);
                AbilityConfigurator.For(Jolt).SetDescription("Jolt.Desc2").Configure();

                var DivineZap = BlueprintTool.GetRef<BlueprintAbilityReference>("8a1992f59e06dd64ab9ba52337bf8cb5");
                EditCantrip(DivineZap);
                AbilityConfigurator.For(DivineZap).SetDescription("DivineZap.Desc2").Configure();

                var DisruptUndead = BlueprintTool.GetRef<BlueprintAbilityReference>("652739779aa05504a9ad5db1db6d02ae");
                EditCantrip(DisruptUndead);
                AbilityConfigurator.For(DisruptUndead).SetDescription("DisruptUndead.Desc2").Configure();
            }
        }

        private static void EditCantrip(BlueprintAbilityReference cantrip)
        {
            Logging.GetLogger("PSC").Log("Scaling " + cantrip.NameSafe());
            var spell = cantrip.Get();

            AbilityEffectStickyTouch sticky = spell.GetComponent<AbilityEffectStickyTouch>();
            if (sticky != null)
            {
                spell = sticky.TouchDeliveryAbility;
            }

            var dmg = spell.GetComponent<AbilityEffectRunAction>().Actions.Actions.FirstOrDefault(x => x is ContextActionDealDamage) as ContextActionDealDamage;
            if ( (dmg is null))
            {
                Logging.GetLogger("PSC").Log("DMG is null for " + cantrip.NameSafe());
            }
            else
            {
                dmg.Value.DiceCountValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
                dmg.Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;
            }
            
            spell.AddContextRankConfig(x =>
            {
                x.m_Type = Kingmaker.Enums.AbilityRankType.DamageDice;
                x.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                x.m_Progression = ContextRankProgression.OnePlusDiv2;
                x.m_StepLevel = 0;
                x.m_StartLevel = 0;
                x.m_UseMax = true;
                x.m_Max = 6;
            });
        }
    }

    
}
