using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public GameObject contextMenu; // 右键菜单对象
    public Button contextButton;   // 菜单按钮
    public TextMeshProUGUI contextButtonText; // 菜单按钮文本
    private Items item;            // 当前道具
    private bool isBattle = false;         // 是否处于战斗状态

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

    // 设置当前道具
    public void SetItem(Items newItem)
    {
        item = newItem;
        if (item != null)
        {
            // 设置右键菜单按钮文本
            if (item.itemType == ItemType.Equipment)
            {
                contextButtonText.text = "装备";
            }
            if(item.itemType == ItemType.Consumable && !isBattle)
            {
                contextButtonText.text = "使用";
            }
            if(item.itemType == ItemType.BattleConsumable && isBattle)
            {
                contextButtonText.text = "使用";
            }
        }
    }
    // 设置战斗状态
    public void SetBattleState(bool battleState)
    {
        isBattle = battleState;
    }
    
    // 获取当前道具
    public Items GetItem()
    {
        return item;
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
        if (contextMenu != null && item != null)
        {
            if (item.itemType != ItemType.Quest)
            {
                if (item.itemType == ItemType.Consumable && !isBattle ||
                    item.itemType == ItemType.BattleConsumable && isBattle ||
                    item.itemType == ItemType.Equipment)
                {
                    // 切换右键菜单的显示状态
                    contextMenu.SetActive(!contextMenu.activeSelf);
                }
            }
            
        }
    }

    // 右键菜单按钮点击事件
    void OnContextButtonClick()
    {
        if (item != null)
        {
            if (item.itemType == ItemType.Equipment)
            {
                // 装备道具，并传递当前 ItemSlot 的 Transform
                InventoryMenu.instance.EquipItem(item, transform);
            }
            else
            {
                // 使用道具
                InventoryMenu.instance.UseItem(item);
            }
            // 隐藏右键菜单
            contextMenu.SetActive(false);
        }
    }
}