using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationGrid
{
    public Vector2 cellSize;
    public List<Vector2Int> filledCells = new List<Vector2Int>();
    public RoomGenerationGrid(Vector2 cellSize)
    {
        this.cellSize = cellSize;
    }
    public Vector2Int WorldToCell(Vector3 pos)
    {
        Vector2Int cell = new Vector2Int((int)(pos.x / cellSize.x), (int)(pos.y / cellSize.y));
        return cell;
    }
    public Vector3 CellToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * cellSize.x, cell.y * cellSize.y, 0);
    }
    public void FillCell(Vector2Int cell)
    {
        filledCells.Add(cell);
    }
    public bool IsAvailable(Vector2Int cell)
    {
        return filledCells.Contains(cell);
    }
    public void DeleteCell(Vector2Int cell)
    {
        filledCells.Remove(cell);
    }
    public List<Vector2Int> GetNodesAroundNode(Vector2Int cell)
    {
        List<Vector2Int> nodes = new List<Vector2Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int node = new Vector2Int(x, y) + cell;
                if (x != 0 || y != 0 && IsAvailable(node))
                {
                    nodes.Add(node);
                }
            }
        }
        return nodes;
    }
    public List<Vector2Int> getFilledCells()
    {
        return filledCells;
    }
    public void DrawRoomMap(GameObject room) // Creates room map every time when the currentRoom changes.
    {
        foreach (Transform roomPart in room.transform.Find("Room_Parts"))
        {

            Vector2Int roomCell = WorldToCell(roomPart.transform.position);
            int size = Mathf.RoundToInt(3 / cellSize.x);
            for (int x = -size / 2; x < size / 2; x++)
            {
                for (int y = -size / 2; y < size / 2; y++)
                {
                    Vector2Int cell = roomCell + new Vector2Int(x, y);
                    Vector3 pos = CellToWorld(cell);
                    string[] maskLayers = new string[] { "Player", "Enemy" };
                    LayerMask layerMask = LayerMask.GetMask(maskLayers);
                    layerMask = ~layerMask;
                    RaycastHit2D hit = Physics2D.BoxCast(pos, cellSize, 0, new Vector2(0, 0), Mathf.Infinity, layerMask);

                    if (hit == false)
                    {
                        FillCell(cell);
                    }

                }
            }
        }
    }
}