using UnityEngine;
using System.Collections.Generic;

public class IconManager : MonoBehaviour
{
    public static IconManager instance;  // 单例实例

    private void Awake()
    {
        // 确保只有一个实例存在
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 打开特定图标的界面
    public void OpenIconPanel(string iconName)
    {
        switch (iconName)
        {
            case "Inventory":
                InventoryManager.instance.ShowInventory();
                break;
            // 这里可以添加其他图标的处理逻辑
        }
    }
}