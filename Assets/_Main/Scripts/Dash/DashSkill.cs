using System.Collections;
using _Main.Scripts.InGameUI;
using _Main.Scripts.Skill;
using _Main.Scripts.SRCarController;
using UnityEngine;

namespace _Main.Scripts.Dash
{
    public class DashSkill : SRSkill
    {
        [SerializeField] private GameObject rocketThruster;
        [SerializeField] private AnimationCurve rocketThrusterForceCurve;
        [SerializeField] private float rocketThrusterDuration;
        [SerializeField] private float rocketThrusterForce;

        private Rigidbody carRb;

        public override void OnEnable()
        {
            base.OnEnable();
            var parent = transform.parent;
            carRb = parent.GetComponent<Rigidbody>();
        }

        protected override void UseSkill()
        {
            HandleRocketThruster();
            StartCoroutine(AddForceTilDurationEnds());
        }

        private void HandleRocketThruster()
        {
            rocketThruster.SetActive(true);
            rocketThruster.transform.SetParent(CarSkillManager.FxSockets.DashSkillSocket, false);
        }

        private IEnumerator AddForceTilDurationEnds()
        {
            var startTime = Time.time;
            var timeChecker = startTime + rocketThrusterDuration;
            CarSkillManager.UnequipSkill();
            while (Time.time < timeChecker)
            {
                var curveTime = (Time.time - startTime) / rocketThrusterDuration;
                carRb.AddForce(carRb.transform.forward *
                               (Time.deltaTime * (rocketThrusterForce * rocketThrusterForceCurve.Evaluate(curveTime))));
                yield return null;
            }
            Destroy(rocketThruster, 1f);
            Destroy(gameObject);
        }
    }
}