using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fungus;
public class EventTrigger1 : MonoBehaviour
{
    private bool playerInRange; // 玩家是否在触发范围内
    public Flowchart flowchart; // Assign in the Inspector
    public string blockName; // Assign in the Inspector
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 检查玩家是否在触发区域内
        if (playerInRange)
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;

        }
    }
}
