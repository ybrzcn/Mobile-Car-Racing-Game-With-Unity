using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Main.Scripts.Cars;
using _Main.Scripts.InGameUI;
using _Main.Scripts.SRCarController.Gear;
using _Main.Scripts.SRInput;
using _Main.Scripts.Track;
using UnityEngine;

namespace _Main.Scripts.SRCarController
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private CarProps carProps;
        [SerializeField] private InputManager inputManager;
        [Header("Comps")] [SerializeField] private Rigidbody carRb;
        [SerializeField] private Vector3 centerOfMass;
        [Header("Wheels")] [SerializeField] private WheelColliders wheelColliders;
        [SerializeField] private WheelMeshes wheelMeshes;
        [Space]
        [SerializeField] [Range(0f, 1f)] private float acceleration;
        [Header("Drift")] [SerializeField] private float driftStiffness;
        [SerializeField] private float driftStiffnessChangeLerpMultiplier;
        [Header("Steering")] [SerializeField] private float ackermanSteeringTurnRadiusConstant = 6;

        [Header("Down Force")] [SerializeField]
        private float downForce;

        [Header("Gear")] [SerializeField] private List<GearProperties> gears;
        [SerializeField] private float shiftSpeedThreshold;
        [Header("RPM")] [SerializeField] private float idleRPM;
        [SerializeField] private float maxRpm;

        [Header("Recover")] [SerializeField] private LayerMask waypointLayerMask;
        [SerializeField] private float waypointYOffset;
        [SerializeField] private float waypointCheckerRadius;
        [SerializeField] private float waypointCheckerRadiusRecoverMax;
        [SerializeField] private float recoverAngle;
        [SerializeField] private float recoverTime;
        [SerializeField] private float recoverSpeed;

        private Transform tr;

        private int currentGear, currentWaypointIndex;

        private float speed;
        private float originalFwStiffness, originalSwStiffness;
        private float recoverTimeChecker;
        private float rpm;
        private bool forceRecover;
        private Vector3 forceRecoverPos;
        private Quaternion forceRecoverRot;

        public float RecoverTimeChecker => recoverTimeChecker;

        public float RecoverTime => recoverTime;

        public int CurrentWaypointIndex => currentWaypointIndex;

        public CarProps CarProps => carProps;

        public float Speed => speed;

        public float Rpm => rpm;

        public float MaxRpm => maxRpm;
        
        public int CurrentGear => currentGear;

        private void Awake()
        {
            tr = transform;
            UpdateCurrentWaypoint();    
        }


        private void Start()
        {
            carRb.centerOfMass = centerOfMass;
            originalFwStiffness = wheelColliders.fRWheel.forwardFriction.stiffness;
            originalSwStiffness = wheelColliders.fRWheel.sidewaysFriction.stiffness;
            recoverTimeChecker = 0f;
            forceRecover = false;
        }

        private void Update()
        {
            UpdateWheels();
            UpdateCurrentWaypoint();    
            CheckRecover();


            var vel = carRb.velocity;
            vel.y = 0f;
            speed = vel.magnitude * 3.6f;
        }

        public void UpdateCurrentWaypoint()
        {
            var arr = Physics.OverlapSphere(transform.position, waypointCheckerRadius, waypointLayerMask.value);
            if (arr.Length == 0)
            {
                Debug.LogError("No Waypoint");
                return;
            }

            var nearestWaypoint = arr.OrderBy(x => Vector3.Distance(tr.position, x.transform.position))
                .FirstOrDefault();
            currentWaypointIndex = TrackWaypointsManager.Instance.Waypoints.IndexOf(nearestWaypoint.gameObject);
        }
        
        private void FixedUpdate()
        {
            ApplyMotorTorque();
            ApplyBrake();
            ApplySteering();
            ShiftGears();
            ApplyDrifting();

            carRb.AddForce(-Vector3.up * (downForce * carRb.velocity.magnitude));
        }


        private void ApplySteering()
        {
            var steerAngle = 0f;
            if (inputManager.SteeringInput > 0)
            {
                wheelColliders.fRWheel.steerAngle = Mathf.Rad2Deg *
                                                    Mathf.Atan(
                                                        2.55f / (ackermanSteeringTurnRadiusConstant + (1.5f / 2))) *
                                                    inputManager.SteeringInput;
                wheelColliders.fLWheel.steerAngle = Mathf.Rad2Deg *
                                                    Mathf.Atan(
                                                        2.55f / (ackermanSteeringTurnRadiusConstant - (1.5f / 2))) *
                                                    inputManager.SteeringInput;
            }
            else if (inputManager.SteeringInput < 0)
            {
                wheelColliders.fRWheel.steerAngle = Mathf.Rad2Deg *
                                                    Mathf.Atan(
                                                        2.55f / (ackermanSteeringTurnRadiusConstant - (1.5f / 2))) *
                                                    inputManager.SteeringInput;
                wheelColliders.fLWheel.steerAngle = Mathf.Rad2Deg *
                                                    Mathf.Atan(
                                                        2.55f / (ackermanSteeringTurnRadiusConstant + (1.5f / 2))) *
                                                    inputManager.SteeringInput;
            }
            else
            {
                wheelColliders.fRWheel.steerAngle = 0f;
                wheelColliders.fLWheel.steerAngle = 0f;
            }
        }


        private void ApplyBrake()
        {
            wheelColliders.fRWheel.brakeTorque = (inputManager.IsBraking ? 1f : 0f) * carProps.BrakePower * .7f * Time.deltaTime;
            wheelColliders.fLWheel.brakeTorque = (inputManager.IsBraking ? 1f : 0f) * carProps.BrakePower * .7f * Time.deltaTime;

            wheelColliders.rLWheel.brakeTorque = (inputManager.IsBraking ? 1f : 0f) * carProps.BrakePower * .3f * Time.deltaTime;
            wheelColliders.rRWheel.brakeTorque = (inputManager.IsBraking ? 1f : 0f) * carProps.BrakePower * .3f * Time.deltaTime;
        }

        private void ApplyMotorTorque()
        {
            if (speed < carProps.MaxSpeed && !inputManager.IsBraking)
            {
                var speedFactor = Mathf.Pow(1 - (speed / carProps.MaxSpeed), 1 - acceleration);
                var appliedMotorPower = carProps.MotorPower * gears[currentGear].GearTorqueRatio * Time.deltaTime *
                                        speedFactor * inputManager.GasInput;
                wheelColliders.rRWheel.motorTorque =
                    appliedMotorPower;
                wheelColliders.rLWheel.motorTorque = appliedMotorPower;
            }

            else
            {
                wheelColliders.rRWheel.motorTorque =
                    0f;
                wheelColliders.rLWheel.motorTorque = 0f;
            }
        }

        private void UpdateWheels()
        {
            UpdateAWheel(wheelColliders.fLWheel, wheelMeshes.fLWheel);
            UpdateAWheel(wheelColliders.fRWheel, wheelMeshes.fRWheel);
            UpdateAWheel(wheelColliders.rRWheel, wheelMeshes.rRWheel);
            UpdateAWheel(wheelColliders.rLWheel, wheelMeshes.rLWheel);
        }

        private void ApplyDrifting()
        {
            ApplyDriftingForFrontWheel(wheelColliders.fLWheel);
            ApplyDriftingForFrontWheel(wheelColliders.fRWheel);
            ApplyDriftingForFrontWheel(wheelColliders.rRWheel);
            ApplyDriftingForFrontWheel(wheelColliders.rLWheel);
        }

        private void ApplyDriftingForFrontWheel(WheelCollider wheelCollider)
        {
            var fwFriction = wheelCollider.forwardFriction;
            var swFriction = wheelCollider.sidewaysFriction;

            fwFriction.stiffness = inputManager.IsBraking
                ? Mathf.Lerp(fwFriction.stiffness, driftStiffness, Time.deltaTime * driftStiffnessChangeLerpMultiplier)
                : Mathf.Lerp(fwFriction.stiffness, originalFwStiffness,
                    Time.deltaTime * driftStiffnessChangeLerpMultiplier);
            wheelCollider.forwardFriction = fwFriction;

            swFriction.stiffness = inputManager.IsBraking
                ? Mathf.Lerp(swFriction.stiffness, driftStiffness, Time.deltaTime * driftStiffnessChangeLerpMultiplier)
                : Mathf.Lerp(swFriction.stiffness, originalSwStiffness,
                    Time.deltaTime * driftStiffnessChangeLerpMultiplier);
            wheelCollider.sidewaysFriction = swFriction;
        }


        private void UpdateAWheel(WheelCollider wheelCol, MeshRenderer wheelMeshRenderer)
        {
            Quaternion rot;
            Vector3 pos;
            wheelCol.GetWorldPose(out pos, out rot);

            var tr = wheelMeshRenderer.transform;
            tr.position = pos;
            tr.rotation = rot;
        }

        private void ShiftGears()
        {
            rpm = Mathf.Lerp(idleRPM, maxRpm, speed / gears[currentGear].GearSpeedLimit);
            if (currentGear < gears.Count - 1 && speed > gears[currentGear].GearSpeedLimit + shiftSpeedThreshold)
            {
                currentGear++;
            }
            else if (currentGear > 0 && speed < gears[currentGear - 1].GearSpeedLimit - shiftSpeedThreshold)
            {
                currentGear--;
            }
        }

        private void RecoverCar()
        {
            recoverTimeChecker = 0f;
            if (forceRecover)
            {
                forceRecoverPos.y += waypointYOffset;
                tr.position = forceRecoverPos;
                tr.rotation = forceRecoverRot;
                carRb.velocity = Vector3.zero;
                carRb.angularVelocity = Vector3.zero;
            }
            else
            {
                
                var waypointTr = TrackWaypointsManager.Instance.Waypoints[currentWaypointIndex].transform;
                var waypointPos = waypointTr.position;
                waypointPos.y += waypointYOffset;
                tr.position = waypointPos;
                tr.rotation = waypointTr.rotation;
                carRb.velocity = Vector3.zero;
                carRb.angularVelocity = Vector3.zero;
            }
        }

        private void CheckRecover()
        {
            var dotProduct = Vector3.Dot(tr.up, Vector3.up);

            var waypointTr = TrackWaypointsManager.Instance.Waypoints[currentWaypointIndex].transform;
            forceRecover = Vector3.Distance(waypointTr.position, tr.position) >= waypointCheckerRadiusRecoverMax;
            forceRecoverPos = waypointTr.position;
            forceRecoverRot = waypointTr.rotation;
            var recoverNeeded = dotProduct <= recoverAngle || speed <= recoverSpeed || forceRecover;
            
            if (recoverNeeded)
            {
                recoverTimeChecker += Time.deltaTime;
            }

            else
            {
                recoverTimeChecker = 0f;
            }

            if (recoverTimeChecker >= recoverTime)
            {
                RecoverCar();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, waypointCheckerRadius);
        }
    }
}