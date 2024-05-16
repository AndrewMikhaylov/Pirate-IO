using Abilities.AbilityScriptableObjects;
using UnityEngine;

namespace Abilities.ShootAbility
{
    [CreateAssetMenu]
    public class ShootAbility : Ability
    {
        public override void ActivateAbility(Transform position)
        {
            BaseAbilityAction(position, 1);
            BaseAbilityAction(position, -1);

        } 
        private void BaseAbilityAction(Transform position, int direction)
        {
            var rightProjectile = Instantiate(abilityData.Projectile, new Vector2(position.position.x + direction, position.position.y), position.rotation);
            rightProjectile.GetComponent<Bullet>().SetBullet(projectileSpeed, new Vector3(direction,0,0 ), damage, knockBackForce);
        }
    }
}