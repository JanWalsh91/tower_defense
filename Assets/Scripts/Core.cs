using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private int hp;
    public int maxHp;

    public ennemyScript.Sides side;
    public GameObject nextCore;
    private bool isCoreUpdateFinished;
    public bool isMainCore;
    public GameObject[] Corespawnableareas;
    // Start is called before the first frame update
    void Start()
    {
        isCoreUpdateFinished = false;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void DamageCore(int damage)
    {

        hp -= damage;
        Debug.Log("Core Damaged. Remaining Hp: " + hp);
        
        if (hp == 0)
        {
            if (isMainCore)
            {
                if (side == ennemyScript.Sides.Enemy)
                {
                    gameManager.gameOver(side,true);
                }
                else
                {
                    gameManager.gameOver(side,false);
                }
            }
            else
            {
                UpdateMinionNextCore();
                if (isCoreUpdateFinished)
                {
                    destroyspawnableareas();
                    isCoreUpdateFinished = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    public int GetCurrentHp()
    {
        return hp;
    }

    private void UpdateMinionNextCore()
    {
        var minions = GetMinions(side);

        foreach (var minion in minions)
        {
            ennemyScript ennemyScript = minion.GetComponent<ennemyScript>();
            if (minion != null)
            {
                ennemyScript.playerCore = nextCore;
            }
           
        }
        isCoreUpdateFinished = true;
    }

    private List<GameObject> GetMinions(ennemyScript.Sides side)
    {
        List<GameObject> minions = new List<GameObject>();
        if (side == ennemyScript.Sides.Enemy)
        {
            minions = GetMinionList("PlayerMinion");
        }
        else
        {
            minions = GetMinionList("EnemyMinion");
        }

        return minions;
    }

    private List<GameObject> GetMinionList(string tag)
    {
        var list = new List<GameObject>();
        var minionArray = GameObject.FindGameObjectsWithTag(tag);

        foreach (var minion in minionArray)
        {
            list.Add(minion);
        }

        return list;
    }
    private void destroyspawnableareas()
    {
        foreach (var area in Corespawnableareas)
        {
            Destroy(area);
        }
    }
}
