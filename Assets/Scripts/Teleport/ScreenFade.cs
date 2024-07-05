using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage; // 引用全屏遮罩图像
    public float fadeDuration = 1f; // 淡入淡出持续时间

    private void Start()
    {
        // 确保遮罩一开始是透明的
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = color;
            yield return null;
        }
    }
    public IEnumerator FadeIn1(float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }
        // 确保最终的透明度为完全不透明
        color.a = 1;
        fadeImage.color = color;
    }
    // 添加一个公共函数来启动淡入过程
    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn1(duration));
    }
}