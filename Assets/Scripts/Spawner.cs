using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Spawner : MonoBehaviour
{

    public bool onlyFlyingEnemies;

    private List<Enemy> availableEnemies;
    void Start()
    {
        availableEnemies = GlobalVar.availableSpawnEnemies;

        StartCoroutine(spawnEnemies());
    }
    IEnumerator spawnEnemies()
    {
        GlobalVar.canMove = false;
        yield return new WaitForSeconds(2f);// foreach child -> get random item from dictionary -> get values from it  -> spawn the amount of mobs, decrease the number of max spawns -> if number <= 0 remove from dictionary
        GlobalVar.canMove = true;

        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                
                int randNum = Random.Range(0, availableEnemies.Count);// to do : spawn amount
                Enemy enemy = availableEnemies[randNum];
                EntityStats enemyStats = enemy.stats;
                
                if (!onlyFlyingEnemies || (onlyFlyingEnemies && enemy.gameObject.ToString() == "FlyingEnemy"))
                {
                    GameObject enemyInst = Instantiate(enemy.gameObject, child.transform.position, Quaternion.identity, child.transform);
                    enemyInst.transform.localScale *= 1 / child.transform.localScale.x;
                    enemyInst.transform.localScale /= transform.root.localScale.x;


                    //enemy.stats.maxAmountPerRoom -= 1;
                    //if (enemyStats.maxAmountPerRoom == 0)
                    //{
                    //    availableEnemies.Remove(enemy);
                    //}
                }
            }
        }
    }
}
