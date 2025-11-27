public class InventorySlot
{
    public ItemDataHolder holder;


    public bool IsFull => holder != null
                          && holder.itemData != null
                          && holder.currentStack >= holder.itemData.itemMaxStack;

    public bool IsEmpty => holder == null
                           || holder.itemData == null
                           || holder.currentStack <= 0;

    public bool IsSameItem(ItemDataSO other)
    {
        if (holder == null || holder.itemData == null || other == null) return false;
        return holder.itemData.itemName == other.itemName;
    }
}
