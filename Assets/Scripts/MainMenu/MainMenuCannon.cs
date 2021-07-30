using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCannon : MonoBehaviour
{

    private Rigidbody rb;

    public Transform CannonBall_Spawn;
    public GameObject Cannonball = null;
    public float Power = 35.0f;

    public Button shootButton1;
    public Button shootButton2;
    public Button shootButton3;
    public Button shootButton4;
    public Button shootButton5;

    //private PowerAndSize powerAndSize;

    private float scale = 1f;

    public float fireRate = 0.75f; //seconds
    private float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //powerAndSize = GetComponent<PowerAndSize>();
        counter = fireRate;
        shootButton1.onClick.AddListener(ShootCannonBall);
        shootButton2.onClick.AddListener(ShootCannonBall);
        shootButton3.onClick.AddListener(ShootCannonBall);
        shootButton4.onClick.AddListener(ShootCannonBall);
        shootButton5.onClick.AddListener(ShootCannonBall);
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles + new Vector3(0, 0, 0.25f));

        counter += Time.deltaTime;

        //if touch
        //ShootCannonBall()

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {

        }
    }

    public void ShootCannonBall()
    {
        if (counter >= fireRate)
        {
            //next time cannon can shoot (in seconds)
            counter = 0;

            GameObject cannonball = Instantiate(Cannonball, CannonBall_Spawn.position, Quaternion.identity);

            Rigidbody cannonballRB = cannonball.GetComponent<Rigidbody>();

            cannonballRB.mass = 0.1f;

            SphereCollider SC = cannonball.AddComponent<SphereCollider>();

            cannonballRB.AddForce(Power * CannonBall_Spawn.forward * scale);
        }

    }
}
