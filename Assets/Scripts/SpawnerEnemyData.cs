using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo
{
    public GameObject gameObject;
    public EnemyStats stats;
    public EnemyInfo(GameObject gameObject, EnemyStats stats)
    {
        this.gameObject = gameObject;
        this.stats = stats;
    }
}
