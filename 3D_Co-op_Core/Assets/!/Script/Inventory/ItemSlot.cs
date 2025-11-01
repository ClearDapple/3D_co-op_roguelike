public class ItemSlot
{
    public ItemDataSO itemData;
    public int quantity; // 현재 수량

    public bool IsFull => itemData != null && quantity >= itemData.itemMaxStack;

    public bool IsEmpty => itemData == null || quantity <= 0;

    public bool IsSameItem(ItemDataSO other)
    {
        if (itemData == null || other == null) return false;
        return itemData.itemID == other.itemID;
    }
}
