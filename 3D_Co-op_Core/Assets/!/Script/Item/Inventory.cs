using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 5;
    public List<ItemSlot> slots = new List<ItemSlot>();

    public bool TryAddItem(ItemData newItem)
    {
        // 1. 같은 아이템이 있고 스택이 안 찼으면 → 수량 증가
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsSameItem(newItem) && !slot.IsFull)
            {
                slot.quantity++;
                Debug.Log($"[스택 증가] {newItem.itemName} → {slot.quantity}/{newItem.maxStack}");
                return true;
            }
        }

        // 2. 같은 아이템이 있지만 모두 스택이 가득 찼음
        bool hasSameItem = slots.Exists(s => s.IsSameItem(newItem));
        if (hasSameItem)
        {
            if (slots.Count < maxSlots) // 빈칸 있음
            {
                // → 새 슬롯 추가
                ItemSlot newSlot = new ItemSlot { itemData = newItem, quantity = 1 };
                slots.Add(newSlot);
                Debug.Log($"[새 슬롯 추가] {newItem.itemName} → 1/{newItem.maxStack}");
                return true;
            }
            else // 빈칸 없음
            {
                // → 줍기 실패
                Debug.LogWarning($"[실패] {newItem.itemName} 스택 가득 + 슬롯 없음");
                return false;
            }
        }

        // 3. 아이템이 없고 빈 슬롯이 있다면 → 새 슬롯 추가
        if (slots.Count < maxSlots)
        {
            ItemSlot newSlot = new ItemSlot { itemData = newItem, quantity = 1 };
            slots.Add(newSlot);
            Debug.Log($"[새 아이템 추가] {newItem.itemName} → 1/{newItem.maxStack}");
            return true;
        }

        // 4. 아이템 없고 빈 슬롯도 없음 → 줍기 실패
        Debug.LogWarning($"[실패] {newItem.itemName} 없음 + 슬롯 없음");
        return false;
    }
}