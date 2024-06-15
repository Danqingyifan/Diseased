using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int maxStamina = 100;
    [SerializeField] private int health;
    [SerializeField] private int stamina;
    [SerializeField] private int attack;
    [SerializeField] private int defense;

    [Header("UI Texts")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;

    void Start()
    {
        // 初始化属性值
        health = Mathf.Clamp(health, 0, maxHealth); // 确保生命值在 0 到最大值之间
        stamina = Mathf.Clamp(stamina, 0, maxStamina); // 确保活力值在 0 到最大值之间
        UpdateStatDisplays();
    }

    // 修改属性值的方法
    public void ModifyHealth(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth); // 确保生命值在 0 到最大值之间
        UpdateStatDisplays();
    }

    public void ModifyStamina(int amount)
    {
        stamina += amount;
        stamina = Mathf.Clamp(stamina, 0, maxStamina); // 确保活力值在 0 到最大值之间
        UpdateStatDisplays();
    }

    public void ModifyAttack(int amount)
    {
        attack += amount;
        UpdateStatDisplays();
    }

    public void ModifyDefense(int amount)
    {
        defense += amount;
        UpdateStatDisplays();
    }

    // 更新属性显示
    public void UpdateStatDisplays()
    {
        if (healthText != null)
        {
            healthText.text = $"生命值: {health}/{maxHealth}";
        }
        if (staminaText != null)
        {
            staminaText.text = $"活力值: {stamina}/{maxStamina}";
        }
        if (attackText != null)
        {
            attackText.text = $"攻击力: {attack}";
        }
        if (defenseText != null)
        {
            defenseText.text = $"防御力: {defense}";
        }
    }
}