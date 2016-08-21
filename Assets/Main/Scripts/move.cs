using UnityEngine;
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

	void Start () {
		controller = (CharacterController) GetComponent(typeof(CharacterController));
		forward = transform.forward;
		forward = transform.TransformDirection(forward);
		forward *= FORWARD_SPEED;
	}

	void Update () {
		Vector3 dir = Vector3.zero;
		dir += forward;
		// Applies y acceleration
		if (isPressed()) {
			ySpeed += ACCELERATION;
		}
		if (controller.isGrounded && ySpeed < 0) {
			ySpeed = 0;
		} else {
			ySpeed -= GRAVITY;
		}
		dir += ySpeed * yUnitVec;
		controller.Move(dir * Time.deltaTime);
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

	private void CheckHeight()
	{
//		if (transform.position.y < -10)
//		{
//			GameManager.Instance.Die();
//		}
	}
}