using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] ItemEquipment itemEquipment;
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUI inventoryUI;


    public void GetUseItem()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;
        if (itemEquipment.selectedSlotIndex < 0 || itemEquipment.currentItem == null) return;
        int index = itemEquipment.selectedSlotIndex;

        ItemDataHolder holder = inventory.slots[index].holder;
        if (holder == null || holder.itemData == null) return;

        GameObject player = transform.parent.gameObject;
        if (player == null) return;

        holder.itemData.ItemUse(player, holder);
    }

    public void rechargeItem() //충전 아이템 후처리
    {
        inventoryUI.RefreshSlotUI();
    }

    public void DurabilityItem(int durability) //내구도 아이템 후처리
    {
        int index = itemEquipment.selectedSlotIndex;
        if (durability <= 0) inventory.ClearSlot(index);
        inventoryUI.RefreshSlotUI();
    }

    public void ConsumeItem(int stack) //소모 아이템 후처리
    {
        int index = itemEquipment.selectedSlotIndex;
        if (stack <= 0) inventory.ClearSlot(index);
        inventoryUI.RefreshSlotUI();
    }
}
