using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //public Rigidbody rb;

    private Transform SpawnCube2 = null;

    public GameObject Enemy = null;

    public float Speed = 2.0f;


    void Start()
    {
        SpawnCube2 = transform.Find("SpawnCube2");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }

    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(Enemy, SpawnCube2.position, Quaternion.identity);

        Rigidbody rb = enemy.AddComponent<Rigidbody>();

        rb.velocity = Speed * SpawnCube2.forward;

    }
}
