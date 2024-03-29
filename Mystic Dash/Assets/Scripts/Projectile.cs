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
        deathTimer = deathTimer - Time.fixedDeltaTime;
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if (deathTimer <= 0 )
        {
            Destroy(this.gameObject);
        }
    }
}
