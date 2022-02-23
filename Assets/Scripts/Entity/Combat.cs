using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    RaycastHit2D hit;
    BoxCollider2D boxColl;
    Movement movement;
    public int health;
    public int damage;
    string attackTag;
    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        boxColl = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        // It's not in start just incase i want to change the values later
        if (gameObject.tag == "Enemy") attackTag = "Entity";
        if (gameObject.tag == "Entity") attackTag = "Enemy";

        CanAttack();
        animator.SetBool("Fighting", movement.fighting);

        // Check if die
        if (health <= 0) StartCoroutine(Die());
    }
    public IEnumerator Die()
    {
        // Disable box collider so it doesnt re-attack during its death animation
        GetComponent<BoxCollider2D>().enabled = false;
        movement.freeze = true;

        animator.SetBool("Die", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);

    }
    void CanAttack()
    {
        // Try in its current movement direction
        hit = Physics2D.BoxCast(boxColl.bounds.center, boxColl.size, 0, movement.moveDelta, 0.1f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == attackTag)
            {
                movement.fighting = true;
                if (IsInvoking("Attack") == false)
                {
                    InvokeRepeating("Attack", 0.4f, 1f);
                }
            }
            else // Collider exists but wrong attack tag
            {
                CancelInvoke();
                movement.fighting = false;
            }
        }
        else // Collider does not exist
        {
            CancelInvoke();
            movement.fighting = false;
        }
    }
    void Attack()
    {
        hit.collider.gameObject.GetComponent<Combat>().health -= damage;
        if (hit.collider.gameObject.GetComponent<Combat>().health <= 0)
        {
            // Stop attacking
            CancelInvoke();
        }
    }
}