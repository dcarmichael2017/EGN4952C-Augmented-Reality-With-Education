using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    public float speed = 25f;
    private Rigidbody rb;

    private float dirY;

    private float rotationX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("up") && rotationX > 72f)
        {
            //rotate up
            rotationX = rotationX - 1f;
            rb.rotation = Quaternion.Euler(rotationX, 0, 0);
        } else if (Input.GetKeyDown("down") && rotationX < 90f)
        {
            //rotate down
            rotationX = rotationX + 1f;
            rb.rotation = Quaternion.Euler(rotationX, 0, 0);
        }

    }
}
