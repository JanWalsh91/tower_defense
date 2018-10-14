using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragFire : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public bool dragOnSurfaces = true;
	public GameObject canvas;
	public Sprite RadiusSprite;
	public Image fireImage;

	GameObject tmpFire;

	// Update is called once per frame
	void Update () {
		float playerEnergy = gameManager.gm.playerEnergy;
		if (playerEnergy < fireScript.fm.energy || !fireScript.fm.canUse) {
			UpdateImagesColor(Color.magenta);
		} else {
			UpdateImagesColor(Color.white);
		}
	}

	void UpdateImagesColor(Color color) {
		Image[] images = gameObject.GetComponentsInChildren<Image>();
		foreach (Image image in images) {
			image.color = color;
		}
	}

	public void OnBeginDrag(PointerEventData data) {
		if (canvas == null || !fireScript.fm.canUse)
			return;
		
		//Debug.Log("OnBeginDrag");
		tmpFire = new GameObject("icon");
		tmpFire.AddComponent<Image>();
		tmpFire.GetComponent<Image>().sprite = fireImage.sprite;
		tmpFire.transform.localScale = new Vector3(0.5f, 0.5f, 0f);
		tmpFire.transform.SetParent(canvas.transform, false);
		tmpFire.transform.SetAsLastSibling();
		GameObject circle = new GameObject();
		circle.transform.parent = tmpFire.transform;
		SpriteRenderer spriteRenderer = circle.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = RadiusSprite;
		circle.transform.localPosition = new Vector3(0, 0, 0);
		float range = fireScript.fm.radius;
		circle.transform.localScale *= range / (circle.GetComponent<SpriteRenderer>().bounds.size.y / 2);
		SetDraggedPosition(data);
		BuyFire();
	}

	public void OnDrag(PointerEventData data) {
		if (tmpFire != null)
			SetDraggedPosition(data);
	}

	public void OnEndDrag(PointerEventData data) {
		if (!tmpFire) return;
		fireScript.fm.Explode(tmpFire.transform.position);
		Destroy(tmpFire);
	}

	void SetDraggedPosition(PointerEventData data) {
		tmpFire.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -1);
	}

	void BuyFire() {
		gameManager.gm.playerEnergy -= fireScript.fm.energy;
	}

}
