    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static int enemiesLeft;
    public static GameObject currentRoom;
    public static EnemyGrid roomMap;

    public static bool canMove = true;

    public static PlayerStats playerStats;
    // Player Stats

    // Spawner
    public static float CurrentLevel;
    public static List<EnemyInfo> AvailableSpawnEnemies;

    public static Dictionary<string, GameObject> importantGameObjects = new Dictionary<string, GameObject>();

}
