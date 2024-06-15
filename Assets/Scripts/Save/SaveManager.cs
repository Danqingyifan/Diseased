using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static string saveFilePath;

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public static void SaveGame(int currentQuestIndex, string timeDescription)
    {
        SaveData saveData = new SaveData();
        saveData.currentQuestIndex = currentQuestIndex;
        saveData.timeDescription = timeDescription;

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveFilePath, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            return null;
        }
    }
}