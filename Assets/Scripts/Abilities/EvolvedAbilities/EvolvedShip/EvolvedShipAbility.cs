using Abilities.ShipAbility;
using UnityEngine;

namespace Abilities.EvolvedAbilities.EvolvedShip
{
    [CreateAssetMenu]
    public class EvolvedShipAbility:Ability
    {
        [SerializeField] private int _additionalProjectileNumber;
        private float _projectileSpawnCounter;
        public override void ActivateAbility(Transform position)
        {
            _projectileSpawnCounter=_additionalProjectileNumber;
            float spawnDistance = GetTopRightCornerDistance(position);
            AbilityAction(position, spawnDistance);
        }

        private float GetTopRightCornerDistance(Transform transform)
        {
            Camera camera = Camera.main;
            Vector3 topRightCorner = camera.ViewportToWorldPoint(new Vector3(1,1, camera.nearClipPlane));
            var distance = Vector3.Distance(transform.position, topRightCorner * _radius);
            return distance;
        }

        private void PlayerHit(Transform transform, float radius)
        {
            while (_projectileSpawnCounter != 0)
            {
                var randomDestinationPoint = Random.insideUnitCircle.normalized * GetTopRightCornerDistance(transform) + (Vector2)transform.position;
                var firstShipProjectile = Instantiate(abilityData.Projectile, transform.position, transform.rotation);
                firstShipProjectile.GetComponent<EvolvedShip>().SetUpProjectile(randomDestinationPoint, _radius, projectileSpeed, damage, knockBackForce);
                _projectileSpawnCounter -= 1;
            }
        }

        private void AbilityAction(Transform position, float spawnDistance)
        {
            var randomSpawnPoint = Random.insideUnitCircle.normalized * spawnDistance + (Vector2)position.position;
            var ghostShipProjectile = Instantiate(abilityData.Projectile, randomSpawnPoint, position.rotation);
            ghostShipProjectile.GetComponent<EvolvedShip>().SetUpProjectile(position, _radius, projectileSpeed, damage, knockBackForce);
            EvolvedShip.OnEvolvedShipPlayerHit += PlayerHit;
        }
    }
}