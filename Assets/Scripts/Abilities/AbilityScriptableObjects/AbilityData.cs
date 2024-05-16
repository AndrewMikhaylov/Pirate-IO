using UnityEngine;

namespace Abilities.AbilityScriptableObjects
{
    [CreateAssetMenu]
    public class AbilityData: ScriptableObject
    {
        public float CooldownSpeed;
        public float ProjectileSpeed;
        public float Damage;
        public float Radius;
        public float KnockBackForce;
        public GameObject Projectile;
        // public string description; TODO
    }
}