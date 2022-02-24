using UnityEngine;
using System;
public class Door : MonoBehaviour // Script gets info in what direction the door will generate new room, move player (left,top,right,bottom)
{
    public Vector2Int doorDirectionVector;
    public enum DoorDirection { Top = 0 , Right = 1, Bottom = 2, Left= 3 };
    public DoorDirection doorDirection;
    public void Start()
    {
        switch ((int)doorDirection)
        {
            case 0:
                doorDirectionVector = new Vector2Int(0, 1);
                break;
            case 1:
                doorDirectionVector = new Vector2Int(1, 0);
                break;
            case 2:
                doorDirectionVector = new Vector2Int(0, -1);
                break;
            case 3:
                doorDirectionVector = new Vector2Int(-1, 0);
                break;
        }
    }
    public void Update()
    {
        if(GlobalVar.enemiesLeft == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
