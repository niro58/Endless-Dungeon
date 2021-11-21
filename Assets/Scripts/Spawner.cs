using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] availableEnemies;
    public bool onlyFlyingEnemies;
    void Start()
    {
        StartCoroutine(spawnEnemies());
    }
    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(2f);
        int x = 0;
        while (x < availableEnemies.Length)
        {
            x++;
            int index = Random.Range(0, availableEnemies.Length);
            GameObject enemy = availableEnemies[index];
            if(!onlyFlyingEnemies || (onlyFlyingEnemies && enemy.layer.ToString() == "FlyingEnemy"))
            {
                enemy = Instantiate(enemy, transform.position, Quaternion.identity, transform.parent.parent.Find("Enemies"));
                enemy.transform.localScale /= transform.root.localScale.x;

                break;
            }
        }
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
