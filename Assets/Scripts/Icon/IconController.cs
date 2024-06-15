using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class IconController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI tooltipText; // 提示文本
    public string iconName; // 图标的名称

    void Start()
    {
        // 确保提示文本初始状态为隐藏
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标移动到图标后才显示提示文本
        if (tooltipText != null)
        {
            tooltipText.gameObject.SetActive(true);
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
        // 通知图标管理器打开对应的界面
        IconManager.instance.OpenIconPanel(iconName);
    }
}