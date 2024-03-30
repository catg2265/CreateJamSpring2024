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
            StartCoroutine(DestroyEnemy(collision));
        }
    }
    IEnumerator DestroyEnemy(Collider2D collision)
    {
        collision.gameObject.GetComponent<EnemyController>().DisableThis();
        collision.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.points += 1;
        yield return new WaitForSeconds(1f);
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }
}
