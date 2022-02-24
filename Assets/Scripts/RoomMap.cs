using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private GameObject currentRoom = null;

    public GameObject checkerPrefab;
    public Vector2 mapCellSize;

    EnemyGrid roomMap;
    private void Awake()
    {
        GlobalVar.importantGameObjects.Add("Scripts", gameObject);
        roomMap = new EnemyGrid(mapCellSize);
    }
    void Update()
    {
        
        if (currentRoom != GlobalVar.currentRoom)
        {
            SetNewRoomMap();
        }
    }
    private void SetNewRoomMap()
    {
        currentRoom = GlobalVar.currentRoom;
        roomMap.DrawRoomMap(currentRoom);
        GlobalVar.roomMap = roomMap;
    }
}
