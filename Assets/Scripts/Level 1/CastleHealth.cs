using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        foregroundImage.fillAmount = ((float)currentHealth / (float)maxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ModifyHealth(-10);

        }
    }
}
