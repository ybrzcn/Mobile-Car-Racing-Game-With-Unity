using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Track
{
    public class TrackWaypointsManager : MonoSingleton<TrackWaypointsManager>
    {
        [SerializeField] private List<GameObject> waypoints;
        [SerializeField] private float roadThickness;

        [Header("Gizmos")] [SerializeField] private Color gizmoColor;

        public List<GameObject> Waypoints => waypoints;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawWireSphere(waypoint.transform.position, roadThickness);
            }
        }

        [ContextMenu("Look At")]
        public void LookAtNextWaypoint()
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (i == waypoints.Count - 1)
                {
                    waypoints[i].transform.LookAt(waypoints[0].transform, Vector3.up);
                }
                waypoints[i].transform.LookAt(waypoints[i+1].transform, Vector3.up);
            }
        }
    }
}