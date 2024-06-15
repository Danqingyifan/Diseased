using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button inventoryButton;

    void Start()
    {
        inventoryButton.onClick.AddListener(OpenInventory);
    }

    void OpenInventory()
    {
        InventoryManager.instance.ShowInventory();
    }
}