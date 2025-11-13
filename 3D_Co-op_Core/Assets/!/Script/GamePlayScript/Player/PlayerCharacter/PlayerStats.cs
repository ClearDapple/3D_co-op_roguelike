using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHP = 150;
    public int currentHP;

    private void Start()
    {
        maxHP = 150;
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        var temp = currentHP;
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        Debug.Log($"[피격] 플레이어 {damage} 피해입음.\n" + $"HP: {temp} > {currentHP}");

        if (currentHP <= 0) PlayerDead();
    }

    public void TakeHeal(int heal)
    {
        var temp = currentHP;
        currentHP = Mathf.Clamp(currentHP + heal, 0, maxHP);
        Debug.Log($"[회복] 플레이어 {heal}({currentHP - temp}) 회복함.\n" + $"HP: {temp} > {currentHP}");
    }

    public void PlayerDead()
    {
        Debug.Log("플레이어 사망함.");
    }
}
