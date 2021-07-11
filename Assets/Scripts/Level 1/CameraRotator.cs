using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Rigidbody cannon;

    void Start()
    {
        transform.position = cannon.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
