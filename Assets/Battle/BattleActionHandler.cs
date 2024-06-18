using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionHandler : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject self;
    private GameObject opponent;
    [SerializeField]
    private GameObject BattleInfo;
    private void Awake()
    {
        if (gameObject.tag.CompareTo("Player_Battle") == 0)
        {
            self = GameObject.FindGameObjectWithTag("Player_Battle");
            opponent = GameObject.FindGameObjectWithTag("Enemy");
        }
        else if (gameObject.tag.CompareTo("Enemy") == 0)
        {
            self = GameObject.FindGameObjectWithTag("Enemy");
            opponent = GameObject.FindGameObjectWithTag("Player_Battle");
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

        if (actionName.CompareTo("Attack") == 0)
        {
            Attack();

        }
        else if (actionName.CompareTo("Backpack") == 0)
        {
            OpenBackpack();
        }
        else if (actionName.CompareTo("Flee") == 0)
        {
            Flee();

        }
    }

    private void Attack()
    {
        self.GetComponent<Animator>().Play("Attack");
        BattleManager.instance.ActionMenu.SetActive(false);
        BattleManager.instance.NextTurnCoroutine();
    }

    private void TriggerDamage()
    {
        if(self.tag.CompareTo("Player_Battle") == 0)
        {
            SoundFXManager.instance.PlaySound(0);
        }else
        {
            SoundFXManager.instance.PlaySound(1);
        }
        float damage = self.GetComponent<BattleStatus>().baseAttackDamage + Random.Range(10, 20);
        opponent.GetComponent<BattleStatus>().TakeDamage(damage);
        BattleInfoManager.instance.UpdateBattleInfoText(BattleInfoTextType.ATTACK, damage, self.name, opponent.name);
    }

    private void OpenBackpack()
    {
        InventoryManager.instance.ShowInventory();
    }

    private void Flee()
    {
        BattleInfoManager.instance.UpdateBattleInfoText(BattleInfoTextType.FLEE, 0, self.name, opponent.name);
        BattleManager.instance.ActionMenu.SetActive(false);
        BattleManager.instance.NextTurnCoroutine();
    }
}
