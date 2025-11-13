using UnityEngine;

[CreateAssetMenu(fileName = "CubeG", menuName = "Scriptable Objects/CubeG")]
public class CubeG : UseableItemDataSO
{
    [Header("UseableItemDataSO - CubeG")]
    public int itemHeal;

    public override void ItemUse(GameObject player)
    {
        // 애니메이션
        Debug.Log($"{itemName} 아이템 사용");
        Animator animator = player.GetComponentInChildren<Animator>();
        animator.SetTrigger("Heal");

        // 회복
        var stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.TakeHeal(itemHeal);
        }

        // 인벤토리에서 제거
        var inventory = player.GetComponentInChildren<Inventory>();
        if (inventory != null)
        {
            //inventory.RemoveItem(this);
        }
    }
}
