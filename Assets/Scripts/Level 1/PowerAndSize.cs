using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PowerAndSize : MonoBehaviour
{
	public GameObject cannon;

	public CannonActions cannonActions;

	public Rigidbody cannonBall;

	public Text powerText;

	void Start()
	{
		cannonBall = cannon.GetComponent<Rigidbody>();
		cannonActions = cannon.GetComponent<CannonActions>();

	}
	// Update is called once per frame
	void Update()
	{
		cannonActions = cannon.GetComponent<CannonActions>();
		powerText.text = "Power: " + cannonActions.Power.ToString() + "\nMass: " + cannonBall.mass.ToString("G");
	}
}
