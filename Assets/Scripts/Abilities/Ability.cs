using System;
using Abilities.AbilityScriptableObjects;
using Abilities.EvolvedAbilities.EvolvedShip;
using Abilities.LevelUp;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        protected int abilityLevel;
        protected float damage;
        protected float cooldownSpeed;
        protected float projectileSpeed;
        protected float _radius;
        protected float knockBackForce;
        public AbilityData abilityData;
        public AbilityType AbilityType;
        public bool HasEvolvedStage;
        public void LevelUp(float boostAmount, AbilityKeyWord keyword)
        {
            if (abilityLevel<=5)
            {
                switch (keyword)
                {
                    case AbilityKeyWord.Damage:
                        damage += boostAmount;
                        break;
                    case AbilityKeyWord.Cooldown:
                        cooldownSpeed += boostAmount;
                        break;
                    case AbilityKeyWord.Speed:
                        projectileSpeed += boostAmount;
                        break;
                    case AbilityKeyWord.Radius:
                        _radius += boostAmount;
                        break;
                    case AbilityKeyWord.KnockBackForce:
                        knockBackForce += boostAmount;
                        break;
                }
                abilityLevel += 1;   
            }
        }
        public abstract void ActivateAbility(Transform position);

        public float GetAbilityCooldown()
        {
            return cooldownSpeed;
        }

        public bool CanBeLeveledUp()
        {
            if (abilityLevel<5)
            {
                return true;
            }
            return false;
        }

        public void SetUpAbility()
        {
            damage = abilityData.Damage;
            cooldownSpeed = abilityData.CooldownSpeed;
            projectileSpeed = abilityData.ProjectileSpeed;
            knockBackForce = abilityData.KnockBackForce;
            _radius = abilityData.Radius;
        }
    }
}
