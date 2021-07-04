using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    private float damageScale = 50;
    private double score = 0;
    private int difficultyLevel = 1;

    public GameObject healthBarCanvas;
    public TMP_Text scoreText;

    //null check
    public event Action<float> OnHealthPctChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarCanvas.gameObject.transform.localScale = new Vector3(0, 0, 0);
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
        score = double.Parse(scoreText.text);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < maxHealth)
            healthBarCanvas.gameObject.transform.localScale = new Vector3((float)0.01, (float)0.01, (float)0.01);

        if (currentHealth <= 0)
        {
            updateScore();
            Destroy(gameObject);
        }

        //Update score variable if another enemy is damaged
        if (scoreText.havePropertiesChanged)
        {
            updateScore();
        }

        updateScore();


    }

    public void ModifyHealth(float amount) 
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        //Update score with damage dealt
        score -= (double) Math.Round(amount); //amount is negative so subtract to get positive
        scoreText.text = score.ToString();
        updateScore();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Vector3 vel = collision.relativeVelocity;
            float damageModifier = -0.5f * (float) Math.Log(vel.magnitude)/difficultyLevel; //divided by enemy level (1, 2, 3)
            ModifyHealth(damageScale * damageModifier);
        }

        else if (collision.gameObject.CompareTag("Castle"))
        {
            Destroy(gameObject);
        }
    }

    public void updateScore()
    {
        //Update score variable
        score = double.Parse(scoreText.text);
    }
}
