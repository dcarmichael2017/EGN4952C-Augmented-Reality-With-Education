using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using DigitalRuby.PyroParticles;

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

    public float lookRadius = 20f;

    Transform target;
    NavMeshAgent agent;
    public float actionTimer;

    public Transform fireballSpawn;
    public GameObject FireBallPrefab;
    private FireBaseScript currentPrefabScript;

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
            agent.speed = 0;
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
        //if (currentHealth < maxHealth)
        // healthBarCanvas.gameObject.transform.localScale = new Vector3((float)0.01, (float)0.01, (float)0.01);

        if (actionTimer > (float)0.01)
            healthBarCanvas.gameObject.transform.localScale = new Vector3((float)0.01, (float)0.01, (float)0.01);

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius && distance >= lookRadius / 2)
        {
            agent.SetDestination(target.position);
            agent.speed = 1;
            actionTimer = 0;
        }
        else if (distance <= lookRadius / 2)
        {
            agent.speed = 0;
            actionTimer += Time.deltaTime;
            float currentHealthPct = actionTimer / (float)4;
            OnHealthPctChanged(currentHealthPct);
            if (actionTimer >= 4)
            {
                animator.SetTrigger("Shoot");
                actionTimer = 0;
                ShootFireBall();
            }
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

    public void StartLevel()
    {
        agent.speed = 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, lookRadius / 2);
    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        //OnHealthPctChanged(currentHealthPct);

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
            float damageModifier = -0.1f * (float)Math.Log(vel.magnitude) / difficultyLevel; //divided by enemy level (1, 2, 3)
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

    private void ShootFireBall()
    {

        GameObject currentPrefabObject = GameObject.Instantiate(FireBallPrefab);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
                if (projectileScript != null)
                {
                    // make sure we don't collide with other fire layers
                    projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
                }

                currentPrefabObject.transform.position = fireballSpawn.position;
                currentPrefabObject.transform.rotation = fireballSpawn.rotation;

            }
        }
    }
}
