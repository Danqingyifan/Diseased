using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine.UI;

public class D_DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<Sprite> portraits;

    public Text nameText;
    public Text currentDialogText;
    public GameObject dialogBox;

    void Start()
    {
        sentences = new Queue<string>();
        portraits = new Queue<Sprite>();
    }

    public void StartDialog(D_Dialog dialog)
    {
        dialogBox.SetActive(true);
        nameText.text = dialog.npcName;

        sentences.Clear();
        portraits.Clear();

        foreach (string sentence in dialog.lines)
        {
            sentences.Enqueue(sentence);
        }

        foreach (Sprite portrait in dialog.portraits)
        {
            portraits.Enqueue(portrait);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        Sprite portrait = portraits.Dequeue();

        StopAllCoroutines();  // Ensure that any currently running coroutine is stopped before starting a new one
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        currentDialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            currentDialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog()
    {
        dialogBox.SetActive(false);
        Debug.Log("End of conversation");
    }
}
