using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    [SerializeField]
    public GameObject[] availableEnemies;

    List<EnemyInfo> enemyList = new List<EnemyInfo>();
    private void Awake()
    {
        foreach (GameObject enemy in availableEnemies)
        {
            EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
            enemyList.Add(new EnemyInfo(enemy, enemyStats));
        }

        GlobalVar.AvailableSpawnEnemies = enemyList;
    }
    
}
