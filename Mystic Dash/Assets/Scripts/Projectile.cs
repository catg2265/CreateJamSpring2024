using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private float deathTimer = 2f;
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        deathTimer -= Time.fixedDeltaTime;
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (deathTimer <= 0 )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            gm.points += 1;
            Destroy(this.gameObject);
        }
    }
}
