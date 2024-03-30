using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private List<GameObject> magicSpawnList = new List<GameObject>();
    [SerializeField] private GameObject projectile;

    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private float attackSpeed = 1f;

    private float currentAttack;
    private Movement moveScript;

    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Movement>();
        currentAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentAttack -= Time.deltaTime;
    }

    void OnFire()
    {
        if (!moveScript.flipped && currentAttack <= 0)
        {
            currentAttack = attackSpeed;
            GameObject proj = Instantiate(projectile, magicSpawnList[1].transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().speed = projectileSpeed;
        }
        if (moveScript.flipped && currentAttack <= 0)
        {
            currentAttack = attackSpeed;
            GameObject proj = Instantiate(projectile, magicSpawnList[0].transform.position, Quaternion.identity);
            proj.GetComponent<Projectile>().speed = -projectileSpeed;
            proj.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }
}
