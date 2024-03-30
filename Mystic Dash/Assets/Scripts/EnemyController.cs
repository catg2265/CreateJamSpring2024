using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float attackTimer = .5f;
    private float currentAttack = 0;

    public Transform GroundCheck;
    public LayerMask groundLayer;

    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
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
                playerHP.TakeDamage(attackDamage);
            if (playerHP.currentplayerHealth <= 0)
            {
                // Kill player
                // End Game
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.1f, groundLayer);
    }
}