using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Mechanics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Kingmaker.Kingdom.Settlements.SettlementGridTopology;
using static Kingmaker.UnitLogic.Mechanics.Properties.UnitPropertyComponent;

namespace PhoenixScalingCantrips.Spells
{
    internal class CreateTouchCantrips
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(CreateTouchCantrips));
        public static void CreateBurningTouch()
        {
            string burningTouchGUID = "25C60A4F408C4B2D97E4B9BC784E7F2F";
            var burningTouchIcon = BlueprintTool.Get<BlueprintAbility>("4783c3709a74a794dbe7c8e7e0b1b038").Icon;//Burning Hands
            string burningTouchGUID2 = "D3118971CE464C32982ED356B8971966";
            var cantrip = CreateTouchCantrip("BurningTouch", burningTouchGUID, burningTouchGUID2, DamageEnergyType.Fire, burningTouchIcon);
            

        }

        public static void CreateFrostyTouch()
        {
            var icon = BlueprintTool.Get<BlueprintAbility>("c83447189aabc72489164dfc246f3a36").Icon;


            string castGUid = "7B35B5CDD43046B28C4A0415C8E1799B";

            string touchGUID = "D51FCCAA33244489BAD0B69A14B157CC";
            var cantrip = CreateTouchCantrip("FrostyTouch", castGUid, touchGUID, DamageEnergyType.Cold, icon, touchprefabasset: "274fbd84b4c9d794bb5fe677472292b1");
           

        }

        public static void CreateLesserCorrosiveTouch()
        {

            string castGUID = "6D85B4E4E8D7418BBC6E4676E863CB5C";
            string touchGUID = "A230B2701FE34ED1B50DABDE6E878466";
            var icon = BlueprintTool.Get<BlueprintUnitFact>("1a40fc88aeac9da4aa2fbdbb88335f5d").Icon;
            var cantrip = CreateTouchCantrip("LesserCorrosiveTouch", castGUID, touchGUID, DamageEnergyType.Acid, icon, SpellSchool.Conjuration, "524f5d0fecac019469b9e58ce1b8402d");
            

        }

        public static void CreateLesserShockingGrasp()
        {
            string castGUID = "9B405733B7B842C0A06CE14DC08030D2";
            string touchGUID = "2FD3A8231D554CFAB08A88865DC1A97D";
            var icon = BlueprintTool.Get<BlueprintUnitFact>("ab395d2335d3f384e99dddee8562978f").Icon;
            var cantrip = CreateTouchCantrip("LesserShockingGrasp", castGUID, touchGUID, DamageEnergyType.Electricity, icon, touchprefabasset: "3ab291fca61cf3b4da311da82340ee9e");
            

        }

        public static void CreateDissonantTouch()
        {
            string dissonantTouchCast = "58DD4D975D904D1DA0F2DCD51BF5BECF";
            var dissonantTouchIcon = BlueprintTool.Get<BlueprintActivatableAbility>("287e0c88af08f3e4ba4aca52566f33a7").Icon;

            string dissonantTouch = "F1863B76AE55418C9EABA6C232BED44C";
            var cantrip = CreateTouchCantrip("DissonantTouch", dissonantTouchCast, dissonantTouch, DamageEnergyType.Electricity, dissonantTouchIcon);

        }


        private static BlueprintAbility CreateTouchCantrip(string systemName, string spellGuid, string touchGuid, DamageEnergyType damage, Sprite icon, SpellSchool school = SpellSchool.Evocation, string touchprefabasset = null)
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

            AbilityConfigurator touch = AbilityConfigurator.NewSpell(systemName, touchGuid, school, false, descriptor)
                .SetDisplayName(systemName + ".Name")
                .SetDescription(systemName + (Settings.IsEnabled("addscaling") ? ".Desc" : ".Desc2"))
                .AddCantripComponent()
                .SetIcon(icon)
                .AddAbilityEffectRunAction(action)
                .AddAbilityDeliverTouch(touchWeapon: "bb337517547de1a4189518d404ec49d4")
                .SetCanTargetEnemies(true)
                .SetSpellResistance(true)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(true)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Touch)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard);
            if (touchprefabasset != null)
                touch = touch.AddAbilitySpawnFx(AbilitySpawnFxAnchor.SelectedTarget, delay: 0.0f, orientationAnchor: AbilitySpawnFxAnchor.None, orientationMode: AbilitySpawnFxOrientation.Copy, weaponTarget: AbilitySpawnFxWeaponTarget.None, prefabLink: touchprefabasset);
            if (Settings.IsEnabled("addscaling"))
                touch = touch.AddContextRankConfig(new Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig()
                {
                    m_Type = Kingmaker.Enums.AbilityRankType.DamageDice,
                    m_BaseValueType = Kingmaker.UnitLogic.Mechanics.Components.ContextRankBaseValueType.CasterLevel,
                    m_Progression = Kingmaker.UnitLogic.Mechanics.Components.ContextRankProgression.StartPlusDivStep,
                    m_StepLevel = 2,
                    m_StartLevel = 0,
                    m_Max = 6

                });
           var touchDone = touch.Configure();

            var cantrip = AbilityConfigurator.NewSpell(systemName + "Cast", spellGuid, school, false, descriptor)
                .SetDisplayName(systemName + ".Name")
                .SetDescription(systemName + (Settings.IsEnabled("addscaling") ? ".Desc" : ".Desc2"))
                .AddCantripComponent()
                .SetIcon(icon)
                .AddAbilityEffectStickyTouch(touchDeliveryAbility: systemName)
                .SetCanTargetEnemies(true)
                .SetSpellResistance(true)
                .SetCanTargetFriends(true)
                .SetCanTargetSelf(true)
                .SetEffectOnEnemy(AbilityEffectOnUnit.Harmful)
                .SetAnimation(Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Self)
                .SetActionType(Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Standard);

            return cantrip.Configure();

        }
    }
}
