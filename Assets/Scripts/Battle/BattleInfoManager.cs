using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum BattleInfoTextType
{
    ATTACK,
    HEAL,
    FLEE,
    TIPS,
    BATTLEEND,
    USEITEM
}
public class BattleInfoManager : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    private BattleStatus playerStatus;
    private BattleStatus enemyStatus;

    public TextMeshProUGUI battleInfoText;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
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
        
        playerStatus = player.GetComponent<BattleStatus>();
        enemyStatus = enemy.GetComponent<BattleStatus>();
    }

    public void UpdateBattleInfoText(BattleInfoTextType type, float value,string instigatorName,string victimName)
    {
        switch (type)
        {
            case BattleInfoTextType.ATTACK:
                battleInfoText.text = instigatorName + "攻击了" + victimName + "，造成了" + value + "点伤害。";
                break;
            case BattleInfoTextType.HEAL:
                battleInfoText.text = instigatorName + "为" + victimName + "恢复了" + value + "点生命值。";
                break;
            case BattleInfoTextType.FLEE:
                battleInfoText.text = "早就知道你想逃跑了，跑不了一点。";
                break;
            case BattleInfoTextType.TIPS:
                battleInfoText.text = "这是" + instigatorName + "的回合。";
                break;
            case BattleInfoTextType.BATTLEEND:
                battleInfoText.text = "战斗结束！" + instigatorName + "击败了" + victimName + "！";
                break;
            case BattleInfoTextType.USEITEM:
                battleInfoText.text = instigatorName + "使用了" + victimName + "。";
                break;
            default:
                break;
        }
    }
}
