using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawnerTurret : MonoBehaviour
{
    public enum Type { basic }

    public GameObject Minion;

    private bool canSpawn = false;
    private float nextSpawn = 0;
    private float spawnRate = 3f;

    public GameObject entryCheckpoint;
    private GameObject nearestCore;

    public ennemyScript.Sides side;

    // Start is called before the first frame update
    void Start()
    {
        FindNearestRoad();
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
        FindNearestCore();

        GameObject newMinion = (GameObject)Instantiate(Minion, gameObject.transform.position, gameObject.transform.rotation);
        newMinion.transform.parent = this.gameObject.transform;
        ennemyScript botScript = newMinion.GetComponent<ennemyScript>();

        botScript.nextCheckpoint = entryCheckpoint;
        botScript.playerCore = nearestCore;
        botScript.hp = 100;
        botScript.value = 100;
        botScript.SetSide(side);
        nextSpawn = Time.time + spawnRate;

        canSpawn = false;
        nextSpawn = Time.time + spawnRate;
    }

    private void FindNearestCore()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(GetSideTag());

        float closestDistance = 0f;

        foreach (var obj in taggedObjects)
        {

            float distance = Vector3.Distance(obj.transform.position, gameObject.transform.position);

            if (distance < closestDistance || closestDistance == 0f)
            {
                closestDistance = distance;

                nearestCore = obj;
            }
        }

    }

    private void SetCore()
    {
        GameObject[] cores = GameObject.FindGameObjectsWithTag("");
    }

    private string GetSideTag()
    {
        if (side == ennemyScript.Sides.Player)
        {
            return ennemyScript.Constants.EnemyTag;
        }
        else
        {
            return ennemyScript.Constants.PlayerTag;
        }
    }

    private void FindNearestRoad()
    {
        GameObject nearestRoad = new GameObject();
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("road");

        float closestDistance = Mathf.Abs(Vector3.Distance(taggedObjects[0].transform.position, gameObject.transform.position));

        foreach (var obj in taggedObjects)
        {

            float distance = Vector3.Distance(obj.transform.position, gameObject.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;

                nearestRoad = obj;
            }
        }

        Debug.Log(nearestRoad);

        Instantiate(entryCheckpoint, nearestRoad.transform);


    }

}
