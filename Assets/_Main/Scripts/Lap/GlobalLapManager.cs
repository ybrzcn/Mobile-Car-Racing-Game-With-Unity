using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.SRCarController;
using _Main.Scripts.Track;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Main.Scripts.Lap
{
    public class GlobalLapManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> checkpoints;

        public List<GameObject> Checkpoints => checkpoints;
    }
}