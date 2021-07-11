using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Position(At Specified Time) = Initial Positions of X and Y multiplied by Speed, then subtract Gravity Factor from Y
//f(t) = (x0 + x*t, y0 + y*t - 9.81t²/2, z0 + z*t)
//In essence, move X and Y at a constant speed, subtract Gravity from Y

//Variables to decide:
//Force, Mass, Origin & Direction in which to launch objects
//Simulation Length, Step Measurement Interval
//Variables to calculate: 
//Max Steps, Velocity, and Position at each Step


public class TrajectoryV2 : MonoBehaviour
{

    //public Transform CannonBall_Spawn;
    public Rigidbody cannon;

    private CannonActions cannonActions;

    private LineRenderer _lr; //Line to predict trajectory

    //private float _force = 500; //Force, can be assigned in Unity Inspector
    private float _mass = 1; //Automatic mass of an object is 1, can be reassigned
    private float _fixedDeltaTime;
    private float _vel; //Initial Velocity, calculated via V = Force / Mass * fixedTime (0.02)
    private float _gravity;
    private float _collisionCheckRadius = 0.1f; //Collision radius of last point on SimulationArc, to communicate with it when to stop. Currently using IgnoreRaycast Layer on some objects, suboptimal

    // Start is called before the first frame update
    void Start()
    {
        cannonActions = cannon.GetComponent<CannonActions>();
       _lr = GetComponent<LineRenderer>();
       _lr.startColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        DrawTrajectory(); //Simulate trajectory calculation at run-time

    }

    void DrawTrajectory()
    {
        _lr.positionCount = SimulateArc().Count;
        for (int a = 0; a < _lr.positionCount; a++)
        {
            _lr.SetPosition(a, SimulateArc()[a]); //Add each Calculated Step to a LineRenderer to display a Trajectory. Look inside LineRenderer in Unity to see exact points and amount of them
        }
    }

    private List<Vector3> SimulateArc() //A method happening via this List
    {
        List<Vector3> lineRendererPoints = new List<Vector3>(); //Reset LineRenderer List for new calculation

        float maxDuration = 5f; //INPUT amount of total time for simulation
        float timeStepInterval = 0.1f; //INPUT amount of time between each position check
        int maxSteps = (int)(maxDuration / timeStepInterval);//Calculates amount of steps simulation will iterate for
        Vector3 directionVector = cannonActions.CannonBall_Spawn.forward; //INPUT launch direction (This Vector3 is automatically normalized for us, keeping it in low and communicable terms)
        //Vector3 launchPosition = transform.position + transform.up; //INPUT launch origin (Important to make sure RayCast is ignoring some layers (easiest to use default Layer 2))
        Vector3 launchPosition = cannonActions.CannonBall_Spawn.position;

        //_vel = cannonActions.Power / _mass * Time.fixedDeltaTime; //Initial Velocity, or Velocity Modifier, with which to calculate Vector Velocity
        _vel = cannonActions.Power; //Initial Velocity, or Velocity Modifier, with which to calculate Vector Velocity

        for (int i = 0; i < maxSteps; ++i)
        {
            //Remember f(t) = (x0 + x*t, y0 + y*t - 9.81t²/2)
            Vector3 calculatedPosition = launchPosition + directionVector * _vel * i * timeStepInterval; //Move both X and Y at a constant speed per Interval
            //calculatedPosition = launchPosition + (directionVector * (speed * i * timeStepInterval)); //calculatedPosition = Origin + (transform.up * (speed * which step * the length of a step);
            calculatedPosition += Physics.gravity / 2 * Mathf.Pow(i * timeStepInterval, 2); //Subtract Gravity from Y

            lineRendererPoints.Add(calculatedPosition); //Add this to the next entry on the list

            if (CheckForCollision(calculatedPosition)) //if you hit something, stop adding positions
            {
              break; //stop adding positions
            }
        }
        return lineRendererPoints;
    }

    private bool CheckForCollision(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, _collisionCheckRadius); //Measure collision via a small circle at the latest position, dont continue simulating Arc if hit
        if (hits.Length > 0) //Return true if something is hit, stopping Arc simulation
        {
            return true;
        }
        return false;
    }

}
