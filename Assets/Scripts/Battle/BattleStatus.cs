using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleStatus : MonoBehaviour
{
    // Start is called before the first frame update

    private bool dead;

    [SerializeField] private float maxHealth;

    private float health;

    [SerializeField] public float baseAttackDamage;


    //Resize Health Bar
    [SerializeField]
    private HealthBar healthBar;

    private void Awake()
    {
        dead = false;
        health = maxHealth;
    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.SetHealth(health);
        GetComponent<Animator>().Play("TakeDamage");

        if (health <= 0)
        {
            dead = true;
            GetComponent<Animator>().Play("Die");
            BattleManager.instance.EndBattle();
        }
    }

    public bool getDead()
    {
        return dead;
    }
}
