using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class move : MonoBehaviour {

	private const float ACCELERATION = 2f;
	private const float GRAVITY = 0.981f;
	private const float FORWARD_SPEED = 6.0f;

	private Vector3 yUnitVec = new Vector3 (0, 1, 0);
	private Vector3 forward = Vector3.zero;
	private CharacterController controller;
	private float ySpeed = 0;

	private float fuel = 100;
	public Text fuelText;

	void Start () {
		controller = (CharacterController) GetComponent(typeof(CharacterController));
		forward = transform.forward;
		forward = transform.TransformDirection(forward);
		forward *= FORWARD_SPEED;
		setFuelText ();
	}

	void Update () {
		Vector3 dir = Vector3.zero;
		dir += forward;
		// Applies y acceleration
		if (Input.GetMouseButton(0) && fuel >= 1) {
			ySpeed += ACCELERATION;
			fuel--;
		}
		if (controller.isGrounded && ySpeed < 0) {
			fuel += 0.5f;
			ySpeed = 0;
		} else {
			ySpeed -= GRAVITY;
		}
		dir += ySpeed * yUnitVec;
		controller.Move(dir * Time.deltaTime);
		// Cap the fuel to 100.
		fuel = Mathf.Min (100, fuel);
		setFuelText ();
	}
	void setFuelText() {
		fuelText.text = "Fuel: " + (Mathf.FloorToInt(fuel)).ToString () + "%";
	}
}