using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // 定义一个委托和事件
    public static event Action<int> OnHealthModified;

    public static event Action<string> OnUseBattleItem;

    // 定义一个方法来触发事件
    public static void RaiseHealthModified(int amount)
    {
        OnHealthModified?.Invoke(amount);
    }

    public static void RaiseUseBattleItem(string itemName)
    {
        OnUseBattleItem?.Invoke(itemName);
    }
}