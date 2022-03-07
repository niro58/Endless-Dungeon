using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static GameObject currentRoom;
    public static EnemyGrid roomMap;

    public static int enemiesLeft = 0;
    public static bool canMove = true;
    public static bool canSwap = true;

    public static Player player;
    public static int playerGunCount = 1;
    public static PlayerStats playerStats = new PlayerStats();
    // Player Stats

    public static int currentLevel;

    public static Dictionary<string, GameObject> importantPrefabs = new Dictionary<string, GameObject>();

}
