using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCannon : MonoBehaviour
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.transform.rotation = Quaternion.Euler(rb.transform.rotation.eulerAngles + new Vector3(0, 0, 0.25f));
    }
}
