using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeleportingSystem
{
    public class TeleportArea : Teleporter, ITeleport
    {
        [Space]
        [SerializeField] MeshRenderer meshRenderer = default;
        [SerializeField] Material lockedMaterial = default;
        [SerializeField] Material defaultMaterial = default;

        public override void IsVisable(bool isVisable)
        {
            gameObject.SetActive(isVisable);
            meshRenderer.material = IsLocked ? lockedMaterial : defaultMaterial;
        }

        public override void TeleportTo(Transform transformToTeleport, Vector3 teleportTo, Quaternion rotationTo)
        {
            transformToTeleport.position = teleportTo;
            base.onTeleport?.Invoke();
        }
    }
}