using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{

    public CastleController castleController;
    public CastleHealth castleHealth;

    private int difficultyLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        //set difficultyLevel (higher level increases damage dealt to castle)
        if (GameValues.currentDifficulty == GameValues.Difficulties.Easy)
        {
            difficultyLevel = 1;
        }
        else if (GameValues.currentDifficulty == GameValues.Difficulties.Medium)
        {
            difficultyLevel = 2;
        }
        else if (GameValues.currentDifficulty == GameValues.Difficulties.Hard)
        {
            difficultyLevel = 3;
        }
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
                FindObjectOfType<AudioManager>().Play("MeteorCollide");
                castleHealth.ModifyHealth(-10 * difficultyLevel);
                //Destroy cannonball on collision
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }

    }
}
