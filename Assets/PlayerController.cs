using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    public float health = 50f;

    private GameOverScreen gameOverScreen;

    public Image background;

    Vector2 movement;
    
    void Start()
    {
        gameOverScreen = background.GetComponent<GameOverScreen>();
        if (gameOverScreen == null)
        {
            Debug.LogError("GameOverScreen script not found on the background GameObject.");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("PLayer is dead!\nGame is over.");
        gameOverScreen.Setup(0);
        Time.timeScale = 0f;
    }
}
