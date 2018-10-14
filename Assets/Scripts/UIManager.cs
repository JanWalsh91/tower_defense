using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

	Transform endGamePanel;
	public Text textVictoryStatus;
	public Text textScore;
	public Text textRank;
	public Button button;
	public Text textButtonText;

	string rank;
	static string[] ranks = { "F", "D", "C", "B", "A", "S", "S+" };

	void Start() {
		if (transform.childCount > 0) {
			endGamePanel = transform.GetChild(3);
			if (endGamePanel) {
				endGamePanel.gameObject.SetActive(false);
			}
		}
	}

	void OnEnable() {
		gameManager.onGameEnd += ShowEndGamePanel;
	}

	void OnDisable() {
		gameManager.onGameEnd -= ShowEndGamePanel;
	}

	public void ShowEndGamePanel(bool victory) {
		if (!endGamePanel) return;
		if (!endGamePanel) {
			endGamePanel = transform.GetChild(3);
		}
		endGamePanel.gameObject.SetActive(true);
		int currentScene = SceneManager.GetActiveScene().buildIndex;
		if (victory) {
			textVictoryStatus.text = "You win!";
			textButtonText.text = "Next level";
			int nextScene;
			if (currentScene < SceneManager.sceneCountInBuildSettings - 1) {
				nextScene = currentScene + 1;
				button.onClick.AddListener(delegate { SceneManager.LoadScene(nextScene); });
			} else {
				SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
			}

		} else {
			textVictoryStatus.text = "You lose ...";
			textButtonText.text = "Play again";
			button.onClick.AddListener(delegate { SceneManager.LoadScene(currentScene); });
		}
		textScore.text = "Your score: " + gameManager.gm.score.ToString();
		SetRank();
		textRank.text = "Your rank: " + rank;

	}

	void SetRank() {
		float rankScore = gameManager.gm.score + gameManager.gm.playerHp * 10 + gameManager.gm.playerEnergy;
		rankScore /= 10000;
		rankScore = Mathf.Min(Mathf.RoundToInt(rankScore), ranks.Length - 1);
		rank = ranks[(int)rankScore];
	}
}
