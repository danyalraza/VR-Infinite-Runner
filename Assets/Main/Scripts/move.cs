﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class move : MonoBehaviour {

	private const float ACCELERATION = 2f;
	private const float GRAVITY = 0.981f;
	private float forwardSpeed = 12.0f;

	private Vector3 yUnitVec = new Vector3 (0, 1, 0);
	private Vector3 forward = Vector3.zero;
	private CharacterController controller;
	private float ySpeed = 0;

	private bool pressed = false;
	private float fuel = 100;
	private bool gameOver = false;
	private bool started = false;

	public Text restartText;
	public AudioSource music;
	public Text fuelText;
	public Button restartButton;
	public TileManager manager;

	void Start () {
		controller = (CharacterController) GetComponent(typeof(CharacterController));
		forward = transform.forward;
		forward = transform.TransformDirection(forward);
		setFuelText ();
		music.Play ();
	}

	void Update () {
		Vector3 dir = Vector3.zero;
		dir += forward * forwardSpeed;

		forwardSpeed *= 1.001f;
		forwardSpeed = Mathf.Min (16.0f, forwardSpeed);
		// Applies y acceleration
		if (isPressed () && fuel >= 1 && (controller.collisionFlags & CollisionFlags.Above) == 0) {
			ySpeed += ACCELERATION;
			fuel--;
		} else if ((controller.collisionFlags & CollisionFlags.Above) != 0) {
			ySpeed = 0;
			fuel--;
		} else {
			fuel += 0.5f;
		}
		if (controller.isGrounded && ySpeed < 0) {
			ySpeed = 0;
			fuel += 2.0f;
		} else {
			ySpeed -= GRAVITY;
		}
		dir += ySpeed * yUnitVec;
		if (!gameOver && started) {
			manager.addX (dir.magnitude);
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
		fuelText.text = "Energy: " + (Mathf.FloorToInt(fuel)).ToString () + "%";
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
			fuel += 16;
		}
		if (other.gameObject.CompareTag ("obstacle")) {
			restartText.text = "Restart";
			restartButton.gameObject.SetActive (true);
			restartButton.interactable = true;
			fuelText.text = "Score: " + manager.getX().ToString();
			gameOver = true;
		}
	}
}
