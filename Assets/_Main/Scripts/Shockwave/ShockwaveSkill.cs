using System;
using _Main.Scripts.Skill;
using UnityEngine;

namespace _Main.Scripts.Shockwave
{
    public class ShockwaveSkill : SRSkill
    {
        [SerializeField] private GameObject fx;
        [SerializeField] private float shockWaveRadius;
        [SerializeField] private float shockwaveForce;
        [SerializeField] private LayerMask carLayer;

        
        protected override void UseSkill()
        {
            CarSkillManager.UnequipSkill();
            fx.SetActive(true);

            var arr = Physics.OverlapSphere(CarSkillManager.Caster.transform.position, shockWaveRadius, carLayer.value);
            foreach (var car in arr)
            {
                if (car.attachedRigidbody.gameObject != CarSkillManager.Caster.gameObject)
                {
                    car.attachedRigidbody.AddExplosionForce(shockwaveForce, CarSkillManager.Caster.transform.position, shockWaveRadius, 3.0f, ForceMode.Impulse);
                }
            }
                    
            Destroy(gameObject,1f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shockWaveRadius);
        }
    }
}