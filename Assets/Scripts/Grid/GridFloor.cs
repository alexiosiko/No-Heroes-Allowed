using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridFloor : MonoBehaviour
{
    Tilemap tilemap;
    GameManager game;
    void Awake()
    {
        game = FindObjectOfType<GameManager>();
        tilemap = GetComponent<Tilemap>();
    }
    public void DrawFloor(Floor floor)
    {
        // Draw
        tilemap.SetTile(floor.position, GetFloor(floor));

        // Store
        game.floors[floor.position.x, floor.position.y] = floor;
    }
    Tile GetFloor(Floor floor)
    {
        switch (floor.type)
        {
            case Floor.Type.empty: return null;
            default: Debug.Log("Defaulting"); return null;
        }
    }
}
