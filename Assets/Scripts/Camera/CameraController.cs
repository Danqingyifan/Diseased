using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    // 更改 Follow 属性的方法
    public void SetCameraFollow(Transform newFollowTarget)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTarget;
        }
    }
}