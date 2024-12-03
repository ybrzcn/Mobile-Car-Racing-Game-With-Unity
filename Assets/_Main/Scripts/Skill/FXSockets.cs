using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Main.Scripts.Skill
{
    [Serializable]
    public class FXSockets
    {
        [SerializeField] private Transform dashSkillSocket;
        [SerializeField] private Transform carBackSocket;

        public Transform DashSkillSocket => dashSkillSocket;

        public Transform CarBackSocket => carBackSocket;
    }
}