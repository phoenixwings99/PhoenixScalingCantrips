using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.BasicEx;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
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
        public static void CreateFirebolt()
        {
            string fireboltGUID = "69E2769505B34A548E0D9A5F21E3D5B4";
            Sprite fireboltIcon = BlueprintTool.Get<BlueprintAbility>("42a65895ba0cb3a42b6019039dd2bff1").Icon;//MoltenOrb
            string scorchingProj = "8cc159ce94d29fe46a94b80ce549161f";//Scorching ray projectile
            BlueprintAbility bolt = CreateRay("Firebolt", fireboltGUID, DamageEnergyType.Sonic, scorchingProj, fireboltIcon);
        }

        public static void CreateDissonantNote()
        {
            string painfulNoteGUID = "EA38928D3CF342CAB80A335D2AE44D72";
            Sprite painfulNoteIcon = BlueprintTool.Get<BlueprintAbility>("8e7cfa5f213a90549aadd18f8f6f4664").Icon;//EarPiercingscream
            string airbullet = "e093b08cd4cafe946962b339faf2310a";
            BlueprintAbility note = CreateRay("PainfulNote", painfulNoteGUID, DamageEnergyType.Sonic, airbullet, painfulNoteIcon);

            
        }

        private static BlueprintAbility CreateRay(string systemName, string guid, DamageEnergyType damage, Blueprint<BlueprintProjectileReference> projectile, Sprite icon, SpellSchool school = SpellSchool.Evocation)
        {
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

            ActionsBuilder action = ActionsBuilder.New().DealDamage(damageType: new DamageTypeDescription()
            {
                Type = DamageType.Energy,
                Energy = damage
            },
                value: ContextDice.Value(damage == DamageEnergyType.Sonic ? Kingmaker.RuleSystem.DiceType.D2 : Kingmaker.RuleSystem.DiceType.D3,
                diceCount: Settings.IsEnabled("addscaling") ? new ContextValue() { ValueType = ContextValueType.Rank, ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice } : ContextValues.Constant(1), bonus: ContextValues.Constant(0)));

            AbilityConfigurator cantrip = AbilityConfigurator.NewSpell(systemName, guid, school, false, descriptor)
                .SetDisplayName(systemName + ".Name")
                .SetDescription(systemName + (Settings.IsEnabled("addscaling") ? ".Desc" : ".Desc2"))
                .AddCantripComponent()
                .SetIcon(icon)
                .AddAbilityDeliverProjectile(needAttackRoll: true, projectiles: new List<Blueprint<BlueprintProjectileReference>>() { projectile }, weapon: "f6ef95b1f7bb52b408a5b345a330ffe8", lineWidth: new(5f))
                .AddAbilityEffectRunAction(actions: action)
                .SetCanTargetEnemies(true)
                .SetRange(AbilityRange.Close)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Directional)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard)
                .SetSpellResistance(true);
            if (Settings.IsEnabled("addscaling"))
            {
                cantrip = cantrip.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                {
                    m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                    m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                    m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.OnePlusDiv2,
                    m_StepLevel = 0,
                    m_StartLevel = 0,
                    m_Max = 6
                });
            }
            

            return cantrip.Configure();
        }
    }
}
