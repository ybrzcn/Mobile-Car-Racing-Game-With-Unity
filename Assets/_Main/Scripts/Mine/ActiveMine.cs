using System;
using _Main.Scripts.Skill;
using UnityEngine;

namespace _Main.Scripts.Mine
{
    public class ActiveMine : MonoBehaviour
    {
        [SerializeField] private GameObject activeMineVFX;
        [SerializeField] private GameObject mineExplosionVFX;

        [SerializeField] private float mineActivationDelay;
        [SerializeField] private float explosionRadius;
        [SerializeField] private float explosionForce;
        [SerializeField] private LayerMask carLayer;

        private bool mineActivated = false;

        private void OnEnable()
        {
            mineActivated = false;
            Invoke("ActivateMine", mineActivationDelay);
        }

        private void ActivateMine()
        {
            mineActivated = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && mineActivated)
            {
                Debug.Log("Mine Activated" + other.gameObject);
                activeMineVFX.SetActive(false);
                mineExplosionVFX.SetActive(true);

                var arr = Physics.OverlapSphere(transform.position, explosionRadius, carLayer.value);
                foreach (var car in arr)
                {
                    car.attachedRigidbody.AddExplosionForce(explosionForce, transform.position,
                        explosionRadius, 3.0f, ForceMode.Impulse);
                }
                Destroy(gameObject, 1f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}