using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy05Script : MonoBehaviour
{
    // mob turret - that moves is there's a direction, prefires if prefire is true
    public Vector2 direction;
    public float maxDistance;
    public bool prefire;
    public EnemyGrid roomMap;

    private EnemyStats enemyStats;
    // Start is called before the first frame update
    void Start()
    {
        roomMap = GlobalVar.roomMap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
