using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Rigidbody rb;

    private Transform CannonBall_Spawn = null;

    public GameObject Cannonball = null;

    public float Power = 2.0f;

    public float sidewaysMove = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        CannonBall_Spawn = transform.Find("CannonBall_Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShootCannonBall();
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysMove * Time.deltaTime, 0, 0);

        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysMove * Time.deltaTime, 0, 0);

        }

    }

    private void ShootCannonBall()
    {
        GameObject cannonball = Instantiate(Cannonball, CannonBall_Spawn.position, Quaternion.identity);

        Rigidbody rb = cannonball.AddComponent<Rigidbody>();

        SphereCollider SC = cannonball.AddComponent<SphereCollider>();

        rb.velocity = Power * CannonBall_Spawn.forward;

    }
}