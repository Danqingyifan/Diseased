using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public List<QuestData> quests = new List<QuestData>();

    public TextMeshProUGUI questDisplay;
    public TextMeshProUGUI timeDisplay;

    private int currentQuestIndex = 0;
    private List<GameObject> spawnedCharacters = new List<GameObject>();

    void Start()
    {
        LoadGame();
        if (quests.Count > 0)
        {
            UpdateQuestUI();
        }
        else
        {
            Debug.LogWarning("No quests assigned in the QuestManager.");
        }
    }

    public void NextQuest()
    {
        if (currentQuestIndex < quests.Count - 1)
        {
            currentQuestIndex++;
            UpdateQuestUI();
            SaveGame();
        }
        else
        {
            Debug.Log("No more quests.");
        }
    }

    private void UpdateQuestUI()
    {
        QuestData currentQuest = quests[currentQuestIndex];
        questDisplay.text = $"Quest: {currentQuest.questDescription}";
        timeDisplay.text = $"Time: {currentQuest.timeDescription}";

        UpdateCharacters(currentQuest.characterPrefabs, currentQuest.spawnPositions);
        InitializeDialogTriggers(currentQuest.dialogTriggers);

        // You can call another component to handle dialog if needed
        // dialogManager.SetDialog(currentQuest.dialog);
    }

    private void UpdateCharacters(GameObject[] characterPrefabs, Vector3[] spawnPositions)
    {
        foreach (GameObject character in spawnedCharacters)
        {
            Destroy(character);
        }
        spawnedCharacters.Clear();

        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            Vector3 spawnPosition = (spawnPositions != null && spawnPositions.Length > i) ? spawnPositions[i] : Vector3.zero;
            GameObject character = Instantiate(characterPrefabs[i], spawnPosition, Quaternion.identity);
            spawnedCharacters.Add(character);

            // Optionally initialize character behavior here
            // character.GetComponent<CharacterBehavior>().Initialize();
        }
    }

    private void InitializeDialogTriggers(D_DialogTrigger[] dialogTriggers)
    {
        foreach (D_DialogTrigger dialogTrigger in dialogTriggers)
        {
            if (dialogTrigger != null)
            {
                // Initialize dialog trigger as needed
                dialogTrigger.mainCamera = Camera.main; // Ensure mainCamera is assigned
                // Other initialization if needed
            }
        }
    }

    private void SaveGame()
    {
        QuestData currentQuest = quests[currentQuestIndex];
        SaveManager.SaveGame(currentQuestIndex, currentQuest.timeDescription);
    }

    private void LoadGame()
    {
        SaveData saveData = SaveManager.LoadGame();
        if (saveData != null)
        {
            currentQuestIndex = saveData.currentQuestIndex;
            timeDisplay.text = $"Time: {saveData.timeDescription}";
        }
    }
}
