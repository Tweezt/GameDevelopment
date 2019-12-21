using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int health;
    private int currentHealth;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            PlayerController.instance.enemiesKilledNumber += 1;
            PlayerController.instance.EnemyKilled();
        }     
    }

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage;
    }
}
