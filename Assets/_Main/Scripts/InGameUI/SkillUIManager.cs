using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _Main.Scripts.InGameUI
{
    public class SkillUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject skillIconGo;
        [SerializeField] private Image cdImg;
        [SerializeField] private Image img;
        [SerializeField] private Button useSkillBtn;
        
        public Button UseSkillBtn => useSkillBtn;

        public void HandleUI(Sprite skillIcon)
        {
            img.sprite = skillIcon;
            skillIconGo.SetActive(true);
        }

        public void ResetUI()
        {
            img.sprite = null;
            skillIconGo.SetActive(false);
        }

        public void StartCooldown(float coolDown)
        {
            StartCoroutine(OnCooldown(coolDown));
        }

        private IEnumerator OnCooldown(float cooldown)
        {
            useSkillBtn.interactable = false;
            var startTime = Time.time;
            var cdChecker = startTime + cooldown;
            while (Time.time < cdChecker)
            {
                cdImg.fillAmount = 1 - ((Time.time - startTime) / cooldown);
                yield return null;
            }
            cdImg.fillAmount = 0;

            useSkillBtn.interactable = true;
        }
    }
}