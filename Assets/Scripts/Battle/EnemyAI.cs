using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public void PerformAIAction()
    {
        gameObject.GetComponent<BattleActionHandler>().SelectAction("Attack");
    }
}
