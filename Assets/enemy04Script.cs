using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04Script : MonoBehaviour
{
    public List<Vector2Int> roomMap;
    // Start is called before the first frame update
    void Start()
    {
        roomMap = GlobalVar.RoomMap.getFilledCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
