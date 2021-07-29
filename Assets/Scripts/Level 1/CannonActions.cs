using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActions : MonoBehaviour
{
    public Rigidbody rb;

    public Transform CannonBall_Spawn;
    public Rigidbody barrel;

    public GameObject Cannonball = null;

    public float Power = 35.0f;

    public float sidewaysMove = 100.0f;

    private PowerAndSize powerAndSize;

    private float scale = 1f;

    private float nextShot = 0f;
    private float fireRate = 0.75f; //seconds

    // Start is called before the first frame update
    void Start()
    {
        //CannonBall_Spawn = transform.Find("CannonBall_Spawn");

        powerAndSize = GetComponent<PowerAndSize>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShootCannonBall();
        }

        // Found a more efficient way to move
        /*if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysMove * Time.deltaTime, 0, 0);

        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysMove * Time.deltaTime, 0, 0);

        }*/

    }

    public void ShootCannonBall()
    {
        if (Time.time > nextShot)
        {
            //next time cannon can shoot (in seconds)
            nextShot = Time.time + fireRate;

            GameObject cannonball = Instantiate(Cannonball, CannonBall_Spawn.position, Quaternion.identity);

            Rigidbody cannonballRB = cannonball.GetComponent<Rigidbody>();

            cannonballRB.mass = powerAndSize.getCannonBallMass();

            //Rigidbody rb = cannonball.AddComponent<Rigidbody>();

            SphereCollider SC = cannonball.AddComponent<SphereCollider>();


            /*if (barrel.transform != null) 
            {
                rb.AddForce(Power * CannonBall_Spawn.forward * scale);
            }*/

            //rb.velocity = Power * CannonBall_Spawn.forward * scale; //original

            //cannonballRB.velocity = Power * CannonBall_Spawn.forward * scale;

            cannonballRB.AddForce(Power * CannonBall_Spawn.forward * scale);
        }

    }

    public void Buttons(string buttons)
    {
        switch (buttons)
        {
            case "PLUS":
                if (Power < 200)
                    Power = Power + 5;

                break;

            case "MINUS":
                if (Power > 35)
                    Power = Power - 5;

                break;
        }
    }
}