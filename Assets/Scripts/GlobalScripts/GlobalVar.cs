using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static GameObject CurrentRoom;
    public static CustomGrid RoomMap;

    public static GameObject Player;
    public static bool CanMove = true;

    public static int CurrentPlayerGunSlot = 0;
    public static PlayerStats playerStats;
    // Player Stats

    // Spawner
    public static bool RoomCleared;
    public static int CurrentLevel;
    public static List<EnemyInfo> AvailableSpawnEnemies;

}
