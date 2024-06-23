using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class D_CameraController : MonoBehaviour
{
    private static D_CameraController instance;
    private CinemachineVirtualCamera virtualCam;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Ensure Cinemachine Virtual Camera follows this player
        SetupCinemachineVirtualCamera();
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnEnable()
    {
        // Ensure Cinemachine Virtual Camera follows this player
        SetupCinemachineVirtualCamera();
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameObject.SetActive(true); // 确保 GameObject 活跃
        StartCoroutine(SetupCinemachineVirtualCameraWithDelay());
    }

    private IEnumerator SetupCinemachineVirtualCameraWithDelay()
    {
        yield return new WaitForEndOfFrame();  // 或者使用更长的延迟，如 yield return new WaitForSeconds(0.1f);
        SetupCinemachineVirtualCamera();
    }

    private void SetupCinemachineVirtualCamera()
    {
        // Find the Cinemachine Virtual Camera in the scene
        GameObject virtualCamObject = GameObject.Find("PlayerFollowingVC");
        if (virtualCamObject != null)
        {
            CinemachineVirtualCamera virtualCam = virtualCamObject.GetComponent<CinemachineVirtualCamera>();
            if (virtualCam != null)
            {
                virtualCam.Follow = transform;
            }
            else
            {
                Debug.LogWarning("CinemachineVirtualCamera component not found on PlayerFollowingVC.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerFollowingVC not found in the scene.");
        }
    }
}
