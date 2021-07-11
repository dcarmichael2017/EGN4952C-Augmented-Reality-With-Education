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

	public bool plusButton;

	public bool minusButton;

	private float massConstraint;

	private float scale = 0.1f;

	void Start()
	{
		massConstraint = 1f;
		cannonBall = cannon.GetComponent<Rigidbody>();
		cannonActions = cannon.GetComponent<CannonActions>();

	}
	// Update is called once per frame
	void Update()
	{
		if (plusButton)
		{
			cannonBall.mass = cannonBall.mass + 5 * scale;
			plusButton = false;
		}
		//if (minusButton && (cannonBall.mass - 5) >= massConstraint)
		if (minusButton && (cannonBall.mass) >= massConstraint)
		{
			cannonBall.mass = cannonBall.mass - 5 * scale;
			minusButton = false;
		}
		cannonActions = cannon.GetComponent<CannonActions>();
		powerText.text = "Force: " + cannonActions.Power.ToString() + "\nMass: " + cannonBall.mass.ToString("G") + "\nAcceleration: " + (cannonActions.Power/cannonBall.mass).ToString();
	}

	public void Buttons(string buttons)
    {
        switch (buttons)
        {
            case "PLUSBUTTONDOWN":
                plusButton = true;

                break;

            case "PLUSBUTTONUP":
                plusButton = false;

                break;

            case "MINUSBUTTONDOWN":
                minusButton = true;

                break;

            case "MINUSBUTTONUP":
                minusButton = false;

                break;
        }
    }
}
