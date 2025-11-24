using UnityEngine;

public interface IItemEffect
{
    void Apply(GameObject player);
}

[System.Serializable]
public class AttackEffect : IItemEffect
{
    public int damageAmount;
    public void Apply(GameObject player)
    {
        if (player == null) return;
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
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