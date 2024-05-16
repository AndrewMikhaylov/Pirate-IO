using Abilities.AbilityScriptableObjects;
using UnityEngine;

namespace Abilities.SwordAbility
{
    [CreateAssetMenu]
    public class SwordAbility: Ability
    {
        public override void ActivateAbility(Transform position)
        {
            AbilityAction(position);
        }

        private void AbilityAction(Transform position)
        {
            var swordObject = Instantiate(abilityData.Projectile,
                new Vector2(position.position.x, position.position.y + 1), position.rotation, position);
            swordObject.GetComponent<Sword>().SetUpSword(damage, projectileSpeed, knockBackForce);
            
        }
    }
}