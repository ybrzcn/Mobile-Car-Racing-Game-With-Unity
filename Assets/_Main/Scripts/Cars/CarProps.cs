using UnityEngine;
using UnityEngine.Serialization;

namespace _Main.Scripts.Cars
{
    [CreateAssetMenu(fileName = "New Car", menuName = "Cars/New Car", order = 0)]
    public class CarProps : ScriptableObject
    {
        [SerializeField] private string carName;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject aiPrefab;
        [SerializeField] private GameObject showcaseModel;
        [Header("Power")] [SerializeField] private float maxSpeed;
        [SerializeField] private float motorPower;
        [SerializeField] private float brakePower;
        
        public float MaxSpeed => maxSpeed;

        public string CarName => carName;

        public GameObject ShowcaseModel => showcaseModel;

        public GameObject PlayerPrefab => playerPrefab;

        public GameObject AiPrefab => aiPrefab;

        public float MotorPower => motorPower;

        public float BrakePower => brakePower;
    }
}