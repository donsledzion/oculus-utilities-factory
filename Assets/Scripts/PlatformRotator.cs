using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float currentRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += rotationSpeed*Time.deltaTime;
        gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, currentRotation, 0));
    }
}
