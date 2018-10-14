using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class towerScript : MonoBehaviour {

	private bool canFire = false;
	private float nextShot = 0;
	public enum Type{gatling, rocket, canon};
	public Type type;
	public GameObject ammo;
	public GameObject turret;
	private List<GameObject> botsList = new List<GameObject>();
	public int damage;
	public float fireRate;
	public int energy = 0;
	public float range = 1;
	public GameObject upgrade;
	public GameObject downgrade;
	
	void Update () {
		if (Time.time > nextShot) {
			canFire = true;
		}
		if (botsList.Count > 0) {
			if (botsList[0] != null) {
				aim(botsList[0]);
				if (canFire == true) {
					fire (botsList[0]);
				}
			}
			else
				botsList.RemoveAt(0);
		}
	}

	void Start() {
		GetComponent<CircleCollider2D>().radius = range;
	}

	void OnTriggerEnter2D(Collider2D bot) {
		if (type == Type.canon && (bot.tag == "bot" || bot.tag == "boss"))
			botsList.Add(bot.gameObject);
		else if (type == Type.gatling && (bot.tag == "bot" || bot.tag == "flybot" || bot.tag == "boss"))
			botsList.Add(bot.gameObject);
		else if (type == Type.rocket && (bot.tag == "bot" || bot.tag == "flybot" || bot.tag == "boss"))
			botsList.Add(bot.gameObject);
	}

	void OnTriggerExit2D(Collider2D bot) {
		if (type == Type.canon && (bot.tag == "bot" || bot.tag == "boss"))
			botsList.Remove(bot.gameObject);
		else if (type == Type.gatling && (bot.tag == "bot" || bot.tag == "flybot" || bot.tag == "boss"))
			botsList.Remove(bot.gameObject);
		else if (type == Type.rocket && (bot.tag == "bot" || bot.tag == "flybot" || bot.tag == "boss"))
			botsList.Remove(bot.gameObject);
	}

	void aim(GameObject bot) {
		Vector2 direction = bot.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
		Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, targetRotation, Time.deltaTime * 12);
	}

	void fire(GameObject bot) {
		if (type == Type.canon)
			canonFire(bot);
		else if (type == Type.gatling)
			gatlingFire(bot);
		else if (type == Type.rocket)
			rocketFire(bot);
		canFire = false;
		nextShot = Time.time + fireRate;
	}

	void canonFire(GameObject bot){
		GameObject newShell = (GameObject)Instantiate (ammo, gameObject.transform.position, gameObject.transform.rotation);
		newShell.GetComponent<shellScript> ().getTarget (bot);
		newShell.GetComponent<shellScript> ().damage = damage;
		newShell.transform.localScale = new Vector2(-1, 1);
	}


	void gatlingFire(GameObject bot) {
		float randX = (Random.Range (-0.1f, 0.1f)) + bot.transform.position.x;
		float randY = (Random.Range (-0.1f, 0.1f)) + bot.transform.position.y;
		Instantiate (ammo, new Vector2(randX, randY), Quaternion.identity);
		bot.gameObject.GetComponent<ennemyScript>().hp -= damage;
	}

	void rocketFire(GameObject bot) {
		GameObject newRocket = (GameObject)Instantiate (ammo, gameObject.transform.position, gameObject.transform.rotation);
		newRocket.transform.rotation = Quaternion.Euler(newRocket.transform.rotation.x, newRocket.transform.rotation.y, newRocket.transform.rotation.z +180);
		newRocket.GetComponent<rocketScript> ().getTarget (bot);
		newRocket.GetComponent<rocketScript> ().damage = damage;
		newRocket.transform.localScale = new Vector2(-1, 1);
	}
}
