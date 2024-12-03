using System;
using _Main.Scripts.InGameUI;
using UnityEngine;

namespace _Main.Scripts.Skill
{
    public class SkillDisplay : MonoBehaviour
    {
        [SerializeField] private CarSkillManager carSkillManager;

        private void OnEnable()
        {
            carSkillManager.equipSkillEvent.AddListener(HandleUI);
            carSkillManager.unEquipSkillEvent.AddListener(ClearUI);
        }

        private void OnDisable()
        {
            carSkillManager.equipSkillEvent.RemoveListener(HandleUI);
            carSkillManager.unEquipSkillEvent.RemoveListener(ClearUI);
        }

        public void HandleUI(SRSkillProperties skillProperties)
        {
        }

        public void ClearUI()
        {
        }
    }
}