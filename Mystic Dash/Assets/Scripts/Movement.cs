using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    public Transform GroundCheck;
    public LayerMask groundLayer;

    private Vector2 movement;
    private Rigidbody2D rb;

    [SerializeField] private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        Flip();
    }

    void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
    }
    void OnJump(InputValue input)
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    void Move()
    {
        float moveInput = movement.x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
    void Flip()
    {
        if (movement.x < 0)
        {
            playerSprite.flipX = true;
        }
        if (movement.x > 0)
        {
            playerSprite.flipX = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.1f, groundLayer);
    }
}
