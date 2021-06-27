using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public float speed = 25f;
    public Rigidbody barrel;

    public bool leftArrow;
    public bool rightArrow;

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

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        if (leftArrow)
        {
            x = -Time.fixedDeltaTime * speed;
        }

        if (rightArrow)
        {
            x = Time.fixedDeltaTime * speed;
        }

        //left/right button movement
        
        rb.MovePosition(rb.position + Vector3.right * x);
        barrel.MovePosition(barrel.position + Vector3.right * x);

        //accelerometer movement
        dirX = Input.acceleration.x * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.position + Vector3.right * dirX);
        barrel.MovePosition(barrel.position + Vector3.right * dirX);

        //UI Button movement
    }

    public void Buttons(string buttons) 
    {
        switch (buttons) 
        {
            case "LEFTARROWDOWN":
                leftArrow = true;

                break;

            case "LEFTARROWUP":
                leftArrow = false;

                break;

            case "RIGHTARROWDOWN":
                rightArrow = true;

                break;

            case "RIGHTARROWUP":
                rightArrow = false;

                break;
        }
    }
}
