using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour {

	//Vous pouvez directement changer ces valeurs de base dans l'inspecteur si vous voulez personnaliser votre jeu
	[HideInInspector]public int playerHp = 20;
	public int playerMaxHp = 20;
	[HideInInspector]public int playerEnergy = 300;
	public int playerStartEnergy = 300;

	public int delayBetweenWaves = 10;					//Temps entre les vagues
	public int nextWaveEnnemyHpUp = 20; 				//Augmentation de la vie des bots a chaque vague (en %)
	public int nextWaveEnnemyValueUp = 30; 		//Augmentation de l'energie donnee par les bots a chaque vague (en %)
	public int averageWavesLenght = 15;					//Taille moyenne d'une vague d'ennemis
	public int totalWavesNumber = 20;						// Nombre des vagues au total
	[HideInInspector]public bool lastWave = false;
	[HideInInspector]public int currentWave = 1;
	private float tmpTimeScale = 1;
	[HideInInspector]public int score = 0;

	public static gameManager gm;

	public bool victory = false;

	public delegate void OnGameEnd(bool victory);
	public static event OnGameEnd onGameEnd;

	//Singleton basique  : Voir unity design patterns sur google.
	void Awake () {
		if (gm == null)
			gm = this;
	}

	void Start() {
		Time.timeScale = 0;
		playerHp = playerMaxHp;
		playerEnergy = playerStartEnergy;
	}

	//Pour mettre le jeu en pause
	public void pause(bool paused) {
		if (paused == true) {
			tmpTimeScale = Time.timeScale;
			Time.timeScale = 0;
		}
		else
			Time.timeScale = tmpTimeScale;
	}

	//Pour changer la vitesse de base du jeu
	public void changeSpeed(float speed) {
		Time.timeScale = speed;
	}

	//Le joueur perd de la vie
	public void damagePlayer(int damage) {
		playerHp -= damage;
		if (playerHp <= 0)
			gameOver();
		else
			Debug.Log ("Il reste au joueur " + playerHp + "hp");
	}

	//On pause le jeu en cas de game over
	public void gameOver() {
		Time.timeScale = 0;
		Debug.Log ("Game Over");
		onGameEnd(victory);
	}
}
