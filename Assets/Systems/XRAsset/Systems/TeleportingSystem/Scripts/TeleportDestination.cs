using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace TeleportingSystem
{
    public class TeleportDestination : Teleporter, ITeleport
    {
        private static readonly Vector3 DEBUG_OFFSET = new Vector3(0, .25f, 0);

        [SerializeField] bool rotateToForward = false;

        [Space]
        [SerializeField] MeshRenderer meshRenderer = default;
        [SerializeField] Transform destinationPoint = default;
        [SerializeField] GameObject arrows = default;
        [SerializeField] Material lockedMaterial = default;
        [SerializeField] Material defaultMaterial = default;

        public override void IsVisable(bool isVisable)
        {
            gameObject.SetActive(isVisable);

            if (IsLocked)
            {
                meshRenderer.material = lockedMaterial;
                arrows.SetActive(false);
            }
            else
            {
                meshRenderer.material = defaultMaterial;
                arrows.SetActive(true);
            }
        }

        public override void TeleportTo(Transform transformToTeleport, Vector3 teleportTo, Quaternion rotationTo)
        {
            transformToTeleport.position = destinationPoint.position;
            if (rotateToForward)
            {
                transformToTeleport.rotation = destinationPoint.rotation;

                var xrManager = XRGeneralSettings.Instance.Manager;
                var xrLoader = xrManager.activeLoader;
                var xrInput = xrLoader.GetLoadedSubsystem<XRInputSubsystem>();

                xrInput.TryRecenter();
            }

            base.onTeleport?.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (rotateToForward)
            {
                DrawArrow.ForGizmo(destinationPoint.position + DEBUG_OFFSET, destinationPoint.forward, Color.blue);
            }
        }
    }
}