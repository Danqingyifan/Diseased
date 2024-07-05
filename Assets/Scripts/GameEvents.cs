using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // ����һ��ί�к��¼�
    public static event Action<int> OnHealthModified;

    public static event Action<string> OnUseBattleItem;

    // ����һ�������������¼�
    public static void RaiseHealthModified(int amount)
    {
        OnHealthModified?.Invoke(amount);
    }

    public static void RaiseUseBattleItem(string itemName)
    {
        OnUseBattleItem?.Invoke(itemName);
    }
}