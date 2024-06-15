using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor
{
    private bool showSpawnPositions = true;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuestManager questManager = (QuestManager)target;

        if (GUILayout.Button("Add New Quest"))
        {
            questManager.quests.Add(new QuestData());
        }

        for (int i = 0; i < questManager.quests.Count; i++)
        {
            EditorGUILayout.LabelField("Quest " + (i + 1));
            questManager.quests[i].questDescription = EditorGUILayout.TextField("Quest Description", questManager.quests[i].questDescription);
            questManager.quests[i].timeDescription = EditorGUILayout.TextField("Time Description", questManager.quests[i].timeDescription);

            EditorGUILayout.LabelField("Character Prefabs");
            SerializedProperty characterPrefabsProperty = serializedObject.FindProperty("quests").GetArrayElementAtIndex(i).FindPropertyRelative("characterPrefabs");
            EditorGUILayout.PropertyField(characterPrefabsProperty, new GUIContent(""), true);

            showSpawnPositions = EditorGUILayout.Foldout(showSpawnPositions, "Spawn Positions");
            if (showSpawnPositions)
            {
                SerializedProperty spawnPositionsProperty = serializedObject.FindProperty("quests").GetArrayElementAtIndex(i).FindPropertyRelative("spawnPositions");
                for (int j = 0; j < spawnPositionsProperty.arraySize; j++)
                {
                    EditorGUILayout.PropertyField(spawnPositionsProperty.GetArrayElementAtIndex(j), new GUIContent("Spawn Position " + (j + 1)));
                }

                if (GUILayout.Button("Add Spawn Position"))
                {
                    spawnPositionsProperty.InsertArrayElementAtIndex(spawnPositionsProperty.arraySize);
                }
            }

            EditorGUILayout.LabelField("Dialog Triggers");
            SerializedProperty dialogTriggersProperty = serializedObject.FindProperty("quests").GetArrayElementAtIndex(i).FindPropertyRelative("dialogTriggers");
            EditorGUILayout.PropertyField(dialogTriggersProperty, new GUIContent(""), true);

            questManager.quests[i].dialog = (D_Dialog)EditorGUILayout.ObjectField("Dialog", questManager.quests[i].dialog, typeof(D_Dialog), false);

            if (GUILayout.Button("Remove Quest"))
            {
                questManager.quests.RemoveAt(i);
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        QuestManager questManager = (QuestManager)target;

        for (int i = 0; i < questManager.quests.Count; i++)
        {
            if (questManager.quests[i].spawnPositions != null)
            {
                for (int j = 0; j < questManager.quests[i].spawnPositions.Length; j++)
                {
                    Vector3 spawnPosition = questManager.quests[i].spawnPositions[j];
                    Vector3 newSpawnPosition = Handles.PositionHandle(spawnPosition, Quaternion.identity);

                    if (newSpawnPosition != spawnPosition)
                    {
                        Undo.RecordObject(questManager, "Move Spawn Position");
                        questManager.quests[i].spawnPositions[j] = newSpawnPosition;
                        EditorUtility.SetDirty(questManager);
                    }
                }
            }
        }
    }
}
