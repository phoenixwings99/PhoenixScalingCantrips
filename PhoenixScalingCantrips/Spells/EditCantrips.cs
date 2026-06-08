using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Microsoft.Build.Utilities;
using Owlcat.Runtime.Core;
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

                EditVirtue();
            }
        }

        private static void EditVirtue()
        {
            var buff = BlueprintTool.Get<BlueprintBuff>("a13ad2502d9e4904082868eb71efb0c5");
            var temphp = buff.GetComponent<TemporaryHitPointsFromAbilityValue>();
            temphp.Value.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
            temphp.Value.ValueRank = Kingmaker.Enums.AbilityRankType.StatBonus;
            buff.AddContextRankConfig(x =>
            {
                x.m_Type = Kingmaker.Enums.AbilityRankType.StatBonus;
                x.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                x.m_Progression = ContextRankProgression.OnePlusDiv2;
                x.m_UseMin = true;
                x.m_Min = 1;
                x.m_UseMax = true;
                x.m_Max = 10;
            });
            BuffConfigurator.For("a13ad2502d9e4904082868eb71efb0c5").SetDescription("Virtue.Desc2").Configure();
            AbilityConfigurator.For("d3a852385ba4cd740992d1970170301a").SetDescription("Virtue.Desc2").Configure();
        }

        private static void EditCantrip(BlueprintAbilityReference cantrip)
        {
            try
            {
                Logging.GetLogger("PSC").Log("Scaling " + cantrip.NameSafe());
                var spell = cantrip.Get();

                AbilityEffectStickyTouch sticky = spell.GetComponent<AbilityEffectStickyTouch>();
                if (sticky != null)
                {
                    spell = sticky.TouchDeliveryAbility;
                }

                var dmg = spell.GetComponent<AbilityEffectRunAction>().Actions.Actions.FirstOrDefault(x => x is ContextActionDealDamage) as ContextActionDealDamage;
                if ((dmg is null))
                {
                    var existingCRC = spell.GetComponent<ContextRankConfig>();
                    spell.RemoveComponent(existingCRC);
                    var existingAction = spell.GetComponent<AbilityEffectRunAction>();
                    Conditional conditional = (Conditional)existingAction.Actions.Actions.FirstOrDefault(x => x is Conditional);
                    (conditional.IfTrue.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative;
                    (conditional.IfTrue.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceType = Kingmaker.RuleSystem.DiceType.D4;
                    (conditional.IfFalse.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;

                    var cantripBuilder = AbilityConfigurator.For(cantrip);


                    cantripBuilder = cantripBuilder.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                    {
                        m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                        m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                        m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.StartPlusDivStep,
                        m_StepLevel = 2,
                        m_StartLevel = 0,
                        m_Max = 6
                    }).AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                    {
                        m_Type = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative,
                        m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                        m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.AsIs,
                        m_StepLevel = 0,
                        m_StartLevel = 0,
                        m_Max = 16
                    });

                }
                else
                {
                    dmg.Value.DiceCountValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
                    dmg.Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;
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
            catch(Exception ex)
            {
                Logging.GetLogger("PSC").Log("Exception editing " + cantrip.NameSafe() + ": " + ex.Message);
            }
            
            
        }
    }

    
}
