using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    public GameObject healthBarCanvas;

    //null check
    public event Action<float> OnHealthPctChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBarCanvas.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < maxHealth)
            healthBarCanvas.gameObject.transform.localScale = new Vector3((float)0.01, (float)0.01, (float)0.01);

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    public void ModifyHealth(int amount) 
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            ModifyHealth(-50);
        }

        else if (collision.gameObject.CompareTag("Castle"))
        {
            Destroy(gameObject);
        }
    }
}
