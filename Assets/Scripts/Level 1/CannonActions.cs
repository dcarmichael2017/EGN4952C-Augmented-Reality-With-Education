using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float fireRate = 0.75f; //seconds
    private float counter = 0f;
    private float skillCounter = 0f;
    private float skillRate = 5f;

    public Button fireButton;
    public Button skillButton;

    // Start is called before the first frame update
    void Start()
    {
        //CannonBall_Spawn = transform.Find("CannonBall_Spawn");

        powerAndSize = GetComponent<PowerAndSize>();
        counter = fireRate;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShootCannonBall();
        }

        counter += Time.deltaTime;
        skillCounter += Time.deltaTime;

        if (counter < fireRate)
            fireButton.interactable = false;
        else
            fireButton.interactable = true;

        try
        {
            if (skillCounter < skillRate)
                skillButton.interactable = false;
            else
                skillButton.interactable = true;
        }
        catch (System.Exception e) { }

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
        if (counter >= fireRate)
        {
            //next time cannon can shoot (in seconds)
            counter = 0;

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
            case "SKILL":
                skillCounter = 0;
                float temp = Power;
                Power = 250;
                ShootCannonBall();
                Power = temp;
                break;
        }
    }
}