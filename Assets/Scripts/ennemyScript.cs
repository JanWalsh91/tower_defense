using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ennemyScript : MonoBehaviour
{

    public static class Constants
    {
        public static string PlayerTag = "playerCore";
        public static string EnemyTag = "EnemyCore";
        public static string EnemyCheckPointTag = "EnemyCheckPoint";
        public static string PlayerCheckPointTag = "PlayerCheckPoint";
    }

    public enum Sides
    {
        Player = 0,
        Enemy = 1
    }

    [HideInInspector] public GameObject nextCheckpoint;
    [HideInInspector] public GameObject playerCore;
    public int waveNumber;
    public GameObject explosion;
    private Vector3 lastPos;
    public float speed;
    public bool isFlying = false;
    public int score;
    public int hp;
    public int value;                                   //Energie gagnee en tuant l'ennemi
    public int ennemyDamage;                //Dommages lorsque l'ennemi touche le core
    public float spawnRate;                     //Vitesse de spawn en secondes
    public int waveLenghtModifier;      //Modificateur de taille de vague en %

    private Sides side;

    //Initialisation de la classe
    void Start()
    {
        lastPos = gameObject.transform.position;
        score = hp;
    }

    //Boucle d'update de la classe
    void Update()
    {
        if (playerCore == null)
        {
            UpdateCore();
        }

        lookForward();
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, nextCheckpoint.transform.position, step);
        if (hp <= 0)
        {
            gameManager.gm.playerEnergy += value;
            gameManager.gm.score += score;
            destruction();
        }
        else if ( playerCore != null &&transform.position == playerCore.transform.position)
        {
            gameManager.gm.damagePlayer(ennemyDamage);
            destruction();
        }
        else if (transform.position == nextCheckpoint.transform.position)
        {
            CheckCloseDistance(GetSideTags());
        }
    }

    //Permet a l'ennemi de regarder dans la direction ou il se deplace
    void lookForward()
    {
        Vector3 moveDirection = gameObject.transform.position - lastPos;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        lastPos = gameObject.transform.position;
    }

    //Fonction appellee a la destruction d'un ennemi
    void destruction()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
        checkLastEnemy();
    }

    //Fonction testant la destruction du dernier ennemi pour la victoire du niveau
    void checkLastEnemy()
    {
        if (gameManager.gm.lastWave == true)
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            foreach (GameObject spawner in spawners)
            {
                if (spawner.GetComponent<ennemySpawner>().isEmpty == false || spawner.transform.childCount > 1)
                {
                    return;
                }
            }
            Debug.Log("Victoire !");
            gameManager.gm.victory = true;
           // gameManager.gm.gameOver();
        }
    }

    private void CheckCloseDistance(List<string> tags)
    {
        List<GameObject> taggedObjects = new List<GameObject>();
        foreach (string tag in tags)
        {
            var objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                taggedObjects.Add(obj);
            }
        }



        for (int i = 0; i < taggedObjects.Count; i++)
        {
            if (taggedObjects[i] == nextCheckpoint)
            {
                taggedObjects = taggedObjects.Where(item => item != taggedObjects[i]).ToList();
            }
        }

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

    public void SetSide(Sides side)
    {
        this.side = side;
    }

    private List<string> GetSideTags()
    {
        List<string> tags = new List<string>();
        if (side == Sides.Player)
        {
            tags.Add(Constants.EnemyTag);
            tags.Add(Constants.EnemyCheckPointTag); ;
        }
        else
        {
            tags.Add(Constants.PlayerTag);
            tags.Add(Constants.PlayerCheckPointTag);
        }

        return tags;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyCore" && side == Sides.Player)
        {
            Core core = collision.gameObject.GetComponent<Core>();
            if (core != null)
            {
                core.DamageCore(ennemyDamage);
            }
            //CheckCloseDistance(GetSideTags());
            destruction();
        }
        else if(collision.tag == "playerCore" && side == Sides.Enemy)
        {
            Core core = collision.gameObject.GetComponent<Core>();
            if (core != null)
            {
                Debug.Log("Damaging Player Core");
                core.DamageCore(ennemyDamage);
            }
            //CheckCloseDistance(GetSideTags());
            destruction();
        }
    }

    private void UpdateCore()
    {
        if (side == Sides.Player)
        {
            playerCore = FindNearestCore(Constants.EnemyTag);
        }
        else
        {
            playerCore = FindNearestCore(Constants.PlayerTag);
        }
    }

    private GameObject FindNearestCore(string tag)
    {
        GameObject nearestCore = null;
        GameObject[] cores = GameObject.FindGameObjectsWithTag(tag);


        float closestDistance = 0f;

        foreach (GameObject core in cores)
        {

            float distance = Vector3.Distance(core.transform.position, gameObject.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;

                nearestCore = core;
            }
        }

        return nearestCore;
    }
}
