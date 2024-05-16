using Abilities.SwordAbility;
using UnityEngine;

namespace Abilities.EvolvedAbilities.EvolvedSword
{
    [CreateAssetMenu]
    public class EvolvedSwordAbility: Ability
    {
        public override void ActivateAbility(Transform position)
        {
            AbilityAction(position);
        }

        private void AbilityAction(Transform position)
        {
            var upperSwordObject = Instantiate(abilityData.Projectile,
                new Vector2(position.position.x, position.position.y + 1), position.rotation, position);
            upperSwordObject.GetComponent<Sword>().SetUpSword(damage, projectileSpeed, knockBackForce);
            var lowerSwordObject = Instantiate(abilityData.Projectile,
                new Vector2(position.position.x, position.position.y - 1), position.rotation, position);
            lowerSwordObject.GetComponent<Sword>().SetUpSword(damage, projectileSpeed, knockBackForce);
            
        }
    }
    
}
