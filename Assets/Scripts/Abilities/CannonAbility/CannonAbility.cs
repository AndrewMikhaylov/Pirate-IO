using System.Collections.Generic;
using Abilities.AbilityScriptableObjects;
using EnemyLogic;
using UnityEngine;
using Random = System.Random;

namespace Abilities.CannonAbility
{
    [CreateAssetMenu]
    public class CannonAbility: Ability
    {
        public override void ActivateAbility(Transform position)
        {
            AbilityAction(position);
        }

        private void AbilityAction(Transform playerPosition)
        {
            var cannonball = Instantiate(abilityData.Projectile,
                new Vector2(playerPosition.position.x, playerPosition.position.y + 1), playerPosition.rotation,
                playerPosition);
            cannonball.GetComponent<Cannonball>().SetUpCannonball(damage, projectileSpeed, _radius, knockBackForce);
        }
    }
}