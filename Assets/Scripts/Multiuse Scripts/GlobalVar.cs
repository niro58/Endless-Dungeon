using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    public static GameObject currentRoom;
    public static bool playerMovement;
    public static CustomGrid Map;

    public static int currentPlayerGunSlot = 0;

    public static GameObject player;
    // Player Stats
    public static int Health;
    public static float Speed;
    public static float Damage;
    public static float FireRate;// In seconds
    public static float BulletSpeed;
    public static float BulletRange;

    // Spawner
    public static bool RoomCleared;
    public static GameObject[] availableEnemies;
    public static int currentLevel;
}
