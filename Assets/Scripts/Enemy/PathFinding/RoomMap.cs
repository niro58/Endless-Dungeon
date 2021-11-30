using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private GameObject currentRoom = null;

    public GameObject checkerPrefab;
    private List<GameObject> roomCheckers = new List<GameObject>();
    public Vector2 mapCellSize;
    private void Start()
    {
        currentRoom = GlobalVar.currentRoom;
        GlobalVar.Map = drawMap(currentRoom);
    }
    void Update()
    {
        
        if (currentRoom != GlobalVar.currentRoom)
        {
            currentRoom = GlobalVar.currentRoom;
            GlobalVar.Map = drawMap(currentRoom);
        }
    }
    CustomGrid drawMap(GameObject room) // Creates room map every time when the currentRoom changes.
    {
        CustomGrid map = new CustomGrid(mapCellSize);
        if (roomCheckers.Count > 1)
        {
            foreach (GameObject checker in roomCheckers)
            {
                Destroy(checker);
            }
            roomCheckers.Clear();
        }
        foreach (Transform roomPart in room.transform)
        {

            Vector2Int roomCell = map.WorldToCell(roomPart.transform.position);
            int size = Mathf.RoundToInt(3 / mapCellSize.x);
            for (int x = -size / 2; x < size / 2; x++)
            {
                for (int y = -size / 2; y < size / 2; y++)
                {
                    Vector2Int cell = roomCell + new Vector2Int(x, y);
                    Vector3 pos = map.CellToWorld(cell);
                    string[] maskLayers = new string[] { "Player", "Enemy" };
                    LayerMask layerMask = LayerMask.GetMask(maskLayers);
                    layerMask = ~layerMask;
                    RaycastHit2D hit = Physics2D.BoxCast(pos, mapCellSize, 0, new Vector2(0, 0), Mathf.Infinity, layerMask);

                    if (hit == false)
                    {
                        map.fillCell(cell);
                    }
                    /*
                    // Not Important
                    // Only for testing (Checkers) (46 -> 59)
                    checkerPrefab.transform.localPosition = mapCellSize;
                    GameObject checker = Instantiate(checkerPrefab,pos,Quaternion.identity, GameObject.Find("Not Important").transform.GetChild(0));
                    checker.transform.localScale = mapCellSize;
                    roomCheckers.Add(checker);
                    //Debug.Log("Pos 1 :" + x + " , " + y);
                    checker.name = ("Checker : " + cell);
                    if (hit == false)
                    {
                        checker.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        checker.GetComponent<SpriteRenderer>().color = Color.red;
                    }*/
                }
            }
            

        }
        return map;
    }
}
