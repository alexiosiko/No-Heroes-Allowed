using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBounds : MonoBehaviour
{
    public Tile edge;
    Tilemap tilemap;
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    public void DrawBound(Vector3Int pos)
    {
        tilemap.SetTile(pos, edge);
    }
}
