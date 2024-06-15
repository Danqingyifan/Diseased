using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContextMenu : MonoBehaviour
{
    public GameObject contextMenuPrefab;  // 右键菜单的预制体
    private GameObject contextMenuInstance;  // 当前右键菜单的实例
    public UnityEvent onRightClick;  // 右键点击事件

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            onRightClick.Invoke();  // 触发右键点击事件
        }
    }

    // 显示右键菜单
    public void Show()
    {
        if (contextMenuPrefab != null && contextMenuInstance == null)
        {
            contextMenuInstance = Instantiate(contextMenuPrefab, transform);  // 实例化右键菜单预制体
        }
    }

    // 隐藏右键菜单
    public void Hide()
    {
        if (contextMenuInstance != null)
        {
            Destroy(contextMenuInstance);  // 销毁右键菜单实例
        }
    }

    // 清除右键菜单选项
    public void ClearOptions()
    {
        if (contextMenuInstance != null)
        {
            foreach (Transform child in contextMenuInstance.transform)
            {
                Destroy(child.gameObject);  // 销毁每个选项
            }
        }
    }

    // 添加右键菜单选项
    public void AddOption(string optionName, UnityAction action)
    {
        if (contextMenuInstance != null)
        {
            GameObject newOption = new GameObject(optionName);  // 创建新的选项
            Button button = newOption.AddComponent<Button>();  // 添加按钮组件
            button.onClick.AddListener(action);  // 为按钮添加点击事件
            button.GetComponentInChildren<Text>().text = optionName;  // 设置选项名称
            newOption.transform.SetParent(contextMenuInstance.transform);  // 将选项添加到菜单中
        }
    }
}