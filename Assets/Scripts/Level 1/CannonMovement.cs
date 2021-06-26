using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public float speed = 25f;
    public Rigidbody barrel;

    private Rigidbody rb; //base of cannon
    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //left/right button movement
        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + Vector3.right * x);
        barrel.MovePosition(barrel.position + Vector3.right * x);

        //accelerometer movement
        dirX = Input.acceleration.x * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + Vector3.right * dirX);
        barrel.MovePosition(barrel.position + Vector3.right * dirX);

        //UI Button movement
    }



}
