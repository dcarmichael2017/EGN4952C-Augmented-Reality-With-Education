using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    public CastleController castleController;
    public CastleHealth castleHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (castleController.WithinRange(this.transform))
            {
                castleHealth.ModifyHealth(-10);
                //Destroy cannonball on collision
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }

    }
}
