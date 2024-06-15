using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenu : MonoBehaviour
{
    public static InventoryMenu instance;    // 单例
    public Button closeButton;               // 关闭背包按钮
    public Transform equipmentSlot;          // 装备栏格子
    public Transform itemContainer;          // 存放道具的容器
    public TextMeshProUGUI itemDescription;  // 显示道具描述的文本
    public CharacterStats characterStats;    // 人物属性

    private Items selectedItem;              // 当前选中的道具

    void Awake()
    {
        // 设置单例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        closeButton.onClick.AddListener(CloseInventory);  // 为关闭按钮添加点击事件
        AddItemToInventory("拳套", 1);                     // 添加测试道具
        AddItemToInventory("对乙酰氨基酚", 1);
        AddItemToInventory("创口贴", 5);
        AddItemToInventory("粉笔", 5);
        AddItemToInventory("魏雪宁的眼镜", 1);
        AddItemToInventory("布洛芬", 3);
        AddItemToInventory("面粉袋", 2);
        AddItemToInventory("许娇的怀表", 1);
        AddItemToInventory("铁丝", 1);
        AddItemToInventory("煤油", 1);
    }

    // 关闭背包界面
    void CloseInventory()
    {
        InventoryManager.instance.HideInventory();
    }

    // 将道具添加到背包
    public void AddItemToInventory(string itemName, int quantity)
    {
        Items item = ItemDatabase.instance.GetItemByName(itemName);  // 从数据库获取道具
        if (item != null)
        {
            item.quantity = quantity;  // 设置道具数量
            foreach (Transform slot in itemContainer)
            {
                Image itemImage = slot.Find("ItemImage").GetComponent<Image>();
                TextMeshProUGUI quantityText = slot.Find("QuantityText").GetComponent<TextMeshProUGUI>();

                if (itemImage.sprite == null) // 找到一个空的道具格子
                {
                    itemImage.sprite = item.itemIcon;  // 设置道具图标
                    quantityText.text = item.quantity.ToString();  // 显示道具数量
                    itemImage.color = new Color(1f, 1f, 1f, 1f);// 设置ItemImage的不透明度
                    Button itemButton = slot.GetComponent<Button>();
                    itemButton.onClick.RemoveAllListeners();
                    itemButton.onClick.AddListener(() => OnItemClick(item));  // 为道具添加左键点击事件

                    // 获取 ItemSlot 组件并设置道具
                    ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
                    if (itemSlot != null)
                    {
                        itemSlot.SetItem(item);
                    }

                    break;
                }
            }
        }
    }

    // 左键点击道具事件
    void OnItemClick(Items item)
    {
        selectedItem = item;
        string itemTypeDescription = item.GetItemTypeDescription();
        itemDescription.text = $"【{item.itemName}】{itemTypeDescription}\n{item.itemDescription}\n数量: {item.quantity}";
    }

    // 装备道具
    public void EquipItem(Items item, Transform itemSlot)
    {
        if (equipmentSlot != null)
        {
            Transform equipmentImageTransform = equipmentSlot.Find("EquipmentImage");
            if (equipmentImageTransform != null)
            {
                Image equipmentImage = equipmentImageTransform.GetComponent<Image>();
                if (equipmentImage != null)
                {
                    // 检查装备类型并应用相应属性增益
                    ApplyEquipmentEffect(item);
                    equipmentImage.sprite = item.itemIcon;  // 设置装备栏图标
                    equipmentImage.color = new Color(1f, 1f, 1f, 1f);// 设置equipmentImage的不透明度
                    string itemTypeDescription = item.GetItemTypeDescription();
                    itemDescription.text = $"【{item.itemName}】{itemTypeDescription}\n{item.itemDescription}\n数量: {item.quantity}";  // 更新描述
                    DestroyItemFromSlot(itemSlot);  // 从原来的道具栏销毁该装备

                    // 更新装备槽中的道具
                    EquipmentSlot eqSlot = equipmentSlot.GetComponent<EquipmentSlot>();
                    if (eqSlot != null)
                    {
                        eqSlot.SetEquippedItem(item);
                    }

                    // 清除原本道具格子中的道具引用
                    ClearSlot(itemSlot);
                }
            }
        }
    }

    // 解除装备道具
    public void UnequipItem(Items item, Transform eqSlot)
    {
        if (eqSlot != null)
        {
            // 移除装备图标
            Transform equipmentImageTransform = eqSlot.Find("EquipmentImage");
            if (equipmentImageTransform != null)
            {
                Image equipmentImage = equipmentImageTransform.GetComponent<Image>();
                if (equipmentImage != null)
                {
                    // 检查装备类型并移除相应属性增益
                    RemoveEquipmentEffect(item);

                    equipmentImage.sprite = null;
                    equipmentImage.color = new Color(0f, 0f, 0f, 0f);// 设置equipmentImage的不透明度
                }
            }

            // 将道具重新添加到背包
            AddItemToInventory(item.itemName, 1);

            // 清除装备槽中的道具
            EquipmentSlot eqSlotComponent = eqSlot.GetComponent<EquipmentSlot>();
            if (eqSlotComponent != null)
            {
                eqSlotComponent.SetEquippedItem(null);
            }

            string itemTypeDescription = item.GetItemTypeDescription();
            itemDescription.text = $"【{item.itemName}】{itemTypeDescription}\n{item.itemDescription}\n数量: {item.quantity}";  // 更新描述
        }
    }

    // 使用道具
    public void UseItem(Items item)
    {
        if (item.itemType == ItemType.Consumable)
        {
            // 应用道具效果
            ApplyItemEffect(item);
            item.quantity--;  // 消耗一个道具
            if (item.quantity <= 0)
            {
                DestroyItem(item);  // 道具数量为0时销毁道具
            }
            else
            {
                // 更新道具数量显示
                UpdateItemQuantityDisplay(item);
                string itemTypeDescription = item.GetItemTypeDescription();
                itemDescription.text = $"【{item.itemName}】{itemTypeDescription}\n{item.itemDescription}\n数量: {item.quantity}";
            }
        }
        // 如果是非消耗性道具，添加相应的使用逻辑
    }

    // 应用道具效果
    void ApplyItemEffect(Items item)
    {
        if (characterStats != null && item.itemEffect != null)
        {
            characterStats.ModifyHealth(item.itemEffect.healthEffect);
            characterStats.ModifyStamina(item.itemEffect.staminaEffect);
            characterStats.ModifyAttack(item.itemEffect.attackEffect);
            characterStats.ModifyDefense(item.itemEffect.defenseEffect);
            // 更新属性显示
            characterStats.UpdateStatDisplays();
        }
    }

    // 应用装备效果
    void ApplyEquipmentEffect(Items item)
    {
        if (characterStats != null && item.itemEffect != null)
        {
            characterStats.ModifyHealth(item.itemEffect.healthEffect);
            characterStats.ModifyStamina(item.itemEffect.staminaEffect);
            characterStats.ModifyAttack(item.itemEffect.attackEffect);
            characterStats.ModifyDefense(item.itemEffect.defenseEffect);
            // 更新属性显示
            characterStats.UpdateStatDisplays();
        }
    }
    // 移除装备效果
    void RemoveEquipmentEffect(Items item)
    {
        if (characterStats != null && item.itemEffect != null)
        {
            characterStats.ModifyHealth(-item.itemEffect.healthEffect);
            characterStats.ModifyStamina(-item.itemEffect.staminaEffect);
            characterStats.ModifyAttack(-item.itemEffect.attackEffect);
            characterStats.ModifyDefense(-item.itemEffect.defenseEffect);
            // 更新属性显示
            characterStats.UpdateStatDisplays();
        }
    }

    // 更新道具数量显示
    void UpdateItemQuantityDisplay(Items item)
    {
        foreach (Transform slot in itemContainer)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot != null && itemSlot.GetItem() == item)
            {
                TextMeshProUGUI quantityText = slot.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                if (quantityText != null)
                {
                    quantityText.text = item.quantity.ToString();
                }
                break;
            }
        }
    }

    // 销毁道具
    void DestroyItem(Items item)
    {
        foreach (Transform slot in itemContainer)
        {
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            if (itemSlot != null && itemSlot.GetItem() == item)
            {
                ClearSlot(slot);
                break;
            }
        }
    }

    // 从指定格子中销毁道具
    void DestroyItemFromSlot(Transform slot)
    {
        ClearSlot(slot);
    }

    // 清空道具格子
    void ClearSlot(Transform slot)
    {
        Image itemImage = slot.Find("ItemImage").GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.color = new Color(0f, 0f, 0f, 0f);// 设置ItemImage的不透明度
        }

        TextMeshProUGUI quantityText = slot.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        if (quantityText != null)
        {
            quantityText.text = "";  // 清空道具数量
        }
        itemDescription.text = "";

        // 清除原本道具格子中的道具引用
        ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
        if (itemSlot != null)
        {
            itemSlot.SetItem(null);
        }

        // 移除所有点击事件监听器
        Button itemButton = slot.GetComponent<Button>();
        if (itemButton != null)
        {
            itemButton.onClick.RemoveAllListeners();
        }
    }
}
