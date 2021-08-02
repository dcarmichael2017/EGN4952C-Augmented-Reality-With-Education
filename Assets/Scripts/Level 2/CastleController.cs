using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class CastleController : MonoBehaviour
{

    public float castleRadius = 20f;

    public float  height = 5;

    public float length = 30;

    public float width = 12;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(length, height, width));
        
    }

    public bool WithinRange(Transform gameobject) 
    {
        float distance = Vector3.Distance(gameobject.position, this.transform.position);

        if (distance <= castleRadius)
        {
            return true;
        }
        else
            return false;
    }

}
