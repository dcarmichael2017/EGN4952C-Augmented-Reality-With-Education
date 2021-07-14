using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public float speed = 25f;
    public Rigidbody barrel;
    public GameObject cameraRotator;

    public bool leftArrow;
    public bool rightArrow;

    private Rigidbody rb; //base of cannon
    private float dirX;

    private float scale = 0.01f;

    private float rotationZ = 0f;

    private float x = 0;

    private float cameraOffset = 112.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rotationZ = rb.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {

        x = Input.GetAxis("Horizontal") * Time.deltaTime * speed * 10f;

        if (leftArrow)
        {
            x = Time.deltaTime * speed;
            rotationZ -= 2f * x;
            //rb.transform.Rotate(0, 0, rotationZ);
            //cameraRotator.transform.Rotate(0, rotationZ, 0);
            rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles + new Vector3(0, 0, -1));
            barrel.transform.rotation = Quaternion.Euler(new Vector3(barrel.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z) + new Vector3(0, 0, -1)); //use the cannon's y and z rotation
            if (cameraRotator != null)
            {
                cameraRotator.transform.rotation = Quaternion.Euler(cameraRotator.transform.rotation.eulerAngles + new Vector3(0, -1, 0));
            }

            //rb.transform.rotation = Quaternion.Euler(-90, 180, rotationZ);
            //barrel.transform.rotation = Quaternion.Euler(-90, 180, rotationZ);
            //cameraRotator.transform.rotation = Quaternion.Euler(0, rotationZ + cameraOffset, 0);
        }

        if (rightArrow)
        {
            x = Time.deltaTime * speed;
            rotationZ += 2f * x;
            //rb.transform.Rotate(0, 0, rotationZ);
            //cameraRotator.transform.Rotate(0, rotationZ, 0);
            rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles + new Vector3(0, 0, 1));
            barrel.transform.rotation = Quaternion.Euler(new Vector3(barrel.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z) + new Vector3(0, 0, 1)); //use the cannon's y and z rotation
            if (cameraRotator != null)
            {
                cameraRotator.transform.rotation = Quaternion.Euler(cameraRotator.transform.rotation.eulerAngles + new Vector3(0, 1, 0));
            }

            //rb.transform.rotation = Quaternion.Euler(-90, 180, rotationZ);
            //barrel.transform.rotation = Quaternion.Euler(-90, 180, rotationZ);
            //cameraRotator.transform.rotation = Quaternion.Euler(0, rotationZ + cameraOffset, 0);
        }

        //accelerometer movement
        //if (Input.acceleration != null)
        //{
            dirX = Input.acceleration.x * Time.fixedDeltaTime * speed;

            //movement
            //rb.MovePosition(rb.position + Vector3.right * dirX);
            //barrel.MovePosition(barrel.position + Vector3.right * dirX);

            //rotation
            rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles + new Vector3(0, 0, dirX * 1f));
            barrel.transform.rotation = Quaternion.Euler(new Vector3(barrel.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rb.transform.rotation.eulerAngles.z) + new Vector3(0, 0, dirX * 1f)); //use the cannon's y and z rotation
            //cameraRotator.transform.rotation = Quaternion.Euler(cameraRotator.transform.rotation.eulerAngles + new Vector3(0, dirX * 1, 0));
        //}

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
