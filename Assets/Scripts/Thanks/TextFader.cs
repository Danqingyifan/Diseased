using System.Collections;
using UnityEngine;
using TMPro;

public class TextFader : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 引用TextMeshPro组件
    public float fadeDuration = 1f; // 淡入淡出持续时间

    private void Start()
    {
        // 确保文本一开始是透明的
        textMeshPro.color = new Color(textMeshPro.color.r, textMeshPro.color.g, textMeshPro.color.b, 0);
    }

    public IEnumerator FadeIn(float duration)
    {
        float elapsedTime = 0f;
        Color color = textMeshPro.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            textMeshPro.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        float elapsedTime = 0f;
        Color color = textMeshPro.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / duration));
            textMeshPro.color = color;
            yield return null;
        }
    }

    // 添加一个公共函数来启动淡入过程
    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    // 添加一个公共函数来启动淡出过程
    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }
}