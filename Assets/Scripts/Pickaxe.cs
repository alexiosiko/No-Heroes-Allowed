using System;
using System.Collections;
using UnityEngine;
public class Pickaxe : MonoBehaviour
{
    public GameObject slimeGreen;
    public GameObject slimeMagic;
    public GameObject nutrientLevel4;
    public GameObject nutrientLevel6; 
    public GameObject magicLevel4;
    public GameObject magicLevel6;
    Animator animator;
    GridDetails gridDetails;
    GridCells gridCells;
    GridFloor gridFloor;
    GameManager game;
    void Awake()
    {
        animator = GetComponent<Animator>();
        gridDetails = FindObjectOfType<GridDetails>();
        gridCells = FindObjectOfType<GridCells>();
        gridFloor = FindObjectOfType<GridFloor>();
        game = FindObjectOfType<GameManager>();
        
        Cursor.visible = false;
    }
    void Update()
    {
        // Pickaxe position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cellPos = new Vector2( (int)mousePos.x, (int)mousePos.y);

        // Sprite offset to allign with cursor pointer
        transform.position = mousePos + new Vector3(0.6f, 0.8f, 10);

        // Get click
        Click(cellPos);
    }
    void Click(Vector3 cellPos)
    {
        if (CanBreak( new Vector2Int( (int)cellPos.x, (int)cellPos.y) ) == false)
            return;

        if (Input.GetMouseButton(0))
        {
            // Start pickaxe animation
            animator.SetBool("Clicking", true);


            // Break cell
            Cell cell = game.cells[(int)cellPos.x, (int)cellPos.y];
            if (cell.type != Cell.Type.empty)
            {
                cell.type = Cell.Type.empty;
            }

            // Break detail
            Detail detail = game.details[(int)cellPos.x, (int)cellPos.y];
    
            // Magics
            if (detail.magicLevel > 0 && detail.magicLevel <= 3)
            {
                Instantiate<GameObject>(slimeMagic, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            } 
            else if (detail.magicLevel >= 4 && detail.magicLevel < 6)
            {
                Instantiate<GameObject>(magicLevel4, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            } 
            else if (detail.magicLevel >= 6)
            {
                Instantiate<GameObject>(magicLevel6, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }

            // Nutrients
            else if (detail.nutrientLevel > 0 && detail.nutrientLevel <= 3)
            {
                Instantiate<GameObject>(slimeGreen, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            } 
            else if (detail.nutrientLevel >= 4 && detail.nutrientLevel < 6)
            {
                Instantiate<GameObject>(nutrientLevel4, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            } else if (detail.nutrientLevel >= 6)
            {
                Instantiate<GameObject>(nutrientLevel6, detail.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            }

            // Erase detail levels
            detail.nutrientLevel = 0;
            detail.magicLevel = 0;


            // Draw
            gridDetails.DrawDetail(detail);
            gridCells.DrawCell(cell);
        }
        else
        {
            // Stop pickaxe animation
            animator.SetBool("Clicking", false);
        }
    }
    bool CanBreak(Vector2Int cellPos)
    {
        // Check left right up down
        if (cellPos.x < 0 || cellPos.x >= game.width || cellPos.y < 0 || cellPos.y >= game.height)
        {
            return false;
        }

        if (cellPos.x - 1 >= 0)
        {
            if (game.cells[cellPos.x - 1, cellPos.y].type == Cell.Type.empty)
            return true;
        }

        if (cellPos.x + 1 < game.width)
        {
            if (game.cells[cellPos.x + 1, cellPos.y].type == Cell.Type.empty)
                return true;
        }

        if (cellPos.y - 1 >= 0)
        {
            if (game.cells[cellPos.x, cellPos.y - 1].type == Cell.Type.empty)
                return true;
        }

        if (cellPos.y + 1 < game.height)
        {
            if (game.cells[cellPos.x, cellPos.y + 1].type == Cell.Type.empty)
                return true;
        }
        return false;
    }
}