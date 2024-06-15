using UnityEngine;

// 枚举定义不同的道具类型
public enum ItemType
{
    Equipment,     // 装备类道具
    Consumable,    // 消耗性道具
    NonConsumable  // 非消耗性道具
}

// 定义道具类
[System.Serializable]
public class Items
{
    public string itemName;          // 道具名称
    public string itemDescription;   // 道具描述
    public Sprite itemIcon;          // 道具图标
    public int quantity;             // 道具数量
    public ItemType itemType;        // 道具类型
    public ItemEffect itemEffect;   

    public string GetItemTypeDescription()
    {
        switch (itemType)
        {
            case ItemType.Consumable:
                return "<消耗型道具>";
            case ItemType.Equipment:
                return "<装备型道具>";
            default:
                return "<非消耗型道具>";
        }
    }
}

[System.Serializable]
public class ItemEffect
{
    public int healthEffect;    // 生命值效果
    public int staminaEffect;   // 活力值效果
    public int attackEffect;    // 攻击力效果
    public int defenseEffect;   // 防御力效果
    public ItemEffect(int health, int stamina, int attack, int defense)
    {
        healthEffect = health;
        staminaEffect = stamina;
        attackEffect = attack;
        defenseEffect = defense;
    }
}