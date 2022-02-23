using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject mutator;
    public int foodLevel = 0;
    void Start()
    {
        InvokeRepeating("Hunger", 15, 60);
    }
    void Update()
    {
        if (foodLevel > 1)
        {
            if (IsInvoking("Mutate") == false)
            {
                InvokeRepeating("Mutate", 10, 10);
            }
        }
        if (foodLevel < -3)
        {
            StartCoroutine(GetComponent<Combat>().Die());
        }
    }
    void Mutate()
    {
        int rand = Random.Range(0, 10);
        if (rand == 0)
        {
            Instantiate<GameObject>(mutator, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void Hunger()
    {
        foodLevel -= 1;
    }
}