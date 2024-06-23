using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // 单例实例

    public GameObject inventoryCanvas; // 背包Canvas实例
    public AudioClip closeInventorySound; // 关闭背包音效
    private AudioSource audioSource; // 音频源

    void Awake()
    {
        // 确保只有一个实例存在
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        // 获取或添加 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 显示背包Canvas
    public void ShowInventory()
    {
        //Debug.Log("ShowInventory called");
        if (inventoryCanvas != null)
        {
            //Debug.Log("Using existing inventory canvas instance");
            Canvas canvas = inventoryCanvas.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingLayerName = "Inventory";
            }
        }
    }

    // 隐藏背包Canvas
    public void HideInventory()
    {
        if (inventoryCanvas != null)
        {
            Canvas canvas = inventoryCanvas.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingLayerName = "Default"; // 恢复默认的sorting layer
            }
            PlaySound(closeInventorySound); // 播放关闭背包音效
        }
    }

    // 播放音效
    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}