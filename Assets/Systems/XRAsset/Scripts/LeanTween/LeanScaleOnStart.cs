using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanScaleOnStart : MonoBehaviour
{
    [SerializeField] Vector3 startScale = Vector3.zero;
    [SerializeField] float transitionTime = 2f;
    [SerializeField] LeanTweenType easeType = LeanTweenType.linear;

    private void OnEnable()
    {
        transform.localScale = startScale;
        LeanTween.scale(this.gameObject, Vector3.one, transitionTime).setEase(easeType);
    }
}
