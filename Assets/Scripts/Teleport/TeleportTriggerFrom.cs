using UnityEngine;
using UnityEngine.Events;

public class TeleportTriggerFrom : MonoBehaviour
{
    [Tooltip("Assign a unique ID to this teleport")]
    public string teleportName; // 传送点的唯一ID

    public UnityEvent onSpawn; // 当玩家生成时调用的事件

    void Start()
    {
        string storedTeleportName = D_PlayerController.instance.areaTransitionName;
        //Debug.Log($"TeleportTriggerFrom Start: Stored Teleport Name is {storedTeleportName}");
        //Debug.Log($"TeleportTriggerFrom Start: Teleport Name Name is {teleportName}");

        // 检查传送点ID是否与存储的传送点ID匹配
        if (teleportName == storedTeleportName)
        {
            //Debug.Log($"TeleportTriggerFrom Start: Matching teleportName found: {teleportName}");

            // 将玩家位置设置为当前传送点位置
            D_PlayerController.instance.transform.position = transform.position;
            //Debug.Log($"TeleportTriggerFrom Start: Player position set to {transform.position}");

            // 将玩家的渲染层设置为“Player”
            D_PlayerController.instance.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
        }
        else
        {
            //Debug.Log($"TeleportTriggerFrom Start: No matching teleportName. Current: {teleportName}, Stored: {storedTeleportName}");
        }

        // 触发onSpawn事件
        onSpawn?.Invoke();
    }
}