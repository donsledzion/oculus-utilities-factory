using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideWhenClose : MonoBehaviour
{
    [Tooltip("Object which visibility will be switched.")]
    [SerializeField] GameObject objectToHide;
    [Tooltip("Object that cause visibility switch on approach.")]
    [SerializeField] GameObject objectToHideFrom;
    [Space]
    [Tooltip("Maximum distance between objects to keep visibility turned on.")]
    [SerializeField] float hideRange = 1f;
    [Tooltip("Mark as checked in case to auto reenable hidden object when trigger moves away." +
        " Note that using with teleporting (auto hiding) objects may cause unexpected behaviour.")]
    [SerializeField] bool autoReenable = false;

    void Start()
    {
        if (!objectToHideFrom)
            objectToHideFrom = GameObject.Find("XR Rig_Player");
        if (!objectToHideFrom) return;
    }

    void Update()
    {
        
        if (DistanceBetween() < hideRange)
            Hide();
        else if(autoReenable)
            Show();
    }

    void Hide()
    {
        if (objectToHide.activeSelf)
            objectToHide.SetActive(false);
    }

    void Show()
    {
        if(!objectToHide.activeSelf)
            objectToHide.SetActive(true);
    }

    float DistanceBetween()
    {
        return (new Vector3(objectToHide.transform.position.x, 0, objectToHide.transform.position.z) -
            new Vector3(objectToHideFrom.transform.position.x, 0, objectToHideFrom.transform.position.z))
            .magnitude;
    }
            
}
