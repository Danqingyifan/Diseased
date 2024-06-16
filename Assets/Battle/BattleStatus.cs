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

    [SerializeField] private float baseAttackDamage;


    //Resize Health Bar
    [SerializeField] 
    private GameObject healthBar;

    private void Awake()
    {
        dead = false;
        health = maxHealth;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        //Find parent's Animator
        

        if (health <= 0)
        {
            dead = true;
            GetComponent<Animator>().Play("Die");
        }
        else
        {
            Vector2 healthScale = healthBar.transform.localScale;
            float newHealthScaleX = healthScale.x * (health/maxHealth);
            healthBar.transform.localScale = new Vector2(newHealthScaleX, healthScale.y);
        }
    }

}
