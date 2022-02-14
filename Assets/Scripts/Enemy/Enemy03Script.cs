using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03Script : MonoBehaviour // Fly that just goes straight to player
{
    [Header("Movement")]
    private float speed;
    private EnemyStats stats;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        stats = gameObject.GetComponent<EnemyStats>();
        target = GlobalVar.playerStats.player;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, stats.speed * Time.deltaTime);
    }
}