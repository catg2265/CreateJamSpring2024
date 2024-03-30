using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{

    [SerializeField] private int totalplayerHealth = 100;
    public int currentplayerHealth;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentplayerHealth = totalplayerHealth;
        healthBar.SetMaxHealth(totalplayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Add control of hp bar
        healthBar.SetHealth(currentplayerHealth);
    }
    public void TakeDamage(int damage)
    {
        currentplayerHealth -= damage;
    }
}
