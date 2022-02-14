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
    private void Start()
    {
        
        roomMap = new EnemyGrid(mapCellSize);
    }
    void Update()
    {
        
        if (currentRoom != GlobalVar.CurrentRoom)
        {
            SetNewRoomMap();
        }
    }
    private void SetNewRoomMap()
    {
        currentRoom = GlobalVar.CurrentRoom;
        roomMap.DrawRoomMap(currentRoom);
        GlobalVar.RoomMap = roomMap;
    }
}
