using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public float speed = 25f;
    public Rigidbody barrel;

    public bool leftArrow;
    public bool rightArrow;

    public int leftConstraint;
    public int rightConstraint;

    private Rigidbody rb; //base of cannon
    private float dirX;

    private float scale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftConstraint = 0;
        rightConstraint = 5;
    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxis("Horizontal") * Time.fixedDeltaTime * speed;

        if (leftArrow && rb.position.x >= leftConstraint)
        {
            x = -Time.fixedDeltaTime * speed * scale;
        }

        if (rightArrow && rb.position.x <= rightConstraint)
        {
            x = Time.fixedDeltaTime * speed * scale;
        }

        //left/right button movement
        rb.MovePosition(rb.position + Vector3.right * x);
        barrel.MovePosition(barrel.position + Vector3.right * x);

        //accelerometer movement
        dirX = Input.acceleration.x * Time.fixedDeltaTime * speed * scale;
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
