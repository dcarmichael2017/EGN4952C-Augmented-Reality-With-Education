using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    public GameObject TrajectoryPointPre;

    public GameObject CannonBall_Spawn;

    private int numOfTrajecoryPoints = 30;

    //private List<GameObject> trajectoryPoints;

    // Start is called before the first frame update
    void Start()
    {
        /* trajectoryPoints = new List<GameObject>();
         for (int i = 0; i < numOfTrajecoryPoints; i++)
         {
             GameObject dot = (GameObject)Instantiate(TrajectoryPointPre);
             dot.renderer.enabled = false;
             trajectoryPoints.Insert(i, dot);

         }*/
    }

    // Update is called once per frame
    void Update()
    {
    }
}

    /*void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity)
    { 
        
        float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y));
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));
        float fTime = 0;
    
    }*/
