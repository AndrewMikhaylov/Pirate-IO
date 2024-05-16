using System;
using UnityEngine;

namespace EnemyLogic
{
    public class EnemyHealthManager
    {
        public Action hitPointsAtZero;
        private float _maxHealth;
        private float _currentHealth;

        public EnemyHealthManager(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            Debug.Log("Ouch");
            if (_currentHealth<=0)
            {
                hitPointsAtZero.Invoke();
            }
        }
    }
}