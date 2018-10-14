using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyboardShortcuts : MonoBehaviour {

	public GameObject[] towerPrefabs;
	public Texture2D[] towerTextures;
	public GameObject firePrefab;
	public Texture2D fireTexture;

	bool hasItem = false;
	GameObject item;
	int itemEnergy = 0;

	public static UIKeyboardShortcuts km;
	void Awake() {
		if (km == null)
			km = this;
	}

	// Update is called once per frame
	void Update () {
		HandleKeyboardInput();
		HandleClick();
	}

	void HandleKeyboardInput() {
		if (Input.GetKeyDown(KeyCode.Q)) {
			SetTowerToCursor(0);
		}
		if (Input.GetKeyDown(KeyCode.W)) {
			SetTowerToCursor(1);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			SetTowerToCursor(2);
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			SetFireToCursor();
		}
	}

	void SetTowerToCursor(int i) {
		itemEnergy = towerPrefabs[i].GetComponent<towerScript>().energy;
		if ( itemEnergy <= gameManager.gm.playerEnergy) {
			if (hasItem) {
				Destroy(item);
			}
			hasItem = true;
			CursorManager.cm.SetCursorTo(towerTextures[i]);
			             
			item = Instantiate(towerPrefabs[i]);
			item.SetActive(false);
		}
	}

	void SetFireToCursor() {
		itemEnergy = fireScript.fm.energy;
		if (itemEnergy <= gameManager.gm.playerEnergy && fireScript.fm.canUse) {
			if (hasItem) {
				Destroy(item);
			}
			if (item) {
				item = null;
			}
			hasItem = true;
			CursorManager.cm.SetCursorTo(fireTexture);
		}
	}

	void HandleClick() {
		if (!hasItem) return;
		if (Input.GetMouseButtonDown(0)) {
			if (item && item.CompareTag("tower")) {
				item.SetActive(true);
				Vector3 shootVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				shootVec.z = -1;
				RaycastHit2D[] hits = Physics2D.RaycastAll(shootVec, Vector2.zero);
				foreach (RaycastHit2D hit in hits) {
					if (hit.collider.gameObject.CompareTag("empty")) {
						item.transform.position = hit.collider.gameObject.transform.position;
						hit.collider.gameObject.tag = "tile";
						gameManager.gm.playerEnergy -= itemEnergy;
						hasItem = false;
						CursorManager.cm.SetCursorToDefault();
						item = null;
						return;
					}
				}
				Destroy(item);
				hasItem = false;
				CursorManager.cm.SetCursorToDefault();
			} else {
				fireScript.fm.Explode(Camera.main.ScreenToWorldPoint(Input.mousePosition));
				CursorManager.cm.SetCursorToDefault();
				gameManager.gm.playerEnergy -= itemEnergy;
				hasItem = false;
			}
		}
		if (Input.GetMouseButtonDown(1)) {
			if (item)
				Destroy(item);
			hasItem = false;
			CursorManager.cm.SetCursorToDefault();
		}
	}
}
