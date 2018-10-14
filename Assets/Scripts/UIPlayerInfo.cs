using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerInfo : MonoBehaviour {

	public Text playerEnergy;
	public Text playerHp;

	gameManager game;
	// Use this for initialization
	void Start () {
		if (!gameManager.gm) return;
		playerEnergy.text = gameManager.gm.playerEnergy.ToString();
		playerHp.text = gameManager.gm.playerHp.ToString();
	}

	public void Update () {
		if (gameManager.gm) {
			playerEnergy.text = gameManager.gm.playerEnergy.ToString();
			playerHp.text = gameManager.gm.playerHp.ToString();
		}
	}
}
