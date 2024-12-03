using System;
using System.Linq;
using _Main.Scripts.SRGameSettings;
using _Main.Scripts.SRInput;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _Main.Scripts.CarAI
{
    public class CarAIInputManager : InputManager
    {
        [SerializeField] private Transform aiTarget;
        [SerializeField] private NavMeshAgent navmeshAgent;

        [SerializeField] private float angleThreshold;

        private Transform tr;
        private int currentTargetIndex;
        
        private Vector3 navmeshLocalPos;

        private void Start()
        {
            AdjustNavmesh();
            tr = transform;

            currentTargetIndex = 0;
            aiTarget.SetParent(null);
            aiTarget.position = GameManager.Instance.GlobalLapManager.Checkpoints[currentTargetIndex].transform
                .position;

            gasInput = 1f - GameSettingsManager.Instance.GameSettings.AiProperties.AiSpeedPenalty;
        }

        protected override void OnEnable()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                currentTargetIndex++;
                if (currentTargetIndex == GameManager.Instance.GlobalLapManager.Checkpoints.Count)
                {
                    currentTargetIndex = 0;
                }
                aiTarget.position = GameManager.Instance.GlobalLapManager.Checkpoints[currentTargetIndex].transform
                    .position;
            }
        }

        private void AdjustNavmesh()
        {
            navmeshAgent.isStopped = true;
            navmeshLocalPos = navmeshAgent.transform.localPosition;
            navmeshAgent.transform.SetParent(null);
        }

        private void MakeNavmeshFollowToCar()
        {
            var pos = transform.position;
            pos += navmeshLocalPos;
            navmeshAgent.transform.position = pos;
            
            navmeshAgent.SetDestination(aiTarget.position);
        }

        protected override void Update()
        {
            base.Update();
            MakeNavmeshFollowToCar();
        }

        protected override void GetInputs()
        {
            if (navmeshAgent.path.corners.Length < 2)
            {
                return;
            }
            
            var dir = (navmeshAgent.path.corners[1] - tr.position).normalized;
            var angle = Vector3.SignedAngle(tr.forward, dir, Vector3.up);
            
            if (Mathf.Abs(angle) < angleThreshold)
            {
                steeringInput = 0f;
            }
            else
            {
                steeringInput = angle < 0f ? steeringInput = -1f : steeringInput = 1f;
            }
        }
    }
}