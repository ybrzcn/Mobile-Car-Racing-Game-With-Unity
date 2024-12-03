using _Main.Scripts.Cars;
using UnityEngine;

namespace _Main.Scripts.UI
{
    public class SelectCarModel : MonoBehaviour
    {
        [SerializeField] private CarProps carProps;

        public CarProps CarProps => carProps;
    }
}