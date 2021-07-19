using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Canvas FinishLevel;
    public GameObject DeathCanvas;
    DisplayStats stats;
    leaderboard leaderboards;
    bool updatedLeaderBoard = false;
    float time = 0;


    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<DisplayStats>();
        leaderboards = FinishLevel.GetComponent<leaderboard>();

    }

    // Update is called once per frame
    void Update()
    {
        //Basic method to find how many enemies are on the level at once.
        stats.SetAliveEnemies(GameObject.FindGameObjectsWithTag("Enemy").Length);
        if (stats.totalEnemiesAmount == stats.deployedEnemiesAmount && stats.aliveEnemiesAmount == 0)
#pragma warning disable CS0612 // Type or member is obsolete
            if(!updatedLeaderBoard)
            LevelComplete();
#pragma warning restore CS0612 // Type or member is obsolete

        if (FinishLevel.enabled)
        {
            if (time > 5)
            {
#pragma warning disable CS0612 // Type or member is obsolete
                leaderboards.GetScoresForLeaderBoard();
#pragma warning restore CS0612 // Type or member is obsolete
                time = 0;
            }
            else
            {
                time += Time.deltaTime;
            }
        }
    }

    public void Buttons(string buttons)
    {
        switch (buttons)
        {
            case "RELOAD":
                FinishLevel.enabled = false;
                DeathCanvas.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "MAIN":
                FinishLevel.enabled = false;
                DeathCanvas.SetActive(false);
                SceneManager.LoadScene("MainMenu");
                break;
            case "REFRESHLEADER":
#pragma warning disable CS0612 // Type or member is obsolete
                leaderboards.PostToLeaderBoard();
#pragma warning restore CS0612 // Type or member is obsolete
                break;
        }
    }

    [System.Obsolete]
    void LevelComplete() 
    {
        updatedLeaderBoard = true;
        FinishLevel.gameObject.SetActive(true);
        FinishLevel.enabled = true;
        //string Username = GameValues.currentUser;
        leaderboards.PostToLeaderBoard();
        Debug.Log("posted to leaderboard");
    }
}
