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
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TabletopTweaks.Core.Utilities;

namespace PhoenixScalingCantrips.Spells
{
    internal class EditCantrips
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(EditCantrips));

        public static void Edit()
        {


            if (Settings.IsEnabled("addscaling"))
            {

                BlueprintAbilityReference RayOfFrost = BlueprintTool.GetRef<BlueprintAbilityReference>("9af2ab69df6538f4793b2f9c3cc85603");
                EditCantrip(RayOfFrost);
                AbilityConfigurator.For(RayOfFrost).SetDescription("RayOfFrost.Desc2").Configure();

                BlueprintAbilityReference Ignition = BlueprintTool.GetRef<BlueprintAbilityReference>("564c2ac83c7844beb1921e69ab159ac6");
                EditCantrip(Ignition);
                AbilityConfigurator.For(Ignition).SetDescription("Ignition.Desc2").Configure();

                BlueprintAbilityReference AcidSplash = BlueprintTool.GetRef<BlueprintAbilityReference>("0c852a2405dd9f14a8bbcfaf245ff823");
                EditCantrip(AcidSplash);
                AbilityConfigurator.For(AcidSplash).SetDescription("AcidSplash.Desc2").Configure();

                BlueprintAbilityReference Jolt = BlueprintTool.GetRef<BlueprintAbilityReference>("16e23c7a8ae53cc42a93066d19766404");
                EditCantrip(Jolt);
                AbilityConfigurator.For(Jolt).SetDescription("Jolt.Desc2").Configure();

                BlueprintAbilityReference DivineZap = BlueprintTool.GetRef<BlueprintAbilityReference>("8a1992f59e06dd64ab9ba52337bf8cb5");
                EditCantrip(DivineZap);
                AbilityConfigurator.For(DivineZap).SetDescription("DivineZap.Desc2").Configure();

                BlueprintAbilityReference DisruptUndead = BlueprintTool.GetRef<BlueprintAbilityReference>("652739779aa05504a9ad5db1db6d02ae");
                EditCantrip(DisruptUndead);
                AbilityConfigurator.For(DisruptUndead).SetDescription("DisruptUndead.Desc2").Configure();

                EditVirtue();
            }
        }

        private static void EditVirtue()
        {
            BlueprintBuff buff = BlueprintTool.Get<BlueprintBuff>("a13ad2502d9e4904082868eb71efb0c5");
            TemporaryHitPointsFromAbilityValue temphp = buff.GetComponent<TemporaryHitPointsFromAbilityValue>();
            temphp.Value.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
            temphp.Value.ValueRank = Kingmaker.Enums.AbilityRankType.StatBonus;
            BuffConfigurator.For(buff).AddContextRankConfig(new ContextRankConfig()
            {
                m_Type = Kingmaker.Enums.AbilityRankType.StatBonus,
                m_BaseValueType = ContextRankBaseValueType.CasterLevel,
                m_Progression = ContextRankProgression.AsIs,
                m_UseMin = true,
                m_Min = 1,
                m_UseMax = true,
                m_Max = 10
            });
            BuffConfigurator.For("a13ad2502d9e4904082868eb71efb0c5").SetDescription("Virtue.Desc2").Configure();
            AbilityConfigurator.For("d3a852385ba4cd740992d1970170301a").SetDescription("Virtue.Desc2").Configure();
        }

        private static void EditCantrip(BlueprintAbilityReference cantrip)
        {
            try
            {

                Logger.Log("Scaling " + cantrip.NameSafe());
                BlueprintAbility spell = cantrip.Get();

                AbilityEffectStickyTouch sticky = spell.GetComponent<AbilityEffectStickyTouch>();
                if (sticky != null)
                {
                    spell = sticky.TouchDeliveryAbility;
                }
                
                ContextActionDealDamage dmg = spell.GetComponent<AbilityEffectRunAction>().Actions.Actions.FirstOrDefault(x => x is ContextActionDealDamage) as ContextActionDealDamage;
                if ((dmg is null))
                {
                    Logger.Log("ContextActionDealDamage not located in root of action for " + spell.NameSafe());
                    AbilityConfigurator.For(spell).RemoveComponents(x => x is ContextRankConfig).Configure();
                    
                    AbilityEffectRunAction existingAction = spell.GetComponent<AbilityEffectRunAction>();
                    Conditional conditional = (Conditional)existingAction.Actions.Actions.FirstOrDefault(x => x is Conditional);
                    
                    (conditional.IfTrue.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative;
                    (conditional.IfTrue.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueType = ContextValueType.Rank;
                    (conditional.IfTrue.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceType = Kingmaker.RuleSystem.DiceType.D4;
                    (conditional.IfFalse.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;
                    
                    (conditional.IfFalse.Actions.First(x => x is ContextActionDealDamage) as ContextActionDealDamage).Value.DiceCountValue.ValueType = ContextValueType.Rank;
                    
                    AbilityConfigurator cantripBuilder = AbilityConfigurator.For(cantrip);


                    cantripBuilder.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                    {
                        m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                        m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                        m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                        m_StepLevel = 0,
                        m_StartLevel = 0,
                        m_Max = 6
                    }).AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                    {
                        m_Type = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative,
                        m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                        m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                        m_StepLevel = 0,
                        m_StartLevel = 0,
                        m_Max = 10
                    }).Configure();

                }
                else
                {
                    Logger.Log("Editing ContextActionDealDamage in root of action for " + spell.NameSafe());
                    dmg.Value.DiceCountValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
                    dmg.Value.DiceCountValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice;
                    AbilityConfigurator.For(cantrip).AddContextRankConfig(new ContextRankConfig()
                    {
                        m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                        m_BaseValueType = ContextRankBaseValueType.CasterLevel,
                        m_Progression = ContextRankProgression.OnePlusDiv2,
                        m_StepLevel = 0,
                        m_StartLevel = 0,
                        m_UseMax = true,
                        m_Max = 6
                    }).Configure();
                }
            }
            catch(Exception ex)
            {
                Logging.GetLogger("PSC").Log("Exception editing " + cantrip.NameSafe() + ": " + ex.Message);
            }
            
            
        }
    }

    
}
