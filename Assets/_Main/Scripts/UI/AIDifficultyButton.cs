using _Main.Scripts.CarAI;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class AIDifficultyButton : MonoBehaviour
    {
        [SerializeField] private GameObject boxGameObject;
        [SerializeField] private AIProperties myAIProperty;

        public AIProperties MyAIProperty => myAIProperty;

        public GameObject BoxGameObject => boxGameObject;
        
    }
}