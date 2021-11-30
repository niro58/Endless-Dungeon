using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    [SerializeField]
    public GameObject[] availableEnemies;

    private List<Enemy> enemyList = new List<Enemy>();
    private void Awake()
    {
        foreach (GameObject enemy in availableEnemies)
        {
            EntityStats enemyStats = enemy.GetComponent<EntityStats>();
            enemyList.Add(new Enemy(enemy, enemyStats));
        }

        GlobalVar.availableSpawnEnemies = enemyList;
    }
    
}
