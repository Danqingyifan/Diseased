using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterStats characterStats;
    private bool dead;

    [SerializeField] public int maxHealth;

    public int health;

    [SerializeField] public float baseAttackDamage;
    [SerializeField] public float baseDefense = 0;

    //Resize Health Bar
    [SerializeField]
    public HealthBar healthBar;

    private void Awake()
    {

    }

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnEnable()
    {
        GameEvents.OnHealthModified += HandleHealthChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnHealthModified -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int amount)
    {
        if (gameObject.tag.CompareTo("Player_Battle") == 0)
        {
            health += amount;
            health = (int)Mathf.Clamp(health, 0, maxHealth);
            healthBar.SetHealth(health);

            if (characterStats != null)
            {
                characterStats.health += amount;
                characterStats.health = (int)Mathf.Clamp(health, 0, maxHealth);
                characterStats.UpdateStatDisplays();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        health = Mathf.Clamp(health, 0, maxHealth);
        healthBar.SetHealth(health);
        GetComponent<Animator>().Play("TakeDamage");

        if (characterStats != null)
        {
            characterStats.health -= (int)damage;
            characterStats.UpdateStatDisplays();
        }


        if (health <= 0)
        {
            dead = true;
            GetComponent<Animator>().Play("Die");

            BattleManager.instance.EndBattle();

            if (characterStats != null)
            {
                characterStats.health = 20;
                characterStats.UpdateStatDisplays();
            }
        }
    }

    public bool getDead()
    {
        return dead;
    }

    public void ApplyPlayerCharacterStats()
    {
        characterStats.attack = (int)baseAttackDamage;
        characterStats.health = (int)health;
    }

    public void initBattleStatus()
    {
        dead = false;
        if (characterStats != null)
        {
            baseAttackDamage = characterStats.attack;
            health = characterStats.health;
            maxHealth = characterStats.maxHealth;
        }
        else
        {
            health = maxHealth;
        }
    }
}
