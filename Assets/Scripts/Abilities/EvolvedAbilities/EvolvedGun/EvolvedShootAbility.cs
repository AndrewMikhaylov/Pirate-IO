using Abilities.ShootAbility;
using UnityEngine;

namespace Abilities.EvolvedAbilities.EvolvedGun
{
    [CreateAssetMenu]
    public class EvolvedShootAbility: Ability
    {
        public override void ActivateAbility(Transform position)
        {
            BaseAbilityAction(position, 1);
            BaseAbilityAction(position, -1);

        } 
        private void BaseAbilityAction(Transform position, int direction)
        {
            var topProjectile = Instantiate(abilityData.Projectile, new Vector2(position.position.x + direction, position.position.y), position.rotation);
            topProjectile.GetComponent<Bullet>().SetBullet(projectileSpeed, new Vector2(direction,0.5f ).normalized, damage, knockBackForce);
            var middleProjectile = Instantiate(abilityData.Projectile, new Vector2(position.position.x + direction, position.position.y), position.rotation);
            middleProjectile.GetComponent<Bullet>().SetBullet(projectileSpeed, new Vector3(direction,0 ).normalized, damage, knockBackForce);
            var bottomProjectile = Instantiate(abilityData.Projectile, new Vector2(position.position.x + direction, position.position.y), position.rotation);
            bottomProjectile.GetComponent<Bullet>().SetBullet(projectileSpeed, new Vector3(direction,-0.5f ).normalized, damage, knockBackForce);
        }
    }
}