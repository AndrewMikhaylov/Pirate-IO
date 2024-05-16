using UnityEngine;

namespace Abilities.ShipAbility
{
    [CreateAssetMenu]
    public class ShipAbility: Ability
    {
        public override void ActivateAbility(Transform position)
        {
            Camera camera = Camera.main;
            Vector3 topRightCorner = camera.ViewportToWorldPoint(new Vector3(1,1, camera.nearClipPlane));
            float spawnDistance = Vector3.Distance(position.position, topRightCorner*_radius);
            BaseAbilityAction(position, spawnDistance);
        }
        

        private void BaseAbilityAction(Transform position, float spawnDistance)
        {
            var randomSpawnPoint = Random.insideUnitCircle.normalized * spawnDistance + (Vector2)position.position;
            var ghostShipProjectile = Instantiate(abilityData.Projectile, randomSpawnPoint, position.rotation);
            ghostShipProjectile.GetComponent<Ship>().SetUpProjectile(position, _radius, projectileSpeed, damage, knockBackForce);
        }
    }
}