using UnityEngine;

public class D_DialogTrigger : MonoBehaviour
{
    public D_Dialog dialog;

    public GameObject dialogCanvasPrefab;

    public Camera mainCamera;

    private GameObject dialogueCanvasInstance;
    // Method to trigger the dialog
    private string virtualCamera = "PlayerFollowingVC";
    public void TriggerDialog()
    {
        // Check if mainCamera is still valid
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found.");
                return;
            }
        }

        // Find the dialogManager in the scene and start the dialog
        D_DialogManager dialogManager = FindObjectOfType<D_DialogManager>();

        GameObject virtualCameraObject = GameObject.Find(virtualCamera);
        if (dialogManager != null)
        {
            if (dialogueCanvasInstance == null)
            {
                dialogueCanvasInstance = Instantiate(dialogCanvasPrefab, virtualCameraObject.transform);
            }

            Canvas canvas = dialogueCanvasInstance.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.WorldSpace;
            }

            dialogueCanvasInstance.transform.position = mainCamera.transform.position + mainCamera.transform.forward * 3;
            float xOffset = 0.1f;
            float yOffset = -2.3f;
            dialogueCanvasInstance.transform.position = new Vector3
                (
                dialogueCanvasInstance.transform.position.x + xOffset,
                dialogueCanvasInstance.transform.position.y + yOffset,
                dialogueCanvasInstance.transform.position.z
                );

            dialogManager.SetDialogCanvas(dialogueCanvasInstance);
            dialogManager.InitDialog(dialog);
        }
        else
        {
            Debug.LogWarning("Dialog Manager not found in the scene.");
            return;
        }
    }
}
