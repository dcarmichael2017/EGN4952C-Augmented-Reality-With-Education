using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject FinishLevel;
    DisplayStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<DisplayStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //Basic method to find how many enemies are on the level at once.
        stats.SetAliveEnemies(GameObject.FindGameObjectsWithTag("Enemy").Length);
        if (stats.totalEnemiesAmount == stats.deployedEnemiesAmount && stats.aliveEnemiesAmount == 0)
            FinishLevel.SetActive(true); ;
    }
}
