using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    GameManager game;
    GridDetails gridNut;
    Animator animator;
    Movement movement;
    void Awake()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        gridNut = FindObjectOfType<GridDetails>();
        game = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        // Slime to stem
        StartCoroutine(Pause(3f));

        InvokeRepeating("GrowOrDie", 10, 10);
    }
    void GrowOrDie()
    {
        int rand = Random.Range(0, 10);

        if (rand == 0)
        {
            animator.SetBool("Die", true);
            StartCoroutine(Die());
        }
        else if (rand == 1 || rand == 2 || rand == 3)
        {
                // Stem to flower
                StartCoroutine(Pause(3f));

                animator.SetBool("FlowerGrow", true);
                CancelInvoke("DieOrFertilize");
                InvokeRepeating("DieOrFertilize", 10, 20);
        }
    }
    void DieOrFertilize()
    {
        int rand = Random.Range(0, 8);
        if (rand == 0) // Die
        {
            
            animator.SetBool("Die", true);
            StartCoroutine(Die());
        }

        // Flower fertilize
        StartCoroutine(Pause(1));
        Vector3Int pos = new Vector3Int( (int)transform.position.x, (int)transform.position.y, 0);
        for (int offsetX = -3; offsetX <= 3; offsetX++)
        {
            for (int offsetY = -3; offsetY <= 3; offsetY++)
            {
                // Check out of bounds
                if (pos.x + offsetX >= game.height || pos.x + offsetX < 0 ||
                pos.y + offsetY >= game.height || pos.y + offsetY < 0)
                {
                    continue;
                }  
                int randLevel = Random.Range(0, 3);
                if (game.cells[pos.x + offsetX, pos.y + offsetY].type != Cell.Type.empty)
                {
                    if (Random.Range(0, 5) == 0)
                        return;

                    Detail nutrient = game.details[pos.x + offsetX, pos.y + offsetY];
                    if (nutrient.nutrientLevel == 0)
                    {
                        nutrient.magicLevel += randLevel;
                    }
                    else if (nutrient.magicLevel == 0)
                    {
                        nutrient.nutrientLevel += randLevel;
                    }
                    
                    gridNut.DrawDetail(nutrient);
                } 
            }
        }
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    IEnumerator Pause(float seconds)
    {
        GetComponent<Movement>().freeze = true;
        yield return new WaitForSeconds(seconds);
        GetComponent<Movement>().freeze = false;
    }
}