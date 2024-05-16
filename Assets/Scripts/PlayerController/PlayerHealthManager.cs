using UnityEngine;

namespace PlayerController
{
    public class PlayerHealthManager
    {
        private float _maxHealth;
        private float _currentHealth;

        public PlayerHealthManager(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }
        
        public void TakeDamage(float damageAmount)
        {
            _currentHealth -= damageAmount;
        }

        public void UpMaxHealth(float upMaxHealth)
        {
            _maxHealth += upMaxHealth;
            _currentHealth += upMaxHealth;
        }
    }
}