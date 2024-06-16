using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Player;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        string name = gameObject.name;
        gameObject.GetComponent<Button>().onClick.AddListener(() => AttachCallBack(name));
    }

    private void AttachCallBack(string buttonName)
    {
        Player.GetComponent<BattleActionHandler>().SelectAction(buttonName);
    }
}
