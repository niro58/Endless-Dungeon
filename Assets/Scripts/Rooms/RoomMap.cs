using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private GameObject currentRoom = null;

    public GameObject checkerPrefab;
    public Vector2 mapCellSize;

    EnemyGrid enemyGrid;
    private void Awake()
    {
        enemyGrid = new EnemyGrid(mapCellSize);
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
        enemyGrid.DrawRoomMap(currentRoom);
        GlobalVar.roomMap = enemyGrid;
        /*
        foreach(KeyValuePair<Vector2Int, string> cell in enemyGrid.filledCells)
        {
            GameObject createdCell = Instantiate(checkerPrefab, enemyGrid.CellToWorld(cell.Key), Quaternion.identity, transform);
            if (cell.Value == null)
            {
                createdCell.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                createdCell.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }*/
    }
}
