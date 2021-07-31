using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PowerAndSize : MonoBehaviour
{
	public GameObject cannon;

	public CannonActions cannonActions;

	public TMP_Text powerText;

	public bool plusButton;

	public bool minusButton;

	private float massConstraint;

	private float scale = 0.1f;

	public float currentMass = 0.1f;

	void Start()
	{
		massConstraint = 0.1f;
		//cannonBall = cannon.GetComponent<Rigidbody>();
		cannonActions = cannon.GetComponent<CannonActions>();

	}
	// Update is called once per frame
	void Update()
	{
		if (plusButton)
		{
			//cannonBall.mass = cannonBall.mass + 5 * scale;
			currentMass += 5 * scale;
			plusButton = false;
		}
		//if (minusButton && (cannonBall.mass - 5) >= massConstraint)
		if (minusButton && (currentMass) >= massConstraint) //&& (cannonBall.mass) >= massConstraint
		{
			//cannonBall.mass = cannonBall.mass - 5 * scale;
			currentMass -= 5 * scale;
			minusButton = false;
		}
		cannonActions = cannon.GetComponent<CannonActions>();
		powerText.text = "Force: " + cannonActions.Power.ToString() + "\nMass: 1\nAcceleration: " + cannonActions.Power.ToString();
	}

	public float getCannonBallMass()
    {
		return currentMass;
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
