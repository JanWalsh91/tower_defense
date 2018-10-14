using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameSpeed : MonoBehaviour {

	public Button minSpeed;
	public Button lessSpeed;
	public Button playPause;
	public Button moreSpeed;
	public Button maxSpeed;

	public Sprite playImage;
	public Sprite pauseImage;
	public Sprite speedImageA;
	public Sprite speedImageB;

	public Text textCurrentSpeed;

	public float maxSpeedValue;
	public float minSpeedValue;
	public float speedIncrement;

	public GameObject PauseMenu;
	public GameObject QuitConfirmationMenu;

	float currentSpeed;
	bool paused;

	float showSpeedTime = 2f;
	float lastShowSpeedTime;

	// Use this for initialization
	void Start () {
		paused = true;
		lastShowSpeedTime = Time.time;
		currentSpeed = 1;
		//UpdateSpeed();
		textCurrentSpeed.text = "";
		PauseMenu.SetActive(false);
		QuitConfirmationMenu.SetActive(false);
		playPause.GetComponentInChildren<Image>().sprite = playImage;

		minSpeed.GetComponentInChildren<Image>().sprite = speedImageB;
		minSpeed.GetComponentInChildren<Image>().gameObject.transform.localScale = new Vector3(-1, 1, 1);

		lessSpeed.GetComponentInChildren<Image>().sprite = speedImageA;
		lessSpeed.GetComponentInChildren<Image>().gameObject.transform.localScale = new Vector3(-1, 1, 1);

		maxSpeed.GetComponentInChildren<Image>().sprite = speedImageB;

		moreSpeed.GetComponentInChildren<Image>().sprite = speedImageA;
	}

	void Update() {
		if (Time.time - lastShowSpeedTime > showSpeedTime) {
			textCurrentSpeed.text = "";
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			PlayPause();
		}
	}

	public void PlayPause() {
		paused = !paused;
		gameManager.gm.pause(paused);
		if (paused) {
			playPause.GetComponentInChildren<Image>().sprite = playImage;
		} else {
			playPause.GetComponentInChildren<Image>().sprite = pauseImage;
		}
		if (paused) {
			PauseMenu.SetActive(true);
		} else {
			PauseMenu.SetActive(false);
			QuitConfirmationMenu.SetActive(false);
		}
	}

	public void MaxSpeed() {
		currentSpeed = maxSpeedValue;
		UpdateSpeed();
	}

	public void MinSpeed() {
		currentSpeed = minSpeedValue;
		UpdateSpeed();
	}

	public void IncrementSpeed() {
		currentSpeed += speedIncrement;
		UpdateSpeed();
	}

	public void DecrementSpeed() {
		currentSpeed -= speedIncrement;
		UpdateSpeed();
	}

	void UpdateSpeed() {
		currentSpeed = Mathf.Clamp(currentSpeed, minSpeedValue, maxSpeedValue);
		textCurrentSpeed.text = "x" + currentSpeed.ToString();
		lastShowSpeedTime = Time.time;
		gameManager.gm.changeSpeed(currentSpeed);
		if (paused) {
			gameManager.gm.pause(true);
		}
	}

	public void ShowQuitConfirmationPanel() {
		QuitConfirmationMenu.SetActive(true);
	}

	public void HideQuitConfirmationPanel() {
		QuitConfirmationMenu.SetActive(false);
	}

	public void GoToMainMenu() {
		SceneManager.LoadScene(0);
	}
}
