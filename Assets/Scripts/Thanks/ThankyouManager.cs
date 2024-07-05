using System.Collections;
using UnityEngine;
using TMPro;

public class ThankYouManager : MonoBehaviour
{
    public TextFader textFader; // 引用 TextMeshProFader 组件
    public string[] thankYouMessages; // 致谢语句数组
    public float displayDuration = 2f; // 每条消息显示的持续时间

    private void Start()
    {
        StartCoroutine(DisplayThankYouMessages());
    }

    private IEnumerator DisplayThankYouMessages()
    {
        foreach (string message in thankYouMessages)
        {
            textFader.textMeshPro.text = message; // 设置文本内容
            textFader.StartFadeIn(textFader.fadeDuration); // 开始淡入
            yield return new WaitForSeconds(displayDuration + textFader.fadeDuration); // 等待显示时间和淡入时间
            textFader.StartFadeOut(textFader.fadeDuration); // 开始淡出
            yield return new WaitForSeconds(textFader.fadeDuration); // 等待淡出时间
        }
    }
}