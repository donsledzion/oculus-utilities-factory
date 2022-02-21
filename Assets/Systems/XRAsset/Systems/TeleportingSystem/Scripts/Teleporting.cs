using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using XRInputManager;

namespace TeleportingSystem
{
    public class Teleporting : MonoBehaviourSingleton<Teleporting>
    {
        private const float LERP_SMOOTH = 10f;
        public event System.Action<bool> onChangeVisabilityTeleportsEvent;

        [SerializeField] InputHelpers.Button teleportButtonLeft = InputHelpers.Button.PrimaryAxis2DUp;
        [SerializeField] InputHelpers.Button teleportButtonRight = InputHelpers.Button.SecondaryAxis2DUp;
        [SerializeField] LayerMask teleportingMask = default;
        [SerializeField] LayerMask teleportingCheckLayer = default;
        [SerializeField] float maxTeleportDistance = 15f;

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


        private Transform teleportObject = default;
        private XRController leftHand = default;
        private XRController rightHand = default;

        private bool isEnabled = true;
        private Vector3 raycastPosition = Vector3.zero;
        private MeshRenderer destinationPointMeshRenderer;

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


        private void OnDisable()
        {
            ChangeVisabilityTeleports(false);
        }

        private void Start()
        {
            teleportObject = GameObject.FindGameObjectWithTag("Player").transform;

            var controllers = FindObjectsOfType<XRController>();
            leftHand = controllers.FirstOrDefault(x => x.controllerNode == XRNode.LeftHand);
            rightHand = controllers.FirstOrDefault(x => x.controllerNode == XRNode.RightHand);

            destinationPointMeshRenderer = destinationPoint.GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (IsTeleporting) OnTeleport();

            Transform teleportHand = null;
            if (InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(rightHand.controllerNode), teleportButtonRight, out bool isPressedRight, .7f))
            {
                XRBaseInteractor interactor = rightHand.GetComponent<XRBaseInteractor>();
                if (isPressedRight && interactor.selectTarget == null) teleportHand = rightHand.transform;
            }

            if (InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(leftHand.controllerNode), teleportButtonLeft, out bool isPressedLeft, .7f))
            {
                XRBaseInteractor interactor = leftHand.GetComponent<XRBaseInteractor>();
                if (isPressedLeft && interactor.selectTarget == null) teleportHand = leftHand.transform;
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
            }
            else
            {
                Teleporter = null;
                TeleportingEfectVisability(false);
            }

            linerRenderer.SetPosition(0, TeleportPointer.position);

            Vector3 lerpDestination = Vector3.Lerp(destinationPoint.position, raycastPosition, LERP_SMOOTH * Time.deltaTime);
            linerRenderer.SetPosition(1, lerpDestination);
            destinationPoint.position = lerpDestination;
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
                destinationPointMeshRenderer.material = goodDestinationPoint;
            }
            else
            {
                linerRenderer.material = badLineRendererMaterial;
                destinationPointMeshRenderer.material = badDestinationPoint;
            }
        }
    }
}