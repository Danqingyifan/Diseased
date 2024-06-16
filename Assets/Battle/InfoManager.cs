using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;

    private GameObject playerInfoPanel;
    private GameObject enemyInfoPanel;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }
    // Start is called before the first frame update
    void Start()
    {
        BattleStatus playerStatus = player.GetComponent<BattleStatus>();
        
    }
}
