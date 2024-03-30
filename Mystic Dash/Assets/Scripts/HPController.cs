using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HPController : MonoBehaviour
{

    [SerializeField] private int totalplayerHealth = 100;
    public int currentplayerHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        currentplayerHealth = totalplayerHealth;
        healthBar.SetMaxHealth(totalplayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentplayerHealth);

        if (input.actions["Sacrifice"].WasPressedThisFrame())
        {
            currentplayerHealth = totalplayerHealth;
            GetComponent<Movement>().freezeMovement = true;
        }
    }
    public void TakeDamage(int damage)
    {
        currentplayerHealth -= damage;
    }
}
