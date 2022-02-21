using UnityEngine;

namespace TeleportingSystem
{
    public interface ITeleport
    {
        /// <summary>
        /// Teleportuje obiekt we wskazany punkt ze wskazaną rotacja
        /// </summary>
        /// <param name="transformToTeleport">Przenoszony obiekt</param>
        /// <param name="teleportTo">Docelowa pozycja</param>
        /// <param name="rotationTo">Docelowa rotacja</param>
        void TeleportTo(Transform transformToTeleport, Vector3 teleportTo, Quaternion rotationTo);

        /// <summary>
        /// Włącza/Wyłącza widoczność teleportu
        /// </summary>
        void IsVisable(bool isVisable);
    }
}