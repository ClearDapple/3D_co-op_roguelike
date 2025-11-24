public class InventorySlot
{
    public ImportanceItemDataSO itemData;
    public int quantity; // 현재 수량

    public bool IsFull => itemData != null && quantity >= itemData.itemMaxStack;

    public bool IsEmpty => itemData == null || quantity <= 0;

    public bool IsSameItem(ImportanceItemDataSO other)
    {
        if (itemData == null || other == null) return false;
        return itemData.itemName == other.itemName;
    }
}
