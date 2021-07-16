using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour
{
    public float speed = 25f;
    public Rigidbody cannonBase;
    private Rigidbody rb; //barrel
    public bool upArrow;
    public bool downArrow;

    private float upConstraint;
    private float downConstraint;
    private float rotationX = -90f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        upConstraint = -72f;
        downConstraint = -90f;
    }

    // Update is called once per frame
    void Update()
    {

        //UI D-pad input
        if (upArrow && rotationX < upConstraint)
        {
            rotateUp();
        }
        if (downArrow && rotationX > downConstraint)
        {
            rotateDown();
        }


        //Arrow keys input
        if (Input.GetKeyDown("up") && rotationX < upConstraint)
        {
            rotateUp();
        } else if (Input.GetKeyDown("down") && rotationX > downConstraint)
        {
            rotateDown();
        }

    }

    public void Buttons(string buttons)
    {
        switch (buttons)
        {
            case "UPARROWDOWN":
                upArrow = true;

                break;

            case "UPARROWUP":
                upArrow = false;

                break;

            case "DOWNARROWDOWN":
                downArrow = true;

                break;

            case "DOWNARROWUP":
                downArrow = false;

                break;
        }
    }

    private void rotateUp()
    {
        float x = Time.deltaTime * speed;
        //rotate up
        rotationX = rotationX + 1f;
        rb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(1, 0, 0));
    }

    private void rotateDown()
    {
        float x = Time.deltaTime * speed;
        //rotate down
        rotationX = rotationX - 1f;
        rb.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(-1, 0, 0));
    }
}
