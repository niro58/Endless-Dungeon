using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public GameObject gameObject;
    public EntityStats stats;
    public Enemy(GameObject gameObject, EntityStats stats)
    {
        this.gameObject = gameObject;
        this.stats = stats;
    }
}
