using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Skill;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Main.Scripts.Pickup
{
    public class SRPickup : MonoBehaviour
    {
        [FormerlySerializedAs("skill")] [SerializeField] private List<SRSkillProperties> skillProperties;
        [SerializeField] private ParticleSystemRenderer pickupIconParticleRenderer;
        [SerializeField] private Collider col;
        [SerializeField] private GameObject destroyFX;
        [SerializeField] private GameObject pickupFXParent;
        [SerializeField] private float enableAgainDelay;

        private SRSkillProperties currentSkill;

        private void Start()
        {
            ResetPickup();
        }

        private SRSkillProperties GetRandomSkill()
        {
            return skillProperties[Random.Range(0, skillProperties.Count)];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) return;
            if (!other.attachedRigidbody.TryGetComponent(out CarSkillManager carSkillManager)) return;
            if (!carSkillManager.EquipSkillIfPossible(currentSkill)) return;


            MakePickupPassive();
            Instantiate(currentSkill.SkillPrefab, other.attachedRigidbody.transform);
        }

        private void MakePickupPassive()
        {
            col.enabled = false;
            pickupFXParent.SetActive(false);
            destroyFX.SetActive(true);
            DOVirtual.DelayedCall(.5f, () => gameObject.SetActive(false)).OnComplete( () => DOVirtual.DelayedCall(enableAgainDelay, ResetPickup));
        }

        private void ResetPickup()
        {
            currentSkill = GetRandomSkill();
            col.enabled = true;
            pickupFXParent.SetActive(true);
            destroyFX.SetActive(false);
            pickupIconParticleRenderer.material.mainTexture = SpriteToTextureConverter.TextureFromSprite(currentSkill.SkillIcon);
            gameObject.SetActive(true);
        }
    }
}