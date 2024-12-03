using _Main.Scripts.Skill;
using UnityEngine;

namespace _Main.Scripts.Mine
{
    public class MineSkillManager : SRSkill
    {
        [SerializeField] private GameObject activeMinePrefab;
        protected override void UseSkill()
        {
            CarSkillManager.UnequipSkill();
            Instantiate(activeMinePrefab, CarSkillManager.FxSockets.CarBackSocket.position,
                CarSkillManager.FxSockets.CarBackSocket.rotation);
            Destroy(gameObject);
        }
    }
}