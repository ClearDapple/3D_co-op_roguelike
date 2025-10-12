public class ItemSlot
{
    public ItemData itemData;
    public int quantity;

    public bool IsFull => quantity >= itemData.maxStack;
    public bool IsSameItem(ItemData other) => itemData == other;
}
