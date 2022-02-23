using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridWalls : MonoBehaviour
{
    public Tile tileWall;
    private GameManager game;
    private Tilemap tilemap;
    void Awake()
    {
        game = FindObjectOfType<GameManager>();
        tilemap = GetComponent<Tilemap>();
    }
    public void DrawWall(Wall wall)
    {
        // Draw
        tilemap.SetTile(wall.position, GetWall(wall));
       
        // No need to store
    }
    Tile GetWall(Wall wall)
    {
        switch (wall.type)
        {
            case Wall.Type.wallDirt: return tileWall;
            default: return null;
        }
    }
}
