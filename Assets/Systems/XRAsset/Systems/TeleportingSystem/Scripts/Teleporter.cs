using UnityEngine;
using UnityEngine.Events;

namespace TeleportingSystem
{
    public abstract class Teleporter : MonoBehaviour, ITeleport
    {
        [SerializeField] bool isLocked = false;
        [SerializeField] protected bool alwaysVisible = false;
        [SerializeField] protected UnityEvent onTeleport = default;

        public bool IsLocked { get => isLocked; set => isLocked = value; }

        protected virtual void Start()
        {
            Teleporting.Instance.onChangeVisabilityTeleportsEvent += IsVisable;
            if (!alwaysVisible)
                IsVisable(false);
        }

        protected virtual void OnDestroy()
        {
            if (Teleporting.Instance)
                Teleporting.Instance.onChangeVisabilityTeleportsEvent -= IsVisable;
        }

        public abstract void IsVisable(bool isVisable);

        public abstract void TeleportTo(Transform transformToTeleport, Vector3 teleportTo, Quaternion rotationTo);
    }
}