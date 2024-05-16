using System;
using Abilities.ShipAbility;
using EnemyLogic;
using PlayerController;
using UnityEngine;

namespace Abilities.EvolvedAbilities.EvolvedShip
{
    public class EvolvedShip: Ship
    {
        public static Action<Transform, float> OnEvolvedShipPlayerHit;
        private Vector2 _destination;
        private float _speed;
        private float _damage;
        private float _knockBackForce;
        private float _radius;
        private bool _canInteractWithPlayer;
        
        public override void SetUpProjectile(Transform playerTransform, float radius, float speed, float damage, float knockBackForce)
        {
            _speed = speed;
            _damage = damage;
            _radius = radius;
            var xDestination = playerTransform.position.x - (transform.position.x - playerTransform.position.x);
            var yDestination = playerTransform.position.y - (transform.position.y - playerTransform.position.y);
            _destination = new Vector2(xDestination, yDestination);
            _knockBackForce = knockBackForce;
            _canInteractWithPlayer = true;
        }
        public void SetUpProjectile(Vector2 destination, float radius, float speed, float damage, float knockBackForce)
        {
            _speed = speed;
            _damage = damage;
            _radius = radius;
            _destination = destination;
            _knockBackForce = knockBackForce;
            _canInteractWithPlayer = false;
        }
        private void Update()
        {
            transform.position = Vector2.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, _destination)<0.01f)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy))
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                Vector2 directionAndForce = direction * _knockBackForce;
                enemy.TakeDamage(_damage, directionAndForce);
            }

            if (other.gameObject.TryGetComponent<Player>(out Player player) && _canInteractWithPlayer)
            {
                OnEvolvedShipPlayerHit.Invoke(other.transform, _radius);
                _canInteractWithPlayer = false;
            }
        }
    }
}