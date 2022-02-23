using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    BoxCollider2D boxColl;
    public Vector3 moveDelta;
    RaycastHit2D hit;
    public bool fighting = false;
    public bool freeze = false;
    public float speed = 1;
    List<Vector3Int> nodes = new List<Vector3Int>();
    GameManager game;
    Vector3 pos;
    float posX;
    float posY;
    bool centerMoving = false;
    void Awake()
    {
        game = FindObjectOfType<GameManager>();
        boxColl = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        GetInput();
    }
    void FixedUpdate()
    { 
        if (fighting == false && freeze == false)
        {
            Move();
            SpriteDirection();
        }
    }
    void GetInput()
    {
        int rand = Random.Range(0, 4);
        switch (rand)
        {
            case 0: moveDelta = new Vector3(    Time.fixedDeltaTime,                              0, 0); break;
            case 1: moveDelta = new Vector3(   -Time.fixedDeltaTime,                              0, 0); break;
            case 2: moveDelta = new Vector3(                                0,  Time.fixedDeltaTime, 0   ); break;
            case 3: moveDelta = new Vector3(                                0, -Time.fixedDeltaTime, 0   ); break;
        }
    }
    void Move()
    {
        if (centerMoving == false)
        {
            // Find when in center of Cell,
            pos = transform.position;
    
            // Get tenth of pos * 10
            // 5.5 -> 55
            posX = Mathf.Round(pos.x * 10);
            posY = Mathf.Round(pos.y * 10);
    
            if (posX % 5 == 0 && posX % 10 != 0 && posY % 5 == 0 && posY % 10 != 0)
            {
                centerMoving = true;
                GetInput();
                Invoke("CenterMovingDone", 5);
            }
        }

        // If slime is touching wall
        hit = Physics2D.BoxCast(boxColl.bounds.center, boxColl.size, 0, moveDelta, 0.05f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Tile")
            {
                // Move onestep back
                transform.position -= moveDelta * speed;
                // Get random direction
                GetInput();
            }
        }
        transform.position += moveDelta * speed;
    }
    void SpriteDirection()
    {
        if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void CenterMovingDone()
    {
        centerMoving = false;
    }
}