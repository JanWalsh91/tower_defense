using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool dragOnSurfaces = true;
	public GameObject PanelBuyTower;
	public GameObject canvas;
	public Sprite RadiusSprite;

	GameObject newTower;
	Image towerImage;
	GameObject tmpTower;
	int towerEnergy;
	bool canBuy = true;

	// Use this for initialization
	void Start () {
		towerImage = PanelBuyTower.GetComponent<UIBuyTower>().towerImage;
		towerEnergy = PanelBuyTower.GetComponent<UIBuyTower>().towerPrefab.GetComponent<towerScript>().energy;
	}
	
	// Update is called once per frame
	void Update () {
		float playerEnergy = gameManager.gm.playerEnergy;
		if (playerEnergy < towerEnergy) {
			UpdateImagesColor(Color.magenta);
			canBuy = false;
		} else {
			UpdateImagesColor(Color.white);
			canBuy = true;
		}
	}

	void UpdateImagesColor(Color color) {
		Image[] images = gameObject.GetComponentsInChildren<Image>();
		foreach (Image image in images) {
			image.color = color;
		}
	}

	public void OnBeginDrag(PointerEventData data) {
		if (canvas == null || !canBuy)
			return;
		
		tmpTower = new GameObject("icon");
		tmpTower.AddComponent<Image>();
		tmpTower.GetComponent<Image>().sprite = towerImage.sprite;
		tmpTower.transform.localScale = new Vector3(0.35f, 0.35f, 0f);
		tmpTower.transform.Rotate(0, 0, -90);
		tmpTower.transform.SetParent(canvas.transform, false);
		tmpTower.transform.SetAsLastSibling();
		GameObject circle = new GameObject();
		circle.transform.parent = tmpTower.transform;
		SpriteRenderer spriteRenderer = circle.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = RadiusSprite;
		circle.transform.localPosition = new Vector3(0, 0, 0);
		float range = PanelBuyTower.GetComponent<UIBuyTower>().towerPrefab.GetComponent<towerScript>().range;
		circle.transform.localScale *= range / (circle.GetComponent<SpriteRenderer>().bounds.size.y / 2);
		SetDraggedPosition(data);
		BuyTower();

	}

	public void OnDrag(PointerEventData data) {
		if (tmpTower != null)
			SetDraggedPosition(data);
	}

	public void OnEndDrag(PointerEventData data) {
		if (!tmpTower) return;
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vector.z = -1;
		RaycastHit2D hit = Physics2D.Raycast(vector, Vector2.zero);
		if (hit && hit.collider.gameObject.CompareTag("empty")) 
		{
			SpawnableArea area = ((GameObject)hit.collider.gameObject).GetComponent<SpawnableArea>();

			if (area != null)
            {
                if (area.side == ennemyScript.Sides.Player)
                {
					if (!area.GetIsTowerPlaced())
					{
						tmpTower.transform.position = hit.collider.gameObject.transform.position;
						hit.collider.gameObject.tag = "tile";
						CreateTower(hit);
					}
                    else
                    {
						Debug.Log("Tower is placed to area");
                    }
				}
                else
                {
					Debug.Log("Not Player Area");
                }
				
			}
            else
            {
				Debug.Log("Spawnable Area Not Found");
            }
         

			
		} else {
			ReturnTower();
		}
		Destroy(tmpTower);
	}

	void SetDraggedPosition(PointerEventData data) {
		Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		vector.z = -1;
		RaycastHit2D hit = Physics2D.Raycast(vector, Vector2.zero);
		if (hit) {
			tmpTower.transform.position = hit.collider.gameObject.transform.position;
			if (hit.collider.gameObject.CompareTag("empty")) {
				tmpTower.GetComponent<Image>().color = Color.white;
			} else {
				tmpTower.GetComponent<Image>().color = Color.magenta;
			}
		} else {
			tmpTower.transform.position = vector;
			tmpTower.GetComponent<Image>().color = Color.magenta;
		}
	}

	void BuyTower() {
		gameManager.gm.playerEnergy -= towerEnergy;
	}

	void ReturnTower() {
		gameManager.gm.playerEnergy += towerEnergy;
	}

	void CreateTower(RaycastHit2D hit) {
		newTower = Instantiate(PanelBuyTower.GetComponent<UIBuyTower>().towerPrefab);
		newTower.transform.position = hit.collider.gameObject.transform.position;
		newTower.GetComponent<towerScript>().side = ennemyScript.Sides.Player;
	}
}
