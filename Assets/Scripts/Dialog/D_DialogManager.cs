using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine.UI;
using TMPro;

public class D_DialogManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<Sprite> portraits;

    private GameObject dialogCanvas;
    private D_Dialog currentDialog;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI currentDialogText;
    private Image currentPortraitImage;

    //public Animator dialogCanvasAnimator;

    private GameObject playerObject;
    private D_PlayerController playerController;
    void Start()
    {
        sentences = new Queue<string>();
        portraits = new Queue<Sprite>();

        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    public void SetDialogCanvas(GameObject dialogCanvasInstance)
    {
        dialogCanvas = dialogCanvasInstance;
        dialogCanvas.transform.Find("DialogCanvas/DialogBox/DialogPanel/ContinueButton").GetComponent<Button>().onClick.AddListener(DisplayNextSentence);
        //dialogCanvasAnimator = dialogCanvas.GetComponent<Animator>();
    }
    public void InitDialog(D_Dialog dialog)
    {
        currentDialog = dialog;

        nameText = dialogCanvas.transform.Find("DialogCanvas/DialogBox/NPC_Name").GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = currentDialog.npcName;
        }

        currentDialogText = dialogCanvas.transform.Find("DialogCanvas/DialogBox/DialogPanel/DialogText").GetComponent<TextMeshProUGUI>();
        if (currentDialogText != null)
        {
            currentDialogText.text = "";
        }

        currentPortraitImage = dialogCanvas.transform.Find("DialogCanvas/NPC_Portrait").GetComponent<Image>();

        StartDialog();
    }
    public void StartDialog()
    {   
        // lock player movement
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<D_PlayerController>();
            if(playerController != null)
            {
                playerController.LockMovement();
            }
        }

        dialogCanvas.SetActive(true);
        //dialogCanvasAnimator.SetBool("isTalking", true);

        sentences.Clear();
        portraits.Clear();

        foreach (string sentence in currentDialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (Sprite portrait in currentDialog.portraits)
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

        Sprite portrait = portraits.Dequeue();
        currentPortraitImage.sprite = portrait;

        string sentence = sentences.Dequeue();
        
        // Ensure that any currently running coroutine is stopped before starting a new one
        StopAllCoroutines();  
        
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        currentDialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            currentDialogText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialog()
    {   
        // unlock player movement
        if (playerController != null)
        {
           playerController.UnlockMovement();
        }

        //dialogCanvasAnimator.SetBool("isTalking", false);
        dialogCanvas.SetActive(false);
        Destroy(dialogCanvas);
    }

}
