using System;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Lap;
using _Main.Scripts.SRCarController;
using UnityEngine;

namespace _Main.Scripts.SRStanding
{
    public class StandingManager : MonoSingleton<StandingManager>
    {
        [SerializeField] private List<Standing> standingList;
        public List<Standing> StandingList => standingList;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
        
        private void Update()
        {
            if (standingList.Count == 0)
            {
                standingList = new List<Standing>();
                var arr = FindObjectsOfType<CarController>();
                standingList = arr.Select(x => x.GetComponent<CarLapManager>().Standing).ToList();
            }
            UpdateRanking();
        }

        private void UpdateRanking()
        {
            standingList = standingList.OrderByDescending(x => x.MyStandingScore()).ToList();
        }
    }
}