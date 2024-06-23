using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void EndingGame()
    {
#if UNITY_EDITOR
        // 在编辑器中停止播放模式
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在构建的游戏中退出应用程序
        Application.Quit();
#endif
    }
}

