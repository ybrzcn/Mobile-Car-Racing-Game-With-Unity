using UnityEngine;

namespace _Main.Scripts.UI
{
    public class TotalLapCountButton : MonoBehaviour
    {
        [SerializeField] private GameObject boxGameObject;
        [SerializeField] private int myTotalLapCount;

        public int MyTotalLapCount => myTotalLapCount;

        public GameObject BoxGameObject => boxGameObject;
    }
}