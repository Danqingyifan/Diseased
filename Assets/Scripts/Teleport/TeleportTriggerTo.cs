using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Fungus;

public class TeleportTriggerTo : MonoBehaviour
{
    [Tooltip("Enter the scene name")]
    public string targetCanvas; // 目标场景Canvas
    [Tooltip("Assign a unique ID to this teleport")]
    public string teleportName; // 传送点的唯一ID
    
    [Tooltip("Enter the duration of transition to the new scene in seconds")]
    public float transitionTime = 1f; // 场景切换的持续时间
    private bool openScene; // 是否正在切换场景

    public TextMeshProUGUI teleportHintText; // 提示信息的文本对象
    public GameObject bubbleBackground; // 显示传送提示信息的背景
    public string hintMessage; // 提示信息内容
    private bool playerInRange; // 玩家是否在触发范围内
    public Flowchart flowchart;
    public GameObject TeleportFrom;//传送到的位置
    public AudioClip teleportSound;          // 音效文件
    private AudioSource audioSource;         // AudioSource 组件

    private ScreenFade screenFade; // ScreenFade组件的引用

    void Start()
    {
        // 确保提示文本初始状态为隐藏
        if (teleportHintText != null)
        {
            teleportHintText.gameObject.SetActive(false);
        }
        // 确保提示文本背景初始状态为隐藏
        if (bubbleBackground != null)
        {
            bubbleBackground.SetActive(false);
        }
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 如果 audioSource 组件不存在，则添加一个
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 获取ScreenFade组件
        screenFade = FindObjectOfType<ScreenFade>();
    }
    
    void Update()
    {
        // 检查玩家是否在触发区域内并按下J键
        if (playerInRange && Input.GetKeyDown(KeyCode.J))
        {
            openScene = true;
            D_PlayerController.instance.areaTransitionName = teleportName;
            //Debug.Log($"TeleportTriggerTo Update: areaTransitionName set to {teleportName}");

            StartCoroutine(TeleportWithFade());
        }

        // 场景切换的计时处理
        if(openScene)
        {
            transitionTime -= Time.deltaTime;
            if(transitionTime <= 0)
            {
                openScene = false;
            }
        }
    }

    private IEnumerator TeleportWithFade()
    {

        // 开始淡入效果
        yield return StartCoroutine(screenFade.FadeIn());
        // 执行传送操作
        D_PlayerController.instance.transform.position = TeleportFrom.transform.position;

        // 播放音效
        if (teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        // 短暂等待，确保传送位置已更新
        yield return new WaitForSeconds(0.50f);

        // 开始淡出效果
        yield return StartCoroutine(screenFade.FadeOut());

        openScene = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发的对象是否是玩家
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            //Debug.Log("TeleportTriggerTo: Player entered the trigger area");

            // 显示提示文本
            if (teleportHintText != null)
            {
                teleportHintText.gameObject.SetActive(true);
                teleportHintText.text = hintMessage;
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

            if (teleportHintText != null)
            {
                teleportHintText.gameObject.SetActive(false);
            }
            if (bubbleBackground != null)
            {
                bubbleBackground.SetActive(false);
            }
        }
    }
}
