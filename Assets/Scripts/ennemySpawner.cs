using UnityEngine;
using System.Collections;

public class ennemySpawner : MonoBehaviour {

//	public enum Type{NANO, ROUND, ONE, TWO, FLY, BOSS}
//	public Type type;
	private int waveNumber = 0;
	private float spawnRate;
	private float nextSpawn = 0;
	private float nextWave;
	private int waveLenght;
	private int spawned = 0;
	private GameObject toSpawn;
	public GameObject[] bots;
	public GameObject nextCheckpoint;
	[HideInInspector]public GameObject playerCore;
	[HideInInspector]public bool isEmpty = false;

	//Pour les curieux une méthode beaucoup plus propre existe mais l'appliquer ici vous spoilerait les jours a venir. 
	// J'utilise donc en attendant une bidouille moins jolie mais fonctionnelle.
	void trySpawn() {
		if (Time.time > nextSpawn && spawned <  waveLenght) {
			GameObject newBot = (GameObject)Instantiate(toSpawn, transform.position, Quaternion.identity);
			newBot.transform.parent = this.gameObject.transform;
			ennemyScript botScript = newBot.GetComponent<ennemyScript>();
			if (botScript.isFlying == true)
				botScript.nextCheckpoint = playerCore;
			else
				botScript.nextCheckpoint = nextCheckpoint;
			botScript.playerCore = playerCore;
			botScript.waveNumber = waveNumber;
			botScript.hp = Mathf.RoundToInt(((float)gameManager.gm.nextWaveEnnemyHpUp * (waveNumber - 1) + 100) / 100 * (float)botScript.hp);
			botScript.value = Mathf.RoundToInt(((float)gameManager.gm.nextWaveEnnemyValueUp * (waveNumber - 1) + 100) / 100 * (float)botScript.value);
			nextSpawn = Time.time + spawnRate;
			spawned += 1;
		}
		if (spawned ==  waveLenght) {
			if (waveNumber == gameManager.gm.totalWavesNumber) {
				gameManager.gm.lastWave = true;
				isEmpty = true;
			}
			else {
				nextWave = Time.time + gameManager.gm.delayBetweenWaves;
				pickType();
			}
		}
	}

	//Selection aleatoire du prochain type d'ennemi a spawner
	void pickType() {
		waveNumber += 1;
		Debug.Log("Vague numero : " + waveNumber);
		spawned = 0;
		int r = Random.Range(0, bots.Length);
		toSpawn = bots[r];
		ennemyScript botScript = bots[r].GetComponent<ennemyScript>();
		waveLenght = Mathf.RoundToInt(((float)botScript.waveLenghtModifier + 100) / 100 * (float)gameManager.gm.averageWavesLenght);
		spawnRate = botScript.spawnRate;
	}

	void Update() {
		if (Time.time > nextWave && waveNumber <= gameManager.gm.totalWavesNumber)
			trySpawn ();
	}

	void Start() {
		playerCore = GameObject.FindGameObjectWithTag("playerCore");
		pickType();
	}

}
