using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject turtle;
    GameManager game;
    bool active = false;
    void Awake()
    {
        game = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (active)
        {
            int randX = Random.Range(0 , game.width);
            int randY = Random.Range(0, game.height);
            if (game.cells[randX, randY].type == Cell.Type.empty)
            {
                Instantiate<GameObject>(turtle, new Vector3(randX + 0.5f, randY + 0.5f, 0), Quaternion.identity);
                active = false;
            }
        }

    }
    void OnMouseDown()
    {
        active = true;
    }
}
