using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.BasicEx;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PhoenixScalingCantrips.Spells
{
    internal class CreateRayCantrips
    {

        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(CreateRayCantrips));
        public static void CreateDissonantNote()
        {
            string painfulNoteGUID = "EA38928D3CF342CAB80A335D2AE44D72";
            Sprite painfulNoteIcon = BlueprintTool.Get<BlueprintAbility>("8e7cfa5f213a90549aadd18f8f6f4664").Icon;//EarPiercingscream
            string airbullet = "e093b08cd4cafe946962b339faf2310a";
            BlueprintAbility note = CreateRay("PainfulNote", painfulNoteGUID, DamageEnergyType.Sonic, airbullet, painfulNoteIcon);

            
        }

        private static BlueprintAbility CreateRay(string systemName, string guid, DamageEnergyType damage, Blueprint<BlueprintProjectileReference> projectile, Sprite icon, SpellSchool school = SpellSchool.Evocation)
        {
           Logger.Log("Creating " + systemName);
            SpellDescriptor[] descriptor = new SpellDescriptor[] { };

            if (damage == DamageEnergyType.Fire)
                descriptor = new SpellDescriptor[] { SpellDescriptor.Fire };
            else if (damage == DamageEnergyType.Acid)
                descriptor = new SpellDescriptor[] { SpellDescriptor.Acid };
            else if (damage == DamageEnergyType.Cold)
                descriptor = new SpellDescriptor[] { SpellDescriptor.Cold };
            else if (damage == DamageEnergyType.Sonic)
                descriptor = new SpellDescriptor[] { SpellDescriptor.Sonic };
            else if (damage == DamageEnergyType.Electricity)
                descriptor = new SpellDescriptor[] { SpellDescriptor.Electricity };

            ActionsBuilder forkAction = ActionsBuilder.New().Conditional(ConditionsBuilder.New().HasFact("d26fb54b74d44a45b84ef7150a460348"),
                ifTrue: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription()
                {
                    Type = DamageType.Energy,
                    Energy = damage
                },
                    value: ContextDice.Value(damage == DamageEnergyType.Sonic ? Kingmaker.RuleSystem.DiceType.D3 : Kingmaker.RuleSystem.DiceType.D4,
                    diceCount: new ContextValue() { ValueType = ContextValueType.Rank, ValueRank = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative } ))

                ,
                ifFalse: ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription()
                {
                    Type = DamageType.Energy,
                    Energy = damage
                }, 
                    value: ContextDice.Value(damage == DamageEnergyType.Sonic ? Kingmaker.RuleSystem.DiceType.D2 : Kingmaker.RuleSystem.DiceType.D3,
                        diceCount: Settings.IsEnabled("addscaling") ? new ContextValue() { ValueType = ContextValueType.Rank , ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice } : ContextValues.Constant(1), bonus: ContextValues.Constant(0)))
                    
                
                );

            

            AbilityConfigurator cantrip = AbilityConfigurator.NewSpell(systemName, guid, school, false, descriptor)
                .SetDisplayName(systemName + ".Name")
                .SetDescription(systemName + (Settings.IsEnabled("addscaling") ? ".Desc" : ".Desc2"))
                .AddCantripComponent()
                .SetIcon(icon)
                .AddAbilityDeliverProjectile(needAttackRoll: true, projectiles: new List<Blueprint<BlueprintProjectileReference>>() { projectile }, weapon: "f6ef95b1f7bb52b408a5b345a330ffe8", lineWidth: new(5f))
                .AddAbilityEffectRunAction(actions: forkAction)
                .SetCanTargetEnemies(true)
                .SetRange(AbilityRange.Close)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetSpellResistance(true);
            if (Settings.IsEnabled("addscaling"))
            {
                Logger.Log("Adding scaling to " + systemName);
                cantrip = cantrip.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                {
                    m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                    m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                    m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                    m_StepLevel = 0,
                    m_StartLevel = 1,
                    m_Max = 6
                }).AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                {
                    m_Type = Kingmaker.Enums.AbilityRankType.DamageDiceAlternative,
                    m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.ClassLevel,
                    m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                    m_StepLevel = 0,
                    m_StartLevel = 1,
                    m_Max = 10
                });
            }
            else
            {
                cantrip = cantrip.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                {
                    m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                    m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.ClassLevel,
                    m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                    m_StepLevel = 0,
                    m_StartLevel = 1,
                    m_Max = 10,
                    m_Class = new[] {BlueprintTool.GetRef<BlueprintCharacterClassReference>("1b9873f1e7bfe5449bc84d03e9c8e3cc"), BlueprintTool.GetRef<BlueprintCharacterClassReference>("eb24ca44debf6714aabe1af1fd905a07") }

                });
            }
            

            return cantrip.Configure();
        }
    }
}
