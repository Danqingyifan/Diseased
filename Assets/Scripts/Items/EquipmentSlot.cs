using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject contextMenu; // 右键菜单对象
    public Button contextButton;   // 菜单按钮
    public TextMeshProUGUI contextButtonText; // 菜单按钮文本
    private Items equippedItem;    // 当前装备的道具

    void Start()
    {
        // 初始化时隐藏右键菜单
        if (contextMenu != null)
        {
            contextMenu.SetActive(false);
        }

        // 为按钮添加点击事件
        if (contextButton != null)
        {
            contextButton.onClick.AddListener(OnContextButtonClick);
        }
    }

    // 设置当前装备的道具
    public void SetEquippedItem(Items newItem)
    {
        equippedItem = newItem;
        if (equippedItem != null)
        {
            // 设置右键菜单按钮文本
            contextButtonText.text = "解除";
        }
    }

    // 获取当前装备的道具
    public Items GetEquippedItem()
    {
        return equippedItem;
    }

    // 实现 IPointerClickHandler 接口方法
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ShowContextMenu();
        }
    }

    // 显示右键菜单
    void ShowContextMenu()
    {
        if (contextMenu != null && equippedItem != null)
        {
            // 切换右键菜单的显示状态
            contextMenu.SetActive(!contextMenu.activeSelf);
        }
    }

    // 右键菜单按钮点击事件
    void OnContextButtonClick()
    {
        if (equippedItem != null)
        {
            // 解除装备道具，并传递当前 EquipmentSlot 的 Transform
            InventoryMenu.instance.UnequipItem(equippedItem, transform);
            // 隐藏右键菜单
            contextMenu.SetActive(false);
        }
    }
}