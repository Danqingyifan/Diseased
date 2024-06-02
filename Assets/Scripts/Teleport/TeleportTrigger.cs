using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] private string targetSceneName; // 目标场景名称
    [SerializeField] private Text teleportHintText; // 参考提示文本
    [SerializeField] private string hintMessage;// 提示信息(在Inspector中设置)
    private bool playerInRange;

    void Start()
    {
        // 确保提示文本初始状态为隐藏
        if (teleportHintText != null)
        {
            teleportHintText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (teleportHintText != null)
            {
                teleportHintText.gameObject.SetActive(true); // 显示提示文本
                teleportHintText.text = hintMessage; // 使用Inspector中设置的提示信息
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 当玩家离开触发区域时，设置playerInRange为false
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (teleportHintText != null)
            {
                teleportHintText.gameObject.SetActive(false); // 隐藏提示文本
            }
        }
    }

    void Update()
    {
        // 检查玩家是否在触发区域内并按下F键
        if (playerInRange && Input.GetKeyDown(KeyCode.J))
        {
            // 跳转到其他场景
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName); // 使用Inspector中设置的场景名称
            }
            else
            {
                Debug.LogError("Target scene name is not set!");
            }
        }
    }
}