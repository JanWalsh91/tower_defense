using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class towerRadialMenu : MonoBehaviour {

	public CanvasRenderer panelRadialMenu;
	public Sprite upgradeSprite;
	public Sprite sellSprite;
	public Button exitButton;
	public Button upgradeButton;
	public Button downgradeButton;
	public Image upgradeEnergy;
	public Image downgradeEnergy;
	public Image upgradeImage;
	public Image downgradeImage;
	public Text upgradeTowertext;
	public Text downgradeTowertext;
	public UIPlayerInfo uIPlayerInfo;
	public GameObject radiusCircle;

	GameObject towerObj;
	towerScript tower;
	int upgradeCost;
	int downgradeRefund;
	float circleSize;
	Vector3 shootVec;
	Vector3 circleOriginalScale;

	void Start() {
		circleSize = radiusCircle.GetComponent<SpriteRenderer>().bounds.size.y;
		circleOriginalScale = radiusCircle.transform.localScale;
		if (panelRadialMenu) {
			panelRadialMenu.gameObject.SetActive(false);
		}	
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			shootVec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			shootVec.z = -1;
			ShootRayAndPositionMenu();
		}
		if (panelRadialMenu.gameObject.activeSelf) {
			if (tower && tower.upgrade) {
				if (upgradeCost <= gameManager.gm.playerEnergy) {
					upgradeImage.color = Color.white;
					upgradeEnergy.color = Color.white;
					upgradeTowertext.color = Color.white;
				} else {
					upgradeImage.color = Color.magenta;
					upgradeEnergy.color = Color.magenta;
					upgradeTowertext.color = Color.magenta;
				}
			} 
		}
	}

	void ShootRayAndPositionMenu() {
		RaycastHit2D[] hits = Physics2D.RaycastAll(shootVec, Vector2.zero);
		foreach (RaycastHit2D hit in hits) {
			if (hit && hit.collider.gameObject.CompareTag("tower") && hit.collider.gameObject.transform.parent.gameObject.GetComponent<towerScript>()) {
				towerObj = hit.collider.gameObject.transform.parent.gameObject;
				tower = towerObj.GetComponent<towerScript>();
				panelRadialMenu.gameObject.transform.position = hit.collider.gameObject.transform.position;
				SetupMenu();
				return;
			}
		}
		panelRadialMenu.gameObject.SetActive(false);

	}

	void SetupMenu() {
		panelRadialMenu.gameObject.SetActive(true);

		if (tower.upgrade) {
			upgradeButton.gameObject.SetActive(true);
			upgradeCost = tower.upgrade.GetComponent<towerScript>().energy;
			upgradeTowertext.text = upgradeCost.ToString();
			if (gameManager.gm.playerEnergy >= tower.upgrade.GetComponent<towerScript>().energy) {
				upgradeImage.color = Color.white;
				upgradeEnergy.color = Color.white;
				upgradeTowertext.color = Color.white;
			} else {
				upgradeImage.color = Color.magenta;
				upgradeEnergy.color = Color.magenta;
				upgradeTowertext.color = Color.magenta;
			}
		} else {
			upgradeButton.gameObject.SetActive(false);
		}

		downgradeRefund = (tower.energy / 2);
		downgradeTowertext.text = downgradeRefund.ToString();
		if (tower.downgrade) {
			downgradeImage.sprite = upgradeSprite;
			downgradeImage.gameObject.transform.localScale = new Vector3(1, -1, 1);
		} else {
			downgradeImage.sprite = sellSprite;
			downgradeImage.gameObject.transform.localScale = new Vector3(1, 1, 1);
		}
		radiusCircle.transform.localScale = circleOriginalScale * tower.range / (circleSize / 2);
	}

	public void ExitMenu() {
		panelRadialMenu.gameObject.SetActive(false);
	}

	public void Upgrade() {
		if (upgradeCost > gameManager.gm.playerEnergy) {
			return;
		}
		if (tower && tower.upgrade) {
			GameObject newTower = Instantiate(tower.upgrade);
			newTower.transform.position = towerObj.transform.position;
			newTower.transform.parent = tower.transform.parent;
			gameManager.gm.playerEnergy -= upgradeCost;
		}
		if (towerObj) {
			Destroy(towerObj);
		}
		ShootRayAndPositionMenu();
	}

	public void Downgrade() {
		if (tower && tower.downgrade) {
			GameObject newTower = Instantiate(tower.downgrade);
			newTower.transform.position = towerObj.transform.position;
			newTower.transform.parent = tower.transform.parent;
			gameManager.gm.playerEnergy += downgradeRefund;
		}
		if (towerObj) {
			Destroy(towerObj);
			RaycastHit2D hit = Physics2D.Raycast(shootVec, Vector2.zero);
			if (hit && hit.collider.gameObject.CompareTag("tile")) {
				hit.collider.gameObject.tag = "empty";
			}
		}
		if (tower.downgrade) {
			ShootRayAndPositionMenu();
		} else {
			ExitMenu();
		}

	}
}
