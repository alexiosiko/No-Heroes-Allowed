using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GridDetails : MonoBehaviour
{
    public Tile nutrient1;
    public Tile nutrient2;
    public Tile nutrient3;
    public Tile magic1;
    public Tile magic2;
    public Tile magic3;
    private Tilemap tilemap;
    private GameManager game;
    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        game = FindObjectOfType<GameManager>();
    }
    public void DrawDetail(Detail detail)
    {
        // Draw
        tilemap.SetTile(detail.position, GetDetail(detail));

        // Store
        game.details[detail.position.x, detail.position.y] = detail;
    }
    Tile GetDetail(Detail detail)
    {
        if (detail.magicLevel > 0)
        {
            switch (detail.magicLevel)
            {
                case 0: return null;
                case 1: return magic1;
                case 2: return magic1;
                case 3: return magic1;
                case 4: return magic2;
                case 5: return magic2;
                case 6: return magic3;
                default: return magic3;
            }  
        } else
        {
            switch (detail.nutrientLevel)
            {
                case 0: return null;
                case 1: return nutrient1;
                case 2: return nutrient1;
                case 3: return nutrient1;
                case 4: return nutrient2;
                case 5: return nutrient2;
                case 6: return nutrient3;
                default: return nutrient3;
            }
        }
    }
}