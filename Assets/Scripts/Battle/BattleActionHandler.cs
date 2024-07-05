using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public SoundFXManager soundFXManager;
    private GameObject self;
    private GameObject opponent;
    [SerializeField]
    private GameObject BattleInfo;
    private void Awake()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player_Battle");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy_Battle");

        GameObject player = null;
        GameObject enemy = null;

        foreach (GameObject _player in players)
        {
            if (_player.activeInHierarchy)
            {
                player = _player;
            }
        }

        foreach (GameObject _enemy in enemies)
        {
            if (_enemy.activeInHierarchy)
            {
                enemy = _enemy;
            }
        }
        if (gameObject.tag.CompareTo("Player_Battle") == 0)
        {
            self = player;
            opponent = enemy;
        }
        else if (gameObject.tag.CompareTo("Enemy_Battle") == 0)
        {
            self = enemy;
            opponent = player;
        }
    }
    void Start()
    {

    }

    private void Update()
    {

    }

    private void OnEnable()
    {
        GameEvents.OnUseBattleItem += UseBattleItem;
    }

    private void OnDisable()
    {
        GameEvents.OnUseBattleItem -= UseBattleItem;
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

    private void TriggerDamage(int baseDamage)
    {
        if (self.tag.CompareTo("Player_Battle") == 0)
        {
            soundFXManager.PlaySound(0);
        }
        else
        {
            soundFXManager.PlaySound(1);
        }
        float damage = (self.GetComponent<BattleStatus>().baseAttackDamage + Random.Range(baseDamage, baseDamage + 5)) * (1 - opponent.GetComponent<BattleStatus>().baseDefense / 100);
        opponent.GetComponent<BattleStatus>().TakeDamage(damage);
        BattleManager.instance.battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.ATTACK, (int)damage, self.name, opponent.name);
    }

    private void TriggerChalkDamage(int baseDamage)
    {
        if (self.tag.CompareTo("Player_Battle") == 0)
        {
            soundFXManager.PlaySound(0);
        }
        float damage = baseDamage * (1 - opponent.GetComponent<BattleStatus>().baseDefense / 100);
        opponent.GetComponent<BattleStatus>().TakeDamage(damage);
    }

    private void OpenBackpack()
    {
        InventoryManager.instance.ShowInventory();
    }

    private void Flee()
    {
        BattleManager.instance.battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.FLEE, 0, self.name, opponent.name);
        BattleManager.instance.ActionMenu.SetActive(false);
        BattleManager.instance.NextTurnCoroutine();
    }

    private void UseBattleItem(string itemName)
    {
        if (itemName.CompareTo("·Û±Ê") == 0)
        {
            UseChalk();
            BattleManager.instance.battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.USEITEM, 0, self.name, "·Û±Ê");
        }
        else if (itemName.CompareTo("Ðí½¿µÄ»³±í") == 0)
        {
            UseClock();
            BattleManager.instance.battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.USEITEM, 0, self.name, "Ðí½¿µÄ»³±í");
        }
        else if (itemName.CompareTo("ÎºÑ©ÄþµÄÑÛ¾µ") == 0)
        {
            UseGlasses();
            BattleManager.instance.battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.USEITEM, 0, self.name, "ÎºÑ©ÄþµÄÑÛ¾µ");
        }
        else if(itemName.CompareTo("´´¿ÚÌù") == 0)
        {
            GameEvents.RaiseHealthModified(12);
        }
    }

    private void UseChalk()
    {
        if(self.tag.CompareTo("Player_Battle") == 0)
        {
            TriggerChalkDamage(10);
        }
    }

    private void UseClock()
    {
        BattleManager.instance.battleStatuses.RemoveAt(0);
        BattleManager.instance.battleStatuses.Add(BattleManager.instance.GetPlayer().GetComponent<BattleStatus>());
    }
    private void UseGlasses()
    {

    }
}
