using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleHealth : MonoBehaviour
{

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;
    private int difficultyLevel = 1;

    public GameObject DeathCanvas;

    public bool reloadButton = false;

    public bool mainMenuButton = false;

    // Start is called before the first frame update
    void Start()
    {   
        currentHealth = maxHealth;
        DeathCanvas.SetActive(false);

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
        if (currentHealth <= 0){
            Death();
        }
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
            ModifyHealth(-10 * difficultyLevel);

        }
    }

    private void Death(){
        DeathCanvas.SetActive(true);
    }

    public void Buttons(string buttons){
        switch (buttons)
        {
            case "RELOAD":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "MAIN":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
