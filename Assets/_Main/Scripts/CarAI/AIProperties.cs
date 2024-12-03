using UnityEngine;

namespace _Main.Scripts.CarAI
{
    [CreateAssetMenu(fileName = "New AI", menuName = "Car AI/New AI", order = 0)]
    public class AIProperties : ScriptableObject
    {
        [SerializeField] [Range(0f, .75f)] private float aiSpeedPenalty;
        [SerializeField] private float aiUseSkillMinTime;
        [SerializeField] private float aiUseSkillMaxTime;

        public float AiSpeedPenalty => aiSpeedPenalty;

        public float AiUseSkillMinTime => aiUseSkillMinTime;

        public float AiUseSkillMaxTime => aiUseSkillMaxTime;
    }
}