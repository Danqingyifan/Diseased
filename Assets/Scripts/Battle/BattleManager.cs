using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    public AudioSource audioSource;
    private List<BattleStatus> battleStatuses;

    private GameObject player;
    private GameObject enemy;
    [SerializeField]
    public GameObject ActionMenu;
    public GameObject EndBattleButton; // 结束战斗按钮
    public Flowchart flowchart; // 引用 Fungus Flowchart
    public string blockName = "战斗结束"; // 要调用的块的名称


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player_Battle");
        enemy = GameObject.FindGameObjectWithTag("Enemy_Battle");

        battleStatuses = new List<BattleStatus>();
        EndBattleButton.SetActive(false); // 初始化时隐藏结束按钮
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
                    BattleInfoManager.instance.UpdateBattleInfoText(BattleInfoTextType.TIPS, 0, player.name, player.name);
                    battleStatuses.Add(enemy.GetComponent<BattleStatus>());
                }
            }
            else if (currentUnit.tag == "Enemy_Battle")
            {
                if (!player.GetComponent<BattleStatus>().getDead())
                {
                    currentUnit.GetComponent<EnemyAI>().PerformAIAction();
                    BattleInfoManager.instance.UpdateBattleInfoText(BattleInfoTextType.TIPS, 0, enemy.name, enemy.name);
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
            flowchart.ExecuteBlock(blockName);
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
}
