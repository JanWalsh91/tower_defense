using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour {
	
	public void StartGame() {
		SceneManager.LoadScene("ex01");

	}

	public void ExitGame() {
		Application.Quit();
	}
}
