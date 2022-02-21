using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class FogOfCollider : MonoBehaviour
{
    private List<Collider> collideWith;

    private void Start()
    {
        collideWith = new List<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!collideWith.Contains(other)) collideWith.Add(other);

        if (collideWith.Count == 1)
            CameraFade.Instance.CurrentFadeType = CameraFade.FadeType.FadeIn;
    }

    private void OnTriggerExit(Collider other)
    {
        if (collideWith.Contains(other)) collideWith.Remove(other);

        if (collideWith.Count == 0)
            CameraFade.Instance.CurrentFadeType = CameraFade.FadeType.FadeOut;
    }
}
