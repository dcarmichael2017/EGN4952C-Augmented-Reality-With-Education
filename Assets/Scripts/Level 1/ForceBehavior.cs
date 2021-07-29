using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBehavior : MonoBehaviour
{

    public Rigidbody cannonball;

    // Start is called before the first frame update
    void Start()
    {
        cannonball = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        cannonball.AddForce(transform.forward * 10f);
    }
}
