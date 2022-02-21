using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;

namespace TeleportingSystem
{
    public class TeleportPoint : Teleporter, ITeleport
    {
        private static readonly Vector3 DEBUG_OFFSET = new Vector3(0, .25f, 0);

        [SerializeField] bool rotateToForward = false;

        [Space]
        [SerializeField] MeshRenderer meshRenderer = default;
        [SerializeField] GameObject arrowObject = default;
        [SerializeField] Material lockedMaterial = default;
        [SerializeField] Material defaultMaterial = default;

        public override void IsVisable(bool isVisable)
        {
            gameObject.SetActive(isVisable);

            if (IsLocked)
            {
                meshRenderer.material = lockedMaterial;
                arrowObject.SetActive(false);
            }
            else
            {
                meshRenderer.material = defaultMaterial;
                arrowObject.SetActive(true);
            }
        }

        public override void TeleportTo(Transform transformToTeleport, Vector3 teleportTo, Quaternion rotationTo)
        {
            transformToTeleport.position = transform.position;
            if (rotateToForward)
            {
                transformToTeleport.rotation = transform.rotation;

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
                DrawArrow.ForGizmo(transform.position + DEBUG_OFFSET, transform.forward, Color.blue);
            }
        }
    }
}