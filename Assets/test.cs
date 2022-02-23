using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject temp;
    void Update()
    {
        transform.position = temp.GetComponent<BoxCollider2D>().bounds.center;
    }
}
