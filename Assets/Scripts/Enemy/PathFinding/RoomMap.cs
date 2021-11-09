using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMap : MonoBehaviour
{
    [SerializeField]
    private GameObject currentRoom = null;

    public GameObject checkerPrefab;
    private List<GameObject> roomCheckers = new List<GameObject>();
    public Vector2 mapSize;
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
        CustomGrid map = new CustomGrid(mapSize);
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
            Vector3 roomPos = roomPart.position;
            Vector2 roomScale = roomPart.root.localScale;

            for (float x = -roomScale.x / 2; x <= roomScale.x / 2; x += mapSize.x)
            {
                for (float y = -roomScale.y / 2; y <= roomScale.y / 2; y += mapSize.y)
                {
                    Vector3 pos = roomPos - new Vector3(x, y, 0);
                    Vector2Int cell = map.WorldToCell(pos);
                    string[] maskLayers = new string[] { "Player", "Enemy" };
                    LayerMask layerMask = LayerMask.GetMask(maskLayers);
                    layerMask = ~layerMask;
                    RaycastHit2D hit = Physics2D.BoxCast(map.CellToWorld(cell), mapSize, 0, new Vector2(0, 0), Mathf.Infinity, layerMask);

                    if (hit == false)
                    {
                        map.fillCell(cell);
                    }
                    
                    // Not Important
                    // Only for testing (Checkers) (46 -> 59)
                    GameObject checker = Instantiate(checkerPrefab,GameObject.Find("Not Important").transform.GetChild(0));
                    roomCheckers.Add(checker);
                    //Debug.Log("Pos 1 :" + x + " , " + y);
                    checker.name = ("Checker : " + cell);
                    checker.transform.position = map.CellToWorld(cell);
                    
                    if (hit == false)
                    {
                        checker.GetComponent<SpriteRenderer>().color = Color.green;
                    }
                    else
                    {
                        checker.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    
                }
            }
        }
        return map;
    }
}
