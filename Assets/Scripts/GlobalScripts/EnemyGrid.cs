using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class EnemyGrid
{
    public Vector2 cellSize;
    public Dictionary<Vector2Int, string> filledCells = new Dictionary<Vector2Int, string>();
    public EnemyGrid(Vector2 cellSize)
    {
        this.cellSize = cellSize;
    }
    public Vector2Int WorldToCell(Vector3 pos)
    {
        Vector2Int cell = new Vector2Int((int)Mathf.Ceil(pos.x / cellSize.x), (int)Mathf.Ceil(pos.y / cellSize.y));
        return cell;
    }
    public Vector3 CellToWorld(Vector2Int cell)
    {
        return new Vector3(cell.x * cellSize.x, cell.y * cellSize.y, 0);
    }
    public Vector3 WorldToCellWorld(Vector3 pos)
    {
        return CellToWorld(WorldToCell(pos));
    }
    public void FillCell(Vector2Int cell, string value)
    {
        filledCells.Add(cell, value);
    }
    public bool IsAvailable(Vector2Int cell)
    {
        if(filledCells.ContainsKey(cell) && filledCells[cell] == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Dictionary<Vector2Int,string> GetAvailableCell(Vector2Int cell)
    {
        if (IsAvailable(cell))
        {
            return new Dictionary<Vector2Int, string> { { cell, filledCells[cell] } };
        }
        return null;
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
    public Dictionary<Vector2Int, string> getFreeCells()
    {
        return filledCells.Where(p => p.Value == null).ToDictionary(p => p.Key, p => p.Value);
    }
    public void DrawRoomMap(GameObject room) // Creates room map every time when the currentRoom changes.
    {
        filledCells.Clear();
        foreach (Transform roomPart in room.transform.Find("Room_Parts"))
        {

            Vector2Int roomCell = WorldToCell(roomPart.transform.position);
            int size = Mathf.RoundToInt(3 / cellSize.x);
            for (int x = -size / 2; x <= size / 2; x++)
            {
                for (int y = -size / 2; y <= size / 2; y++)
                {
                    Vector2Int cell = roomCell + new Vector2Int(x, y);
                    Vector3 pos = CellToWorld(cell);

                    string[] maskLayers = new string[] { "Player", "GroundEnemy", "FlyingEnemy" };
                    LayerMask layerMask = LayerMask.GetMask(maskLayers);
                    layerMask = ~layerMask;
                    RaycastHit2D hit = Physics2D.BoxCast(pos, cellSize, 0, new Vector2(0, 0), Mathf.Infinity, layerMask);

                    if (!filledCells.ContainsKey(cell)){
                        if (hit == false)
                        {
                            FillCell(cell, null);
                        }
                        else
                        {
                            FillCell(cell, hit.collider.gameObject.layer.ToString());
                        }
                    }
                    
                }
            }
        }
    }
}