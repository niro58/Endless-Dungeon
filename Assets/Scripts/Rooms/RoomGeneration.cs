using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> rooms;
    private CustomGrid grid;
    private Vector2Int cellSize = new Vector2Int(3, 3);

    private GameObject roomsParent;

    public void Awake()// fix the bug with cells
    {
        GlobalVar.CurrentRoom = GameObject.Find("Rooms").transform.GetChild(0).gameObject;
    }
    public void Start()
    {
        cellSize = Vector2Int.FloorToInt(GlobalVar.CurrentRoom.transform.root.localScale);

        grid = new CustomGrid(cellSize);
        grid.FillCell(new Vector2Int(0, 0));
        roomsParent = GameObject.Find("Rooms");
    }
    public bool generateNextRoom(Vector2Int roomDirection, Vector3 roomCellPos)
    {
        Vector2Int roomCell = grid.WorldToCell(roomCellPos); // Getting the cell of the current room cell
        if (grid.IsAvailable(roomCell + roomDirection))// If the next cell in the direction(left,bottom,top,right) is filled than script ends
        {
            return false;
        }
        else
        {
            List<GameObject> listOfRooms = new List<GameObject>();
            listOfRooms.AddRange(rooms);
            while (listOfRooms.Count > 0)// Start of the room selection
            {
                int randNum = Random.Range(0, listOfRooms.Count);
                GameObject randRoom = Instantiate(listOfRooms[randNum], Vector3.zero, Quaternion.identity, roomsParent.transform); // Selecting a random room based on randNum, and then deleting the room from array
                Vector3 roomPos = grid.CellToWorld(roomCell);
                roomPos.z = 1;
                randRoom.transform.position = roomPos;
                listOfRooms.RemoveAt(randNum);
                List<GameObject> cells = new List<GameObject>();
                foreach (Transform cell in randRoom.transform.Find("Room_Parts"))// Getting each room cell
                {
                    cells.Add(cell.gameObject);
                }

                for (int i = 0; i < cells.Count; i++)// Moving cells into the direction that was defined at the door until they won't collide with the current room
                {
                    bool endLoop = true;
                    foreach (GameObject cell in cells)
                    {
                        cell.transform.localPosition += Vector3.Scale(new Vector3Int(roomDirection.x, roomDirection.y, 0), new Vector3(1, 1, 0));
                        if (grid.WorldToCell(cell.transform.position) == roomCell)
                        {
                            endLoop = false;
                        }
                    }
                    if (endLoop)
                    {
                        break;
                    }
                }
                bool endWhileLoop = true;
                foreach (GameObject cell in cells)
                {
                    if (grid.IsAvailable(grid.WorldToCell(cell.transform.position)))// If any of the randRoom cells collides with any other already created cell
                    {
                        Destroy(randRoom);
                        endWhileLoop = false;
                        break;
                    }
                }
                if (endWhileLoop)// If it didn't collide with any other cell that is already created
                {
                    foreach (GameObject cell in cells)// add it to the grid
                    {
                        grid.FillCell(grid.WorldToCell(cell.transform.position));
                    }
                    if (roomsParent.transform.childCount > 16)// if Rooms parent has more than 16 childs
                    {
                        foreach (Transform cell in roomsParent.transform.GetChild(0).GetChild(0).transform)// Foreach the earliest created room and delete cells from grid
                        {
                            grid.DeleteCell(grid.WorldToCell(cell.transform.position));
                        }
                        Destroy(roomsParent.transform.GetChild(0).gameObject);// destroy the room
                    }
                }
            }
            return true;
        }
    }

}