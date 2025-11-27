using UnityEngine;

public interface IItemEffect
{
    void Apply(GameObject target);
}

[System.Serializable]
public class AttackEffect : IItemEffect
{
    public int damageAmount;
    public void Apply(GameObject monster)
    {
        if (monster == null) return;
        PlayerStats playerStats = monster.GetComponent<PlayerStats>();
        if (playerStats == null) return;
        playerStats.TakeDamage(damageAmount);
    }
}

[System.Serializable]
public class HealEffect : IItemEffect
{
    public int healAmount;
    public void Apply(GameObject player)
    {
        if (player == null) return;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null) return;
        playerStats.TakeDamage(healAmount);
    }
}

[System.Serializable]
public class DeadEffect : IItemEffect
{
    public void Apply(GameObject player)
    {
        if (player == null) return;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null) return;
        playerStats.PlayerDead();
    }
}