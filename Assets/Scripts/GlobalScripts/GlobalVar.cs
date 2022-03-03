using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static int enemiesLeft;
    public static GameObject currentRoom;
    public static EnemyGrid roomMap;

    public static bool canMove = true;

    public static Player player;
    public static PlayerStats playerStats = new PlayerStats();
    // Player Stats

    // Spawner
    public static int currentLevel;
    public static List<GameObject> availableEnemies;

    public static Dictionary<string, GameObject> importantPrefabs = new Dictionary<string, GameObject>();

}
