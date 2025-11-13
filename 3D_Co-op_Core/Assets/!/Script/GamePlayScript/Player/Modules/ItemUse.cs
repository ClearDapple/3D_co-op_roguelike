using UnityEngine;

public class ItemUse : MonoBehaviour
{
    [SerializeField] ItemEquipment itemEquipment;


    public void GetUseItem()
    {
        if (itemEquipment.selectedSlotIndex < 0 || itemEquipment.currentItem == null) return;

        GameObject itemObject = itemEquipment.currentItem;

        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();
        if (holder == null || holder.itemData == null) return;

        GameObject player = transform.parent.gameObject;
        if (player == null) return;

        if (holder.itemData is UseableItemDataSO usable)
        {
            usable.ItemUse(player);
        }
    }
}
