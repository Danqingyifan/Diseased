// ItemDatabase.cs
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;  // 单例实例

    public Items[] items;  // 所有道具的数组

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);  // 防止在场景切换时销毁此对象
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // 如果已有实例，销毁新创建的对象
        }
    }

    // 根据道具名称获取道具
    public Items GetItemByName(string itemName)
    {
        foreach (Items item in items)
        {
            if (item.itemName == itemName)
            {
                return item;
            }
        }
        return null;  // 未找到道具
    }
}