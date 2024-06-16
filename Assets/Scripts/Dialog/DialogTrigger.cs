using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;

public class DialogTrigger : MonoBehaviour
{
    public TextMeshProUGUI DialogHintText; // 提示信息的文本对象
    public GameObject bubbleBackground; // 显示传送提示信息的背景
    public string hintMessage; // 提示信息内容
    private bool playerInRange; // 玩家是否在触发范围内
    public Flowchart flowchart; // Assign in the Inspector
    public string blockName; // Assign in the Inspector
    // Start is called before the first frame update
    void Start()
    {
        // 确保提示文本初始状态为隐藏
        if (DialogHintText != null)
        {
            DialogHintText.gameObject.SetActive(false);
        }
        // 确保提示文本背景初始状态为隐藏
        if (bubbleBackground != null)
        {
            bubbleBackground.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 检查玩家是否在触发区域内并按下E键
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            flowchart.ExecuteBlock(blockName);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发的对象是否是玩家
        if(other.CompareTag("Player"))
        {
            playerInRange = true;

            // 显示提示文本
            if (DialogHintText != null)
            {
                DialogHintText.gameObject.SetActive(true);
                DialogHintText.text = hintMessage;
            }
            // 显示提示文本背景
            if (bubbleBackground != null)
            {
                bubbleBackground.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 当玩家离开触发区域时，隐藏提示文本
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            //Debug.Log("TeleportTriggerTo: Player exited the trigger area");

            if (DialogHintText != null)
            {
                DialogHintText.gameObject.SetActive(false);
            }
            if (bubbleBackground != null)
            {
                bubbleBackground.SetActive(false);
            }
        }
    }
}
