using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class IconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI tooltipText; // 提示文本
    public string iconName; // 图标的名称
    public AudioClip hoverSound; // 鼠标悬停音效
    public AudioClip clickSound; // 鼠标点击音效
    private AudioSource audioSource; // 音频源

    void Start()
    {
        // 确保提示文本初始状态为隐藏
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(false);
        }

        // 获取或添加 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 显示提示文本并播放悬停音效
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(true);
        }
        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 隐藏提示文本
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 播放点击音效并通知图标管理器打开对应的界面
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        IconManager.instance.OpenIconPanel(iconName);
    }
}