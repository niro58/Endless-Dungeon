using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private GameObject currentRoom = null;

    public GameObject checkerPrefab;
    public Vector2 mapCellSize;
    private void Start()
    {
        SetNewRoomMap();
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
        CustomGrid roomMap = new CustomGrid(mapCellSize);
        roomMap.DrawRoomMap(currentRoom);
        GlobalVar.RoomMap = roomMap;

    }
}
