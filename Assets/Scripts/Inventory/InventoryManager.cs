using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // 单例实例

    public GameObject inventoryCanvasPrefab; // 背包Canvas的预制体

    private GameObject inventoryCanvasInstance; // 当前生成的背包Canvas实例

    void Awake()
    {
        // 确保只有一个实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    // 生成背包Canvas
    public void ShowInventory()
    {
        if (inventoryCanvasPrefab != null && inventoryCanvasInstance == null)
        {
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
        }
    }

    // 销毁背包Canvas
    public void HideInventory()
    {
        if (inventoryCanvasInstance != null)
        {
            Destroy(inventoryCanvasInstance);
        }
    }
}