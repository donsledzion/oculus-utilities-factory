using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [SerializeField, Range(0f, 5f)] private float smooth = 2f;

    public bool inverseX = false, inverseY = false, inverseZ = false;
    public float addX = 0, addY = 0, addZ = 0;
    public bool lockX = false, lockY = false, lockZ = false;
    private void Start()
    {
        if (!camera)
            camera = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 dir = camera.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), Time.time * smooth);
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x * (lockX ? 0 : (inverseX ? -1 : 1) + addX),
            transform.eulerAngles.y * (lockY ? 0 : (inverseY ? -1 : 1) + addY),
            transform.eulerAngles.z * (lockZ ? 0 : (inverseZ ? -1 : 1) + addZ));
    }
}
