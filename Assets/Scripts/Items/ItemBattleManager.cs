using UnityEngine;

public class ItemBattleManager : MonoBehaviour
{
    public static bool isBattle; // 是否处于战斗状态

    // 切换战斗状态
    public void SetBattleState(bool battleState)
    {
        isBattle = battleState;
        UpdateAllItemSlots();
    }

    // 更新所有 ItemSlot 实例的战斗状态
    private void UpdateAllItemSlots()
    {
        ItemSlot[] itemSlots = UnityEngine.Object.FindObjectsOfType<ItemSlot>();
        foreach (ItemSlot slot in itemSlots)
        {
            slot.SetBattleState(isBattle);
        }
    }
}