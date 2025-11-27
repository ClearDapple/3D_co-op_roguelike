using UnityEngine;

public class ItemDataHolder : MonoBehaviour
{
    public ItemDataSO itemData;
    public int currentReloadCount; //충전 아이템 전용(충전 수)
    public int currentDurability;  //내구도 아이템 전용(내구도)
    public int currentStack;       //공통 아이템(수량)


    public ItemDataHolder(ItemDataSO itemData, int currentReloadCount, int currentDurability, int currentStack)
    {
        this.itemData = itemData;
        this.currentReloadCount = currentReloadCount;
        this.currentDurability = currentDurability;
        this.currentStack = currentStack;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}