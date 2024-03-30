using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.3f;
    [SerializeField] private int totalFreezeTimer = 5;

    public Transform GroundCheck;
    public LayerMask groundLayer;
    public bool flipped = false;

    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private float jumpTimer;
    public bool freezeMovement = false;
    private float freezeTimer;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private CooldownBar cooldownBar;
    [SerializeField] private GameObject CoolDownBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        freezeTimer = totalFreezeTimer;
        cooldownBar.SetMaxCooldown(totalFreezeTimer);
        CoolDownBar.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!freezeMovement)
        {
            Move();
        }        
    }
    private void Update()
    {
        Flip();
        playerSprite.flipX = flipped;

        if (IsGrounded() && playerInput.actions["Jump"].WasPressedThisFrame())
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (isJumping && playerInput.actions["Jump"].IsPressed())
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (playerInput.actions["Jump"].WasReleasedThisFrame())
        {
            isJumping = false;
            jumpTimer = 0;
        }

        if (freezeMovement)
        {
            CoolDownBar.SetActive(true);
            cooldownBar.SetMaxCooldown(totalFreezeTimer);
            cooldownBar.SetCooldown(Mathf.RoundToInt(freezeTimer));
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                freezeMovement = false;
                freezeTimer = totalFreezeTimer;
                CoolDownBar.SetActive(false);
            }
                
        }

    }

    void OnMove(InputValue input)
    {
        movement = input.Get<Vector2>();
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
            flipped = true;
        }
        if (movement.x > 0)
        {
            flipped = false;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.Raycast(GroundCheck.position, Vector2.down, 0.6f, groundLayer);
    }
}
