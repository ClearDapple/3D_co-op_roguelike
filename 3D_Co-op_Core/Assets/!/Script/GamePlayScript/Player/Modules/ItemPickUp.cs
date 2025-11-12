using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUI inventoryUI;
    [SerializeField] ObjectCheckUI objectCheckUI;
    [SerializeField] SystemMessageUI systemMessageUI;

    [Header("Animator")]
    [SerializeField] Animator animator;


    public void PickUp(GameObject itemObject)
    {
        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();

        if (holder == null || holder.itemData == null)
        {
            Debug.Log("아이템 데이터가 없습니다.");
            return;
        }

        int remainItemStack = inventory.TryAddItem(holder.itemData, holder.currentStack);

        if (remainItemStack <= 0) //음수 또는 0 이면 모두 획득
        {
            Debug.Log($"[성공] {holder.itemData.itemName} 아이템 {holder.currentStack} 모두 획득함. 남은 개수 없음.");
            animator.SetTrigger("PickUp");
            holder.DestorySelf();
        }
        else if (remainItemStack == holder.currentStack) //남은 개수가 현재 스택과 같으면 하나도 못얻음
        {
            Debug.Log($"[실패] {holder.itemData.itemName} x {holder.currentStack}: 아이템 획득 불가능. 빈 공간 없음.");
            systemMessageUI.ShowMessage("공간이 부족합니다!");
        }
        else //일부만 획득
        {
            Debug.Log($"{holder.itemData.itemName} 아이템 {holder.currentStack - remainItemStack} 획득함. 남은 개수: {remainItemStack}.");
            animator.SetTrigger("PickUp");
            holder.currentStack = remainItemStack;
            objectCheckUI.ItemCheckUI(holder);//UI 갱신
        }

        inventoryUI.RefreshSlotUI();
    }
}
