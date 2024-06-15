using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class D_Sentence
{
    public string sentence;
}


[CreateAssetMenu(fileName = "D_NewDialog", menuName = "Dialog")]
public class D_Dialog : ScriptableObject
{
    [Tooltip("NPC's Name")]
    public string npcName;

    [Tooltip("Shows character portraits contained in this dialog")]
    public Sprite[] portraits;

    [Tooltip("The sentences the npcs say when the player talks to them")]
    [TextArea(3, 10)]
    public string[] sentences;

}
