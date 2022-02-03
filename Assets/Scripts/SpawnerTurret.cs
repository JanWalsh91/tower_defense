using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTurret : MonoBehaviour
{
    public enum Type { basic }

    public GameObject Minion;

    private bool canSpawn = false;
    private float nextSpawn = 0;
    private float spawnRate = 3f;

    private GameObject nextCheckpoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            canSpawn = true;
        }
        if (canSpawn)
        {
            spawnMinion();
        }
    }
    void spawnMinion()
    {
        CheckCloseDistance();

        GameObject newMinion = (GameObject)Instantiate(Minion, gameObject.transform.position, gameObject.transform.rotation);
        newMinion.transform.parent = this.gameObject.transform;
        ennemyScript botScript = newMinion.GetComponent<ennemyScript>();

        botScript.nextCheckpoint = nextCheckpoint;
        botScript.playerCore = GameObject.FindGameObjectsWithTag("playerCore")[0];
        botScript.hp = 100;
        botScript.value = 100;
        nextSpawn = Time.time + spawnRate;

        canSpawn = false;
        nextSpawn = Time.time + spawnRate;
    }

    private void CheckCloseDistance()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("EnemyStructure");

        float closestDistance = 0f;

        foreach (var obj in taggedObjects)
        {

            float distance = Vector3.Distance(obj.transform.position, gameObject.transform.position);

            if (distance < closestDistance || closestDistance == 0f)
            {
                closestDistance = distance;

                nextCheckpoint = obj;
            }
        }



    }

}
