using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLevelSystem : MonoBehaviour
{
    public GameObject[] availableEnemies;

    // Start is called before the first frame update
    void Awake()
    {
        GlobalVar.availableEnemies = availableEnemies;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
