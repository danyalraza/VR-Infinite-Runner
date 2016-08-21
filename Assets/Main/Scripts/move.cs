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

	private bool pressed = false;
	private float fuel = 100;
	public Text fuelText;

	void Start () {
		setupPhoton ();
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
		if (isPressed() && fuel >= 1 && (controller.collisionFlags & CollisionFlags.Above) == 0) {
			ySpeed += ACCELERATION;
			fuel--;
		}
		if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
			ySpeed = 0;
			fuel--;
		}
		if (controller.isGrounded && ySpeed < 0) {
			fuel += 1.5f;
			ySpeed = 0;
		} else {
			ySpeed -= GRAVITY;
		}
		if (pressed == false) {
			fuel += 0.5f;
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

	private bool isPressed(){
		// Checks for updates
		if (Input.GetMouseButtonDown(0)) {
			pressed = true;
		} else if (Input.GetMouseButtonUp(0)) {
			pressed = false;
		}
		return GvrViewer.Instance.Triggered || pressed;
	}

	private void setupPhoton(){
		PhotonNetwork.Instantiate (
			"Player",
			new Vector3 (0, 0, 4),
			Quaternion.identity,
			1
		);
	}
}