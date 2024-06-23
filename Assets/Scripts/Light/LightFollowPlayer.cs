using UnityEngine;

public class LightFollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // 玩家 Transform 的引用

    private void Update()
    {
        if (playerTransform != null)
        {
            // 将光源的位置设置为玩家的位置
            transform.position = playerTransform.position;
        }
    }

    // 设置玩家 Transform 的方法
    public void SetPlayerTransform(Transform newPlayerTransform)
    {
        playerTransform = newPlayerTransform;
    }
}