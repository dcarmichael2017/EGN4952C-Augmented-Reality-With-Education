using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Canvas FinishLevel;
    DisplayStats stats;
    leaderboard leaderboards;
    bool updatedLeaderBoard = false;


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
    }

    [System.Obsolete]
    void LevelComplete() 
    {
        updatedLeaderBoard = true;
        FinishLevel.gameObject.SetActive(true);
        FinishLevel.enabled = true;
        string Username = GameValues.currentUser;
        leaderboards.PostToLeaderBoard(Username);
        Debug.Log("posted to leaderboard");
    }
}
