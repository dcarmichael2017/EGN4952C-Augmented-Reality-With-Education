using UnityEngine;

public class CannonCamera : MonoBehaviour
{

	public Transform cannon;    // A variable that stores a reference to our Player

	// Update is called once per frame
	void LateUpdate()
	{

		transform.LookAt(cannon.transform);

	}
}