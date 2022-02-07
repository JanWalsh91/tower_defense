using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyTower : MonoBehaviour {

	public GameObject towerPrefab;
	public Sprite fly;
	public Sprite noFly;
	public Sprite towerSprite;
	public Text energy;
	public Text damage;
	public Text range;
	public Text fireRate;
	public Image flyImage;
	public Image towerImage;

	// Use this for initialization
	void Start () {
		if (!towerPrefab) return;
        MinionSpawnerTurret Minionspwnr = towerPrefab.GetComponent<MinionSpawnerTurret>();
        if (Minionspwnr !=null)
        {
            towerImage.sprite = towerSprite;
        }
        else
        {
            towerScript tower = towerPrefab.GetComponent<towerScript>();
            energy.text = tower.energy.ToString();
            damage.text = tower.damage.ToString();
            range.text = tower.range.ToString();
            fireRate.text = tower.fireRate.ToString();
            if (tower.type == towerScript.Type.canon)
            {
                flyImage.sprite = noFly;
            }
            else
            {
                flyImage.sprite = fly;
            }
            towerImage.sprite = towerSprite;
        }
		
	}

	void Update() {
		
	}
}
