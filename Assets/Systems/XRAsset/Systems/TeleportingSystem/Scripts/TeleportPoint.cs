using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Management;
using UnityEngine.UI;

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

        [Space]
        [SerializeField] Color colorAvailable;
        [SerializeField] Color colorLocked;

        [Space]
        [TextArea]
        [SerializeField] string textAvailable;
        [TextArea]
        [SerializeField] string textLocked;

        [Space]
        [SerializeField] GameObject iconObject;
        [SerializeField] Mesh iconLockedMesh;
        [SerializeField] Mesh iconAvailableMesh;
        [SerializeField] Text canvasText;

        public override void IsVisable(bool isVisable)
        {
            CheckAvailability();

            gameObject.SetActive(isVisable || alwaysVisible);

            if (IsLocked)
            {
                meshRenderer.material = lockedMaterial;
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

        private void CheckAvailability()
        {
            if (IsLocked)
            {
                iconObject.GetComponent<MeshRenderer>().material = lockedMaterial;
                iconObject.GetComponent<MeshFilter>().mesh = iconLockedMesh;
                canvasText.color = colorLocked;
                if(!string.IsNullOrEmpty(textLocked)) canvasText.text = textLocked;
            }
            else
            {
                iconObject.GetComponent<MeshRenderer>().material = defaultMaterial;
                iconObject.GetComponent<MeshFilter>().mesh = iconAvailableMesh;
                canvasText.color = colorAvailable;
                if (!string.IsNullOrEmpty(textAvailable)) canvasText.text = textAvailable;
            }
        }
    }
}