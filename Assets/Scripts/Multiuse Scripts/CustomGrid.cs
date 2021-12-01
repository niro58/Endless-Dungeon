using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid
{
    public Vector2 cellSize;
    public List<Vector2Int> filledCells = new List<Vector2Int>();
    public CustomGrid(Vector2 cellSize)
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
    public void fillCell(Vector2Int cell)
    {
        filledCells.Add(cell);
    }
    public bool isAvailable(Vector2Int cell)
    {
        return filledCells.Contains(cell);
    }
    public void deleteCell(Vector2Int cell)
    {
        filledCells.Remove(cell);
    }
    public List<Vector2Int> getNodesAroundNode(Vector2Int cell)
    {
        List<Vector2Int> nodes = new List<Vector2Int>();
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1;y <= 1; y++)
            {
                Vector2Int node = new Vector2Int(x, y) + cell;
                if (x != 0 || y != 0 && isAvailable(node))
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
}
