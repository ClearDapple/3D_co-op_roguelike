public class ItemSlot
{
    public ItemDataSO itemData;
    public int quantity; // 현재 수량

    public bool IsFull => quantity >= itemData.maxStack;

    public bool IsSameItem(ItemDataSO other)
    {
        return itemData.itemID == other.itemID;
    }
}
