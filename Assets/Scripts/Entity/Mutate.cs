using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutate : MonoBehaviour
{
    public string food = "Food(Clone)";
    public GameObject mutator;
    Movement movement;
    Animator animator;
    BoxCollider2D boxCol;
    public int hunger = 0;
    bool waiting = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        boxCol = GetComponent<BoxCollider2D>();
        movement = GetComponent<Movement>();
    }
    void Update()
    {
        if (waiting == false)
            Eat();
        if (hunger > 10)
        {
            MutateNow();
        }
    }
    void Eat()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxCol.size, 0, movement.moveDelta, 0.1f);        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.name == food)
            {
                hit.collider.gameObject.GetComponent<Movement>().freeze = true;
                movement.freeze = true;
                animator.SetBool("Eating", true);
                StartCoroutine(Wait(hit));
            }
        }
    }
    IEnumerator Wait(RaycastHit2D hit)
    {
        waiting = true;

        yield return new WaitForSeconds(0.5f);

        Destroy(hit.collider.gameObject);
        hunger += 1;
        animator.SetBool("Eating", false);
        movement.freeze = false;


        yield return new WaitForSeconds(10);
        waiting = false;
    }
    void MutateNow()
    {
        Instantiate<GameObject>(mutator, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
