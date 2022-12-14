using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fertility : MonoBehaviour
{
    public bool enchanted = false;
    private GameManager game;
    private GridDetails gridDetails;
    private Animator animator;
    private new UnityEngine.Rendering.Universal.Light2D light;
    public int nutrientLevel = 0;
    public int magicLevel = 0;
    void Awake()
    {
        light = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        animator = GetComponent<Animator>();
        gridDetails = FindObjectOfType<GridDetails>();
        game = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        InvokeRepeating("Fertilize", 1f, 0.5f);
    }
    void Fertilize()
    {
        Vector3Int offset;
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0:  offset = new Vector3Int( 0, 1, 0); break;
            case 1:  offset = new Vector3Int( 0,-1, 0); break;
            case 2:  offset = new Vector3Int( 1, 0, 0); break;
            case 3:  offset = new Vector3Int(-1, 0, 0); break;
            default: offset = new Vector3Int( 0, 0, 0); break;
        }

        // Out of bounds -> return
        if ((int)transform.position.x + offset.x < 0 || (int)transform.position.x + offset.x >= game.width || (int)transform.position.y + offset.y < 0 || (int)transform.position.y + offset.y >= game.height)
        {
            return;
        }

        // Get cell and nutrient from random direction
        Cell cell = game.cells[(int)(transform.position.x + offset.x), (int)(transform.position.y + offset.y)];
        Detail detail = game.details[(int)(transform.position.x + offset.x), (int)(transform.position.y + offset.y)];

        
        // If empty cell
        if (cell.type == Cell.Type.empty)
        {
            return;
        }

        // Magic
        if (enchanted == true)
        {
            if (magicLevel > 0) // Spit
            {
                if (detail.magicLevel < 6 && detail.magicLevel > 0 && detail.nutrientLevel == 0)
                {
                    // Shine
                    StartCoroutine(Shine());
                    
                    detail.magicLevel += magicLevel;
                    magicLevel = 0;
                    animator.SetBool("Fertile", false);
                }
            } else // Suck
            {
                if (detail.magicLevel > 0 && detail.magicLevel < 6 )
                {
                    // Shine
                    StartCoroutine(Shine());

                    magicLevel = 1;
                    detail.magicLevel -= 1;
                    animator.SetBool("Fertile", true);  
                    
                    // Add to foodLevel
                    GetComponent<Life>().foodLevel += 1;
                }        
            } // End magic
        } else if (enchanted == false) // Nutrient
        {
            if (nutrientLevel > 0)
            {
                if (detail.nutrientLevel < 6 && detail.nutrientLevel > 0 && detail.magicLevel == 0) // Spit
                {
                    // Shine
                    StartCoroutine(Shine());

                    detail.nutrientLevel += nutrientLevel;
                    nutrientLevel = 0;
                    animator.SetBool("Fertile", false);
                }
            } else // Suck
            {
                if (detail.nutrientLevel > 0 && detail.nutrientLevel < 6)
                {
                    // Shine
                    StartCoroutine(Shine());

                    nutrientLevel = 1;
                    detail.nutrientLevel -= 1;
                    animator.SetBool("Fertile", true);  

                    // Add to foodLevel
                    GetComponent<Life>().foodLevel += 1;
                }        
            } // End nutrient
        }

        // Draw
        gridDetails.DrawDetail(detail);
    }
    IEnumerator Shine()
    {
        float store = light.intensity;
        light.intensity = 0.3f;
        yield return new WaitForSeconds(0.3f);
        light.intensity = store;
    }
}
