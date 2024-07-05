using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public AudioSource audioSource;
    public List<BattleStatus> battleStatuses;

    public BattleInfoManager battleInfoManager;

    private GameObject player;
    private GameObject enemy;
    [SerializeField]
    public GameObject ActionMenu;
    public GameObject EndBattleButton; // 结束战斗按钮
    public Flowchart flowchart; // 引用 Fungus Flowchart
    public string winBlockName;
    public string loseBlockName;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        /*else
        {
           // if (instance != this)
            //{
              //  Destroy(gameObject);
            //}
        }*/
    }

    void Start()
    {

    }

    void Update()
    {

    }

    // 公共方法，用于启动战斗
    public void TriggerBattle()
    {
        StartBattle();
    }

    public void StartBattle()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player_Battle");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy_Battle");
        GameObject[] battleManagers = GameObject.FindGameObjectsWithTag("BattleManager");
        // 过滤出激活的玩家对象
        foreach (GameObject _player in players)
        {
            if (_player.activeInHierarchy)
            {
                player = _player;
            }
        }

        // 过滤出激活的敌人对象
        foreach (GameObject _enemy in enemies)
        {
            if (_enemy.activeInHierarchy)
            {
                enemy = _enemy;
            }
        }

        foreach (GameObject _battleManager in battleManagers)
        {
            if (_battleManager.activeInHierarchy)
            {
                instance = _battleManager.GetComponent<BattleManager>();
            }
        }

        battleStatuses = new List<BattleStatus>();
        EndBattleButton.SetActive(false); // 初始化时隐藏结束按钮

        player.GetComponent<BattleStatus>().initBattleStatus();
        enemy.GetComponent<BattleStatus>().initBattleStatus();

        player.GetComponent<BattleStatus>().healthBar.SetMaxHealth(player.GetComponent<BattleStatus>().maxHealth);
        player.GetComponent<BattleStatus>().healthBar.SetHealth(player.GetComponent<BattleStatus>().health);   
        
        battleStatuses.Add(player.GetComponent<BattleStatus>());
        
        audioSource.Play();
        ActionMenu.SetActive(false);
        NextTurn();
    }

    public void EndBattle()
    {
        ActionMenu.SetActive(false);
        //StartCoroutine(DestroySceneAfterDelay());
        EndBattleButton.SetActive(true); // 显示结束按钮
        player.GetComponent<BattleStatus>().ApplyPlayerCharacterStats();
    }

    public void NextTurn()
    {
        BattleStatus currentStatus = battleStatuses[0];
        battleStatuses.RemoveAt(0);
        if (!currentStatus.getDead())
        {
            GameObject currentUnit = currentStatus.gameObject;
            if (currentUnit.tag == "Player_Battle")
            {
                if (!enemy.GetComponent<BattleStatus>().getDead())
                {
                    ActionMenu.SetActive(true);
                    battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.TIPS, 0, player.name, player.name);
                    battleStatuses.Add(enemy.GetComponent<BattleStatus>());
                }
            }
            else if (currentUnit.tag == "Enemy_Battle")
            {
                if (!player.GetComponent<BattleStatus>().getDead())
                {
                    currentUnit.GetComponent<EnemyAI>().PerformAIAction();
                    battleInfoManager.UpdateBattleInfoText(BattleInfoTextType.TIPS, 0, enemy.name, enemy.name);
                    battleStatuses.Add(player.GetComponent<BattleStatus>());
                }
            }
        }
    }

    public void OnEndBattleButtonClicked()
    {
        // 点击结束按钮后的处理逻辑
        StartCoroutine(EndBattleCoroutine());
    }
    IEnumerator EndBattleCoroutine()
    {
        float delayInSeconds = 1.0f; // 可以根据需要设置延迟时间
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Stop();
        // 调用 Fungus 块
        if (flowchart != null)
        {
            if(player.GetComponent<BattleStatus>().getDead() == true)
            {
                flowchart.ExecuteBlock(loseBlockName);
            }else
            {
                flowchart.ExecuteBlock(winBlockName);
            }
        }
    }
    /*IEnumerator DestroySceneAfterDelay()
    {
        float delayInSeconds = 5.0f;
        yield return new WaitForSeconds(delayInSeconds);
        audioSource.Stop();
        Destroy(gameObject);
    }*/

    public void NextTurnCoroutine()
    {
        StartCoroutine(NextTurnDelay());
    }

    public IEnumerator NextTurnDelay()
    {
        float delayInSeconds = 2.0f;
        yield return new WaitForSeconds(delayInSeconds);
        NextTurn();
    }

    public GameObject GetPlayer()
    {
        return player;
    }


}
