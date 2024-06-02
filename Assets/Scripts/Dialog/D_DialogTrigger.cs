using UnityEngine;

public class D_DialogTrigger : MonoBehaviour
{
    public D_Dialog dialog;

    // Method to trigger the dialogue
    public void TriggerDialogue()
    {
        // Find the DialogueManager in the scene and start the dialogue
        D_DialogManager dialogueManager = FindObjectOfType<D_DialogManager>();
        if (dialogueManager != null)
        {
            dialogueManager.StartDialog(dialog);
        }
        else
        {
            Debug.LogWarning("DialogueManager not found in the scene.");
        }
    }

}
