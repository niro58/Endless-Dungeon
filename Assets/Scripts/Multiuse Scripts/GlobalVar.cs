using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static GameObject currentRoom;
    public static CustomGrid Map;

    public static GameObject Player;
    public static bool canMove = true;

    public static int currentPlayerGunSlot = 0;
    public static SumStats sumStats;
    // Player Stats

    // Spawner
    public static bool RoomCleared;
    public static int CurrentLevel;
    public static List<Enemy> availableSpawnEnemies;

}
