    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static GameObject CurrentRoom;
    public static EnemyGrid RoomMap;

    public static bool CanMove = true;
    public static bool RoomCleared = true;

    public static PlayerStats playerStats;
    // Player Stats

    // Spawner
    public static float CurrentLevel;
    public static List<EnemyInfo> AvailableSpawnEnemies;

}
