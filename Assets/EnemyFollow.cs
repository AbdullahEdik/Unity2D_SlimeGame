using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public float speed = 1.0f;
    public Animator animator;

    private float distance;


    private float attackDistance = 0.1f;
    private float attackDamage = 10.0f;
    private float attackCooldown = 2.0f;
    private bool isAttacking = false;
    private float attackTimer = 0.0f;
    private PlayerController playerController;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController script not found on the player GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);

        if (!isAttacking && distance < attackDistance)
        {
            if (attackTimer <= 0)
            {
                AttackPlayer();
                attackTimer = attackCooldown; // Reset cooldown timer
            }
        }
        else
        {
            // Move towards the player if not attacking
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        // Update attack cooldown timer
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void AttackPlayer()
    {
        playerController.TakeDamage(attackDamage);

        // Set isAttacking to true to prevent continuous attacks
        isAttacking = true;

        // Reset isAttacking flag after a certain delay
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }
}
