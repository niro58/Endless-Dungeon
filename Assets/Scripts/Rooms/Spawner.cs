using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Spawner : MonoBehaviour
{
    private enum EnemyType { Ground, Air};
    private EnemyType enemyType;

    private List<GameObject> availableEnemies = new List<GameObject>();
    void Start()
    {
        //tartCoroutine(SpawnEnemies());
    }
    /*
    IEnumerator SpawnEnemies()
    {
        
        foreach (Transform child in transform)
        {
            GlobalVar.enemiesLeft += 1;
            LeanTween.alpha(child.gameObject, 0, 2f);
        }
        yield return new WaitForSeconds(2f);// foreach child -> get random item from dictionary -> get values from it  -> spawn the amount of mobs, decrease the number of max spawns -> if number <= 0 remove from dictionary


        if (transform.name.Contains("Fly"))
        {
            enemyType = EnemyType.Air;
        }
        else
        {
            enemyType = EnemyType.Ground;
        }

        foreach (GameObject enemy in GlobalVar.availableEnemies)
        {
            if (enemyType == EnemyType.Ground && enemy.gameObject.layer == 8)
            {
                availableEnemies.Add(enemy);
            }
            else if (enemyType == EnemyType.Air && enemy.gameObject.layer == 9)
            {
                availableEnemies.Add(enemy);
            }
        }
        Transform enemyParent = GlobalVar.currentRoom.transform.Find("Enemies");
        foreach (Transform child in transform)
        {
            int randNum = Random.Range(0, availableEnemies.Count);
            GameObject enemyInst = Instantiate(availableEnemies[randNum], child.transform.position, Quaternion.identity, enemyParent);
            enemyInst.transform.localScale /= transform.root.localScale.x;
        }
        
    }*/
}