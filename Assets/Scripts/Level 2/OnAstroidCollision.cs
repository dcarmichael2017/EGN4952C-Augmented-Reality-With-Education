using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAstroidCollision : MonoBehaviour
{


    //public Rigidbody rb;

    //public GameObject astroid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //if(other.gameObject.tag == "Projectile") 
        //{

            GetComponent<Rigidbody>().drag = 0;

            //Rigidbody astroidRB = astroid.GetComponent<Rigidbody>();

            //astroidRB.drag = 0;

        //}

    }
}
