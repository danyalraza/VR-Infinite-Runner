using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
	private bool gameOver = false;
	private bool started = false;

	public Text restartText;
	public Text fuelText;
	public Button restartButton;

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
		if (isPressed() && fuel >= 1 && (controller.collisionFlags & CollisionFlags.Above) == 0) {
			ySpeed += ACCELERATION;
			fuel--;
		}
		if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
			ySpeed = 0;
			fuel--;
		}
		if (controller.isGrounded && ySpeed < 0) {
			fuel += 2f;
			ySpeed = 0;
		} else {
			ySpeed -= GRAVITY;
		}
		dir += ySpeed * yUnitVec;
		if (!gameOver && started) {
			controller.Move (dir * Time.deltaTime);
			// Cap the fuel to 100.
			fuel = Mathf.Min (100, fuel);
			// Set the minimum fuel to 0.
			fuel = Mathf.Max (0, fuel);
			setFuelText ();
		} else if (!started && !gameOver) {
			if (pressed) {
				started = true;
				restartButton.gameObject.SetActive (false);
			}
		} else {
			if (pressed) {
				SceneManager.LoadScene("IntroScene");
			}
		}
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

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Coin")) {
			other.GetComponent<AudioSource> ().Play ();
			fuel += 20;
		}
		if (other.gameObject.CompareTag ("obstacle")) {
			restartText.text = "Restart";
			restartButton.gameObject.SetActive (true);
			restartButton.interactable = true;
			fuelText.text = "Game over";
			gameOver = true;
		}
	}
}