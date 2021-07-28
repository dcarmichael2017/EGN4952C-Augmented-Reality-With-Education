using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    private float damageScale = 50;
    private double score = 0;
    private int difficultyLevel = 1;

    private Vector3 previousPosition;
    public float CurSpeed;

    public GameObject healthBarCanvas;
    private TMP_Text scoreText;

    private Animator animator;

    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;

    //null check
    public event Action<float> OnHealthPctChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarCanvas.gameObject.transform.localScale = new Vector3(0, 0, 0);

        try
        {
            animator = GetComponent<Animator>();
            scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
            score = double.Parse(scoreText.text);
            agent = GetComponent<NavMeshAgent>();
            target = PlayerManager.instance.player.transform;
        }
        catch (Exception e) { Debug.Log(e); }

        //set difficultyLevel (higher level reduces damage dealt by cannonballs to enemies and increases damage dealt to castle)
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
        if (currentHealth < maxHealth)
            healthBarCanvas.gameObject.transform.localScale = new Vector3((float)0.01, (float)0.01, (float)0.01);

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }


        //Update score variable if another enemy is damaged
        try
        {
            if (scoreText.havePropertiesChanged)
            {
                updateScore();
            }

            updateScore();
        }

        catch (Exception e) { Debug.Log(e); }

        Vector3 CurMove = transform.position - previousPosition;
        CurSpeed = CurMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
        animator.SetFloat("Speed", CurSpeed);


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

        //Update score with damage dealt
        score -= (double)Math.Round(amount); //amount is negative so subtract to get positive
        scoreText.text = score.ToString();
        updateScore();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //Damage calculations
            Vector3 vel = collision.relativeVelocity;
            float damageModifier = -0.5f * (float)Math.Log(vel.magnitude) / difficultyLevel; //divided by enemy level (1, 2, 3)
            ModifyHealth(damageScale * damageModifier);

            //If health is above zero, play hit animation
            if (currentHealth > 0)
                animator.SetTrigger("Hit");

            else if (currentHealth <= 0)
            {
                updateScore();
                animator.SetTrigger("Dead");
                GetComponent<NavMeshAgent>().speed = 0;
                GetComponent<Animator>().applyRootMotion = false;
                GetComponent<CapsuleCollider>().radius = (float)0.01;
                float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;

                Destroy(gameObject, animTime + 2);
            }

            //Destroy cannonball on collision
            Destroy(collision.collider.gameObject);
        }

        else if (collision.gameObject.CompareTag("Castle"))
        {
            //add hit animation here?
            Destroy(gameObject);
        }
    }

    public void updateScore()
    {
        //Update score variable
        score = double.Parse(scoreText.text);
        GameValues.score = score;
    }
}
