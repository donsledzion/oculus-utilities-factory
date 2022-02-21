using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWorldObject : MonoBehaviour
{
    [SerializeField] Transform followObject = default;
    [SerializeField] Vector3 offset = Vector3.down;

    [SerializeField] bool rotateY = true;

    private Vector3 eulerAngles = Vector3.zero;

    private void Update()
    {
        if (rotateY) eulerAngles.y = followObject.eulerAngles.y;

        transform.position = followObject.position + offset;
        transform.eulerAngles = eulerAngles;
    }
}
