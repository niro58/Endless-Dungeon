using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> availableEnemies = new List<GameObject>();
    public void Start()
    {
        GlobalVar.availableEnemies = availableEnemies;
    }

}
