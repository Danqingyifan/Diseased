using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 结束游戏并返回开始菜单
    public void EndGame()
    {
        // 加载开始菜单场景
        SceneManager.LoadScene("GamebeginMenu");
    }

    // 场景加载完成后调用的回调函数
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 销毁特定标签的对象
        DestroyTaggedObjects();
        // 确保只有一个AudioListener
        EnsureSingleAudioListener();
    }

    // 销毁带有特定标签的对象
    private void DestroyTaggedObjects()
    {
        // 标签列表
        string[] tags = { "Player", "FungusManager", "MainCamera" };

        foreach (string tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                Debug.Log($"Destroying object with tag {tag}: {obj.name}"); // 添加调试日志
                Destroy(obj, 0.1f); // 延迟0.1秒销毁
            }
        }
    }

    // 确保场景中只有一个AudioListener
    private void EnsureSingleAudioListener()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
            {
                Destroy(listeners[i]);
            }
        }
    }
}