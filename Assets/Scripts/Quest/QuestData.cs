using UnityEngine;

[System.Serializable]
public class QuestData
{
    public string questDescription;
    public string timeDescription;
    public GameObject[] characterPrefabs; // Prefabs of characters appearing in this quest
    public Vector3[] spawnPositions; // Spawn positions for each character
    public D_Dialog dialog; // Dialog associated with this quest
    public D_DialogTrigger[] dialogTriggers; // Dialog triggers for this quest
}