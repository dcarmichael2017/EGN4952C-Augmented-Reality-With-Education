using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PowerAndSize : MonoBehaviour
{
	public GameObject cannon;

	public CannonActions cannonActions;

	public Rigidbody cannonBall;

	public TMP_Text powerText;

	void Start()
	{
		cannonBall = cannon.GetComponent<Rigidbody>();
		cannonActions = cannon.GetComponent<CannonActions>();

	}
	// Update is called once per frame
	void Update()
	{
		cannonActions = cannon.GetComponent<CannonActions>();
		powerText.text = "Force: " + cannonActions.Power.ToString() + "\nMass: " + cannonBall.mass.ToString("G");
	}
}
