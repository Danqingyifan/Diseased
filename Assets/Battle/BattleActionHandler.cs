using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject self;
    private GameObject opponent;

    private void Awake()
    {
        if (gameObject.tag.CompareTo("Player") == 0)
        {
            self = GameObject.FindGameObjectWithTag("Player");
            opponent = GameObject.FindGameObjectWithTag("Enemy");
        }
        else if (gameObject.tag.CompareTo("Enemy") == 0)
        {
            self = GameObject.FindGameObjectWithTag("Enemy");
            opponent = GameObject.FindGameObjectWithTag("Player");
        }
    }
    void Start()
    {

    }

    private void Update()
    {
        
    }

    public void SelectAction(string actionName)
    {
        
        if(actionName.CompareTo("Attack") == 0)
        {
            Attack();
        }
        else if(actionName.CompareTo("Backpack") == 0)
        {
            OpenBackpack();
        }
        else if(actionName.CompareTo("Flee") == 0)
        {
            Flee();
        }
    }

    private void Attack()
    {
        float damage = Random.Range(15, 30);
        opponent.GetComponent<BattleStatus>().TakeDamage(damage);
        self.GetComponent<Animator>().Play("Attack");

    }

    private void OpenBackpack()
    { 

    }

    private void Flee()
    {

    }
}
