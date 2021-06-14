using UnityEngine;

public class MovingForward : MonoBehaviour
{
    public Rigidbody rb;

    public float forwardSpeed = -200f;
    
    // Update is called once per frame
    void FixedUpdate()                                      //FixedUpdate is better for Physics
    {

        rb.AddForce(0, 0, forwardSpeed * Time.deltaTime);   //running toward the castle using forward force
                                                            //Time.deltaTime evens out framerate
    }
}
