using System;
using _Main.Scripts.InGameUI;
using _Main.Scripts.Track;
using UnityEngine;

namespace _Main.Scripts.SRCarController
{
    public class WrongDirectionChecker : MonoBehaviour
    {
        [SerializeField] private CarController carController;
        [SerializeField] private Transform tr;

        private void Update()
        {
            CheckIfItsGoesToWrongDir();
        }

        private void CheckIfItsGoesToWrongDir()
        {
            var dotProduct = Vector3.Dot(tr.forward,
                TrackWaypointsManager.Instance.Waypoints[carController.CurrentWaypointIndex].transform.forward);
            InGameUIManager.Instance.WrongDirectionImage.SetActive(dotProduct < 0f);
        }
    }
}