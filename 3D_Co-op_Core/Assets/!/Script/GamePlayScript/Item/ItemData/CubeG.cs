using UnityEngine;

[CreateAssetMenu(fileName = "CubeG", menuName = "Scriptable Objects/CubeG")]
public class CubeG : ItemDataSO
{
    public override void ItemUse(GameObject player, ItemDataHolder myHolder)
    {
        Debug.Log($"{itemName} 아이템 사용");


        // 애니메이션
        player.GetComponentInChildren<Animator>()?.SetTrigger("Heal");


        // 회복
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null) return;
        if (playerStats.currentHP >= playerStats.maxHP)
        {
            Debug.Log("체력이 가득 차 있습니다.");
            return;
        }
        playerStats.TakeHeal(itemPower);
        myHolder.currentStack = Mathf.Clamp(myHolder.currentStack--, 0, itemMaxStack);
        player.GetComponentInChildren<ItemUse>()?.ConsumeItem(myHolder.currentStack);
    }
}
