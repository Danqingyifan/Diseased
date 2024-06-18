using UnityEngine;
using Fungus;

[CommandInfo("Custom",
    "SetCharacterPosition",
    "Sets the position of the specified character to the position of the target object.")]
public class SetCharacterPosition : Command
{
    [Tooltip("Character whose position we want to set")]
    [SerializeField] private Transform character;

    [Tooltip("Target object whose position we will use")]
    [SerializeField] private Transform targetObject;

    public override void OnEnter()
    {
        if (character != null && targetObject != null)
        {
            character.position = targetObject.position;
        }
        else
        {
            Debug.LogWarning("Character or target object is not assigned.");
        }

        Continue(); // Continue to the next command in the block
    }

    public override string GetSummary()
    {
        if (character == null)
        {
            return "Error: No character assigned";
        }
        if (targetObject == null)
        {
            return "Error: No target object assigned";
        }
        return character.name + " to " + targetObject.name;
    }

    public override Color GetButtonColor()
    {
        return new Color32(184, 210, 235, 255);
    }
}