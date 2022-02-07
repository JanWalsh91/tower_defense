using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableArea : MonoBehaviour
{
    public ennemyScript.Sides side;

    private bool isTowerPlaced;

    public bool GetIsTowerPlaced()
    {
        return isTowerPlaced;
    }

    public void SetIsTowerPlaced(bool isTowerPlaced)
    {
        this.isTowerPlaced =isTowerPlaced;
    }
    
    void Start()
    {
        isTowerPlaced = false;
    }
}
