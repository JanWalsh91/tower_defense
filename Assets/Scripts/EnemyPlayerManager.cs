using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPlayerManager : MonoBehaviour
{
    public GameObject[] rivalSpawnableTiles;
    public GameObject[] minionTowers;
    public GameObject[] attackTowers;
    public GameObject[] rivalMinionSpawnableTiles;
    
    public float spawnMinionTowerRate;
    private float nextSpanwMinionTower = 0f;
    private bool canSpawnMinionTower = false;

    public float spawnAttackTowerRate;
    private float nextSpawnAttackTower = 0f;
    private bool canSpawnAttackTower = false;
    
    void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextSpanwMinionTower)
        {
            canSpawnMinionTower = true;
        }

        if (Time.time > nextSpawnAttackTower)
        {
            canSpawnAttackTower = true;
        }

        if (canSpawnAttackTower)
        {
            SpawnAttackTower();
        }

        if (canSpawnMinionTower)
        {
            SpawnMinionTower();
        }
    }

    void SpawnMinionTower()
    {
        if (rivalMinionSpawnableTiles.Length != 0)
        {
            int towerToSpawn = Random.Range(0, minionTowers.Length);

            int areaToSpawn = Random.Range(0, rivalMinionSpawnableTiles.Length);

           Instantiate(minionTowers[towerToSpawn], rivalMinionSpawnableTiles[areaToSpawn].transform.position, rivalMinionSpawnableTiles[areaToSpawn].transform.rotation);

            //tower.GetComponent<towerScript>().side = ennemyScript.Sides.Enemy;

            rivalMinionSpawnableTiles = rivalMinionSpawnableTiles.Where(tile => tile != rivalMinionSpawnableTiles[areaToSpawn]).ToArray();

            nextSpanwMinionTower = Time.time + spawnMinionTowerRate;

            canSpawnMinionTower = false;
        }
    }

    void SpawnAttackTower()
    {
        if (rivalSpawnableTiles.Length != 0)
        {
            int towerToSpawn = Random.Range(0, attackTowers.Length);

            int areaToSpawn = Random.Range(0, rivalSpawnableTiles.Length);

            Instantiate(attackTowers[towerToSpawn], rivalSpawnableTiles[areaToSpawn].transform.position, rivalSpawnableTiles[areaToSpawn].transform.rotation);

            //tower.GetComponent<towerScript>().side = ennemyScript.Sides.Enemy;

            rivalSpawnableTiles = rivalSpawnableTiles.Where(tile => tile != rivalSpawnableTiles[areaToSpawn]).ToArray();

            nextSpawnAttackTower = Time.time + spawnAttackTowerRate;

            canSpawnAttackTower = false;
        }
    }
}
