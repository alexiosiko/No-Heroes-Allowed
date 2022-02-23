using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int width = 80;
    public int height = 48;
    public Cell[,] cells;
    public Detail[,] details;
    public Floor[,] floors;
    GridCells gridCells;
    GridWalls gridWalls;
    GridFloor gridFloor;
    GridBounds gridBounds;
    GridDetails gridDetails;
    void Awake()
    {
        gridBounds = FindObjectOfType<GridBounds>();
        gridFloor = FindObjectOfType<GridFloor>();
        gridWalls = FindObjectOfType<GridWalls>();
        gridCells = FindObjectOfType<GridCells>();
        gridDetails = FindObjectOfType<GridDetails>();

        floors = new Floor[width, height];
        details = new Detail[width, height];
        cells = new Cell[width, height];
        Camera.main.transform.position = new Vector3(width / 2, height - 1, -10);
    }
    void Start()
    {
        NewGame();
    }
    void NewGame()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Cells
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                if (y > 36)
                    cell.type = Cell.Type.level1;
                else if (y > 24)
                    cell.type = Cell.Type.level2;
                else if ( y > 12)
                    cell.type = Cell.Type.level3;
                else
                    cell.type = Cell.Type.level4;
                
                gridCells.DrawCell(cell);

                // Nutrients
                Detail detail = new Detail();
                detail.position = new Vector3Int(x, y, 0);
                detail.nutrientLevel = GetRandNutrientLevel(detail);
                detail.magicLevel = 0;
                gridDetails.DrawDetail(detail);

                // Walls
                Wall wall = new Wall();
                wall.position = new Vector3Int(x, y, 0);
                wall.type = Wall.Type.wallDirt;
                gridWalls.DrawWall(wall);

                // Floor
                Floor floor = new Floor();
                floor.type = Floor.Type.empty;
                floor.position = new Vector3Int(x, y, 0);
                gridFloor.DrawFloor(floor);

                // Bounds
                if (y == 0)
                    gridBounds.DrawBound(new Vector3Int(x, y - 1, 0));
                if (y == height - 1)
                    gridBounds.DrawBound(new Vector3Int(x, y + 1, 0));
                if (x == 0)
                    gridBounds.DrawBound(new Vector3Int(x - 1, y, 0));
                if (x == width - 1)
                    gridBounds.DrawBound(new Vector3Int(x + 1, y, 0));

            }
        }
        
        // Get top middle pos
        int centerX = width / 2;
        int centerY = height - 1;

        for (int y = 0; y < 5; y++)
        {
            // Break cells
            Cell cellTemp = cells[centerX, centerY - y];
            cellTemp.type = Cell.Type.empty;
            // Draw
            gridCells.DrawCell(cellTemp);

            // Break details
            Detail detailTemp = details[centerX, centerY - y];
            detailTemp.nutrientLevel = 0;
            // Draw
            gridDetails.DrawDetail(detailTemp); 

            // Break the one cell to the right ish ish
            if (y == 4)
            {
                Cell cellTemp1 = cells[centerX + 1, centerY - y];
                cellTemp1.type = Cell.Type.empty;
                gridCells.DrawCell(cellTemp1);

                Detail detailTemp1 = details[centerX + 1, centerY - y];
                detailTemp1.nutrientLevel = 0;
                gridDetails.DrawDetail(detailTemp);
            }
        }
    }
    int GetRandNutrientLevel(Detail detail)
    {
        int rand = Random.Range(0, 30);
        if (rand == 0 || rand == 1 || rand == 2 || rand == 10)
        {
            return 1;
        } else if (rand == 3 || rand == 4)
        {
            return 3;
        } else if (rand == 5)
        {   
            return 4;
        } else
        {
            return 0;
        }
    }
}