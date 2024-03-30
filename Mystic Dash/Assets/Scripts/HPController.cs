using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{

    [SerializeField] private float totalplayerHealth = 100f;
    public float currentplayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentplayerHealth = totalplayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Add control of hp bar
    }
    public void TakeDamage(float damage)
    {
        currentplayerHealth -= damage;
    }
}
