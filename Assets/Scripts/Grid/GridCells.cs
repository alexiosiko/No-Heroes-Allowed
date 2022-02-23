using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridCells : MonoBehaviour
{
    public Tile tile1;
    public Tile tile2;
    public Tile tile3;
    public Tile tile4;

    private Tilemap tilemap;
    private GameManager game;
    void Awake()
    {
        game = FindObjectOfType<GameManager>();
        tilemap = GetComponent<Tilemap>();
    }
    public void DrawCell(Cell cell)
    {
        // Draw
        tilemap.SetTile(cell.position, GetCell(cell));

        // Store
        game.cells[cell.position.x, cell.position.y] = cell;
    }
    Tile GetCell(Cell cell)
    {
        switch (cell.type)
        {
            case Cell.Type.empty: return null;
            case Cell.Type.level1: return tile1;
            case Cell.Type.level2: return tile2;
            case Cell.Type.level3: return tile3;
            case Cell.Type.level4: return tile4;

            // Just incase
            default: return null;
        }
    }
}
