using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonActions : MonoBehaviour
{
    public Rigidbody rb;

    public Transform CannonBall_Spawn;

    public GameObject Cannonball = null;

    public float Power = 12.0f;

    public float sidewaysMove = 100.0f;

    private float scale = 0.35f;

    // Start is called before the first frame update
    void Start()
    {
        //CannonBall_Spawn = transform.Find("CannonBall_Spawn");
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
        GameObject cannonball = Instantiate(Cannonball, CannonBall_Spawn.position, Quaternion.identity);

        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        SphereCollider SC = cannonball.AddComponent<SphereCollider>();

        rb.velocity = Power * CannonBall_Spawn.forward * scale;

    }
}