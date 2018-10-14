using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

	public static CursorManager cm;

	public Texture2D cursorTexture;

	void Awake() {
		if (cm == null)
			cm = this;
	}

	// Use this for initialization
	void Start () {
		SetCursorToDefault();
	}

	public void SetCursorToDefault() {
		Cursor.SetCursor(cursorTexture, Vector3.zero, CursorMode.Auto);
	}

	public void SetCursorTo(Texture2D texture2D) {
		Cursor.SetCursor(texture2D, new Vector3(texture2D.width / 2, texture2D.height / 2, 0), CursorMode.Auto);
	}
}
