using System.Linq;
using System;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using XRInputManager;

namespace TeleportingSystem
{
    public class Teleporting : MonoBehaviour
    {
        public static Teleporting Instance { get; private set; }
        private const float LERP_SMOOTH = 10f;
        public event System.Action<bool> onChangeVisabilityTeleportsEvent;

        [SerializeField] InputHelpers.Button teleportButtonLeft = InputHelpers.Button.PrimaryAxis2DUp;
        [SerializeField] InputHelpers.Button teleportButtonRight = InputHelpers.Button.SecondaryAxis2DUp;
        [SerializeField] LayerMask teleportingMask = default;
        [SerializeField] LayerMask teleportingCheckLayer = default;
        [SerializeField] float maxTeleportDistance = 15f;

        [Space]
        [SerializeField] Transform teleportObject = default;
        [SerializeField] XRController leftHand = default;
        [SerializeField] XRController rightHand = default;

        [Space]
        [Header("Pointers")]
        [SerializeField] Transform destinationPoint = default;
        [SerializeField] LineRenderer linerRenderer = default;

        [Space]
        [SerializeField] Material goodLineRendererMaterial = default;
        [SerializeField] Material badLineRendererMaterial = default;
        [Space]
        [SerializeField] Material goodDestinationPoint = default;
        [SerializeField] Material badDestinationPoint = default;

        private bool isEnabled = true;
        private Vector3 raycastPosition = Vector3.zero;
        private MeshRenderer destinationPointMeshRenderer;


        // ==========================================================================
        private MeshFilter destinationPointMeshFilter;
        [Space]
        [SerializeField] Mesh validDestinationMesh = default;
        [SerializeField] Mesh invalidDestinationMesh = default;
        [Space]
        private TeleportCurve teleportCurve; // Needed to add teleportCurve component
        // ==========================================================================

        /// <summary>
        /// Flaga informująca, czy teleportacja jest możliwa
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                if (!isEnabled)
                {
                    ExitTeleport();
                }
            }
        }

        private bool IsTeleporting { get; set; }
        private Transform TeleportPointer { get; set; }
        private Teleporter Teleporter { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Reset()
        {
            if (teleportObject == null) teleportObject = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void OnDisable()
        {
            ChangeVisabilityTeleports(false);
        }

        private void Start()
        {
            destinationPointMeshRenderer = destinationPoint.GetComponent<MeshRenderer>();
            //==================================================================================
            destinationPointMeshFilter = destinationPoint.GetComponent<MeshFilter>();
            teleportCurve = GetComponent<TeleportCurve>(); // Assigning teleportCurve reference
            //==================================================================================
        }

        private void Update()
        {
            if (IsTeleporting) OnTeleport();

            Transform teleportHand = null;
            if (InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(rightHand.controllerNode), teleportButtonRight, out bool isPressedRight, .7f))
            {
                if (isPressedRight) teleportHand = rightHand.transform;
            }

            if (InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(leftHand.controllerNode), teleportButtonLeft, out bool isPressedLeft, .7f))
            {
                if (isPressedLeft) teleportHand = leftHand.transform;
            }

            if (!IsTeleporting && teleportHand != null) OnStartTeleport(teleportHand);
            else if (IsTeleporting && teleportHand == null) OnEndTeleport();

        }

        private void ChangeVisabilityTeleports(bool isVisable)
        {
            onChangeVisabilityTeleportsEvent?.Invoke(isVisable);
        }

        private void OnStartTeleport(Transform teleportHand)
        {
            ChangeVisabilityTeleports(true);
            ChangeTeleportingColor(false);
            IsTeleporting = true;
            TeleportPointer = teleportHand;
            linerRenderer.SetPosition(0, TeleportPointer.position);
        }

        private void OnTeleport()
        {
            RaycastHit hit;
            if (Physics.Raycast(TeleportPointer.position, TeleportPointer.forward, out hit, maxTeleportDistance, teleportingCheckLayer.value))
            {
                bool isOnTeleportLayermask = hit.transform.gameObject.InOnLayerMask(teleportingMask);
                float distanceFromLastPosition = (raycastPosition - hit.point).sqrMagnitude;

                raycastPosition = hit.point;
                TeleportingEfectVisability(true);

                if (isOnTeleportLayermask)
                {
                    Teleporter foundTeleporter = hit.transform.GetComponent<Teleporter>();

                    if (!foundTeleporter.IsLocked)
                    {
                        Teleporter = foundTeleporter;
                        ChangeTeleportingColor(true);
                    }
                    else
                    {
                        Teleporter = null;
                        ChangeTeleportingColor(false);
                    }
                }
                else
                {
                    Teleporter = null;
                    ChangeTeleportingColor(false);
                }
                // ===================================================================================
                // Drawing marker on the walls - perpendicular to it's surface
                destinationPoint.gameObject.transform.rotation = 
                    Quaternion.FromToRotation(destinationPoint.gameObject.transform.up, hit.normal)* destinationPoint.gameObject.transform.rotation;
                // ===================================================================================


            }
            else
            {
                Teleporter = null;
                TeleportingEfectVisability(false);
            }

            /*
             * Content moved from here to DrawTrace() method
             */
            DrawTrace();
        }

        private void OnEndTeleport()
        {
            if (IsTeleporting && Teleporter != null)
            {
                Teleporter.TeleportTo(teleportObject, raycastPosition, teleportObject.rotation);
            }
            ExitTeleport();
        }

        private void ExitTeleport()
        {
            ChangeVisabilityTeleports(false);
            TeleportingEfectVisability(false);
            TeleportPointer = null;
            IsTeleporting = false;
            raycastPosition = Vector3.zero;
            Teleporter = null;
        }

        private void TeleportingEfectVisability(bool isVisable)
        {
            destinationPoint.gameObject.SetActive(isVisable);
            linerRenderer.enabled = isVisable;
        }

        private void ChangeTeleportingColor(bool isGood)
        {

            if (isGood)
            {
                linerRenderer.material = goodLineRendererMaterial;
                // ===============================================================
                destinationPointMeshFilter.mesh = validDestinationMesh;
                // ===============================================================
                destinationPointMeshRenderer.material = goodDestinationPoint;
            }
            else
            {
                linerRenderer.material = badLineRendererMaterial;
                // ===============================================================
                destinationPointMeshFilter.mesh = invalidDestinationMesh;
                // ===============================================================
                destinationPointMeshRenderer.material = badDestinationPoint;
            }
        }

        private void DrawTrace()
        {
            Vector3 lerpDestination = Vector3.Lerp(destinationPoint.position, raycastPosition, LERP_SMOOTH * Time.deltaTime);
            if (linerRenderer.positionCount==2)
            {
                linerRenderer.SetPosition(0, TeleportPointer.position);
                linerRenderer.SetPosition(1, lerpDestination);
            } else if(linerRenderer.positionCount>2)
            {
                teleportCurve.DrawCurve(TeleportPointer.position, lerpDestination);
            }
            destinationPoint.position = lerpDestination;
        }
    }

}