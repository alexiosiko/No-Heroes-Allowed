using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    public float speed = 10;
    public float differenceX = 10;
    public float differenceY = 8;
    GameManager gameManager;
    Vector3 mouse;
    Vector3 cam;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        //mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //cam = transform.position;

        //MoveX();
        //MoveY();

        Scroll();
        MoveControls();
    }
    void MoveControls()
    {
        if (Input.GetKey(KeyCode.A)) // Key left
        {
            transform.position += new Vector3(-Time.deltaTime  * speed / 2, 0, 0);
        }

        if (Input.GetKey(KeyCode.D)) // Key right
        {
            transform.position += new Vector3( Time.deltaTime  * speed / 2, 0, 0);
        }

        if (Input.GetKey(KeyCode.W)) // Key up
        {
            transform.position += new Vector3(0, Time.deltaTime  * speed / 2, 0);
        }

        if (Input.GetKey(KeyCode.S)) // Key down
        {
            transform.position += new Vector3(0, -Time.deltaTime  * speed / 2, 0);
        }

    }
    void MoveX()
    {
        // Move
        if (mouse.x - cam.x > differenceX || mouse.x - cam.x < -differenceX)
        {
            transform.position = Vector3.MoveTowards(new Vector3(cam.x, cam.y, -10), new Vector3(mouse.x, cam.y, -10), Time.deltaTime * speed);
        }

        // Out of bounds -> return
        if (cam.x < 0 || cam.x > gameManager.width)
        {
            transform.position = Vector3.MoveTowards(new Vector3(cam.x, cam.y, -10), new Vector3(-mouse.x, cam.y, -10), Time.deltaTime * speed);
        }
    }
    void MoveY()
    {
        // Move
        if (mouse.y - cam.y > differenceY || mouse.y - cam.y < -differenceY)
        {
            transform.position = Vector3.MoveTowards(new Vector3(cam.x, cam.y, -10), new Vector3(cam.x, mouse.y, -10), Time.deltaTime * speed);
        }
        // Out of bounds -> return
        if (cam.y < 0 || cam.y > gameManager.height)
        {
            transform.position = Vector3.MoveTowards(new Vector3(cam.x, cam.y, -10), new Vector3(cam.x, -mouse.y, -10), Time.deltaTime * speed);
        }
    }
    void Scroll()
    {
        if (Input.mouseScrollDelta == new Vector2(0, -1) && Camera.main.orthographicSize < 14) // Scroll back
        {
            Camera.main.orthographicSize += 1;
        } 
        else if (Input.mouseScrollDelta == new Vector2(0, 1) && Camera.main.orthographicSize > 3) // Scroll forward
        {
            Camera.main.orthographicSize += -1;
        }
    }
}