using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle2D : MonoBehaviour
{
    public Light2D globalLight;
    public Color dayColor = new Color(1f, 0.95f, 0.8f);
    public Color nightColor = new Color(0.2f, 0.2f, 0.5f);
    public float dayDuration = 30.0f; // 每昼夜周期的持续时间（秒）

    private float time;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        time += Time.deltaTime;
        float t = Mathf.PingPong(time / dayDuration, 1.0f);

        // 控制光的颜色和强度
        globalLight.intensity = Mathf.Lerp(0.2f, 1.0f, t);
        globalLight.color = Color.Lerp(nightColor, dayColor, t);

        // 控制背景颜色
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = Color.Lerp(nightColor, dayColor, t);
        }
    }
}