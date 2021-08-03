using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MeteorController : MonoBehaviour
{

    public CastleController castleController;
    public CastleHealth castleHealth;

    private TMP_Text scoreText;
    private int difficultyLevel = 1;
    private double score = 0;

    // Start is called before the first frame update
    void Start()
    {

        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();

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
        /*if (collision.gameObject.CompareTag("Ground"))
        {
            //if (castleController.WithinRange(this.transform))
            if (collision.gameObject.name == ("MDZ"))
            {
                FindObjectOfType<AudioManager>().Play("MeteorCollide");
                castleHealth.ModifyHealth(-10 * difficultyLevel);
                //Destroy cannonball on collision
                Destroy(gameObject);
            }
            GameValues.score += 20;
            Destroy(gameObject);
        }*/

        if (collision.gameObject.name == ("MDZ"))
        {
            FindObjectOfType<AudioManager>().Play("MeteorCollide");
            castleHealth.ModifyHealth(-10 * difficultyLevel);
            //Destroy cannonball on collision
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            updateScoreText(20);
            Destroy(gameObject);
        }

    }

    public void updateScore()
    {
        GameValues.score = score;
    }

    public void updateScoreText(double amount)
    {
        //Update score with damage dealt
        score = GameValues.score;
        score += amount;
        scoreText.text = score.ToString();
        updateScore();
    }
}
