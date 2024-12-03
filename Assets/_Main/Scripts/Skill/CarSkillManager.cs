using System;
using _Main.Scripts.InGameUI;
using UnityEngine;
using UnityEngine.Events;

namespace _Main.Scripts.Skill
{
    public class CarSkillManager : MonoBehaviour
    {
        [SerializeField] private CasterType casterType; 
        [SerializeField] private GameObject caster;
        [SerializeField] private FXSockets fxSockets;
        
        private SRSkillProperties equippedSkillProperties;

        private float cooldown;

        [HideInInspector] public UnityEvent<SRSkillProperties> equipSkillEvent;
        [HideInInspector] public UnityEvent unEquipSkillEvent;
        public FXSockets FxSockets => fxSockets;

        public CasterType CasterType => casterType;

        public GameObject Caster => caster;

        private void Awake()
        {
            equipSkillEvent = new UnityEvent<SRSkillProperties>();
            unEquipSkillEvent = new UnityEvent();
        }

        public bool EquipSkillIfPossible(SRSkillProperties skillProperties)
        {
            if (equippedSkillProperties != null)
                return false;
            equippedSkillProperties = skillProperties;
            
            switch (casterType)
            {
                case CasterType.AI:
                    break;
                case CasterType.Human:
                    InGameUIManager.Instance.SkillUIManager.HandleUI(skillProperties.SkillIcon);
                    break;
            }
            
            equipSkillEvent?.Invoke(skillProperties);
            return true;
        }

        public void UnequipSkill()
        {
            equippedSkillProperties = null;
            
            switch (casterType)
            {
                case CasterType.AI:
                    break;
                case CasterType.Human:
                    InGameUIManager.Instance.SkillUIManager.ResetUI();
                    break;
            }
            
            unEquipSkillEvent?.Invoke();
        }
    }
}