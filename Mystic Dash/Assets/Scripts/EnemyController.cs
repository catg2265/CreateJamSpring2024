using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int attackDamage = 20;
    [SerializeField] private float attackTimer = .5f;
    private float currentAttack = 0;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D col;
    public bool enemyEnabled = true;
    

    public Transform GroundCheck;
    public LayerMask groundLayer;

    Vector3 playerPosition;


    // Update is called once per frame
    void Update()
    {

        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
        if (playerPosition.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        if (playerPosition.x < transform.position.x)
        {
            sprite.flipX = false;
        }
        if (IsGrounded())
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, Time.deltaTime * moveSpeed);
        }
        currentAttack -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HPController playerHP = collision.gameObject.GetComponent<HPController>();
            if (currentAttack <= 0)
            {
                if (enemyEnabled)
                    playerHP.TakeDamage(attackDamage);
                currentAttack = attackTimer;
            }
               
            if (playerHP.currentplayerHealth <= 0)
            {
                Destroy(collision.gameObject);
                GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
                gm.keepSpawning = false;
                foreach (GameObject i in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Destroy(i);
                }
                gm.EndScreenActivate();
            }
        }
    }
    public void DisableThis()
    {
        sprite.enabled = false;
        col.enabled = false;
        this.enabled = false;
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.5f, groundLayer);
    }
}
