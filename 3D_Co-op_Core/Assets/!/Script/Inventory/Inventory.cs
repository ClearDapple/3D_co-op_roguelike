using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<ItemSlot> slots = new List<ItemSlot>();
    public int maxSlots = 5;


    public int TryAddItem(ItemDataSO newItem, int itemStack)
    {
        // 0. 빈 공간 계산
        int emptySpace = 0;
        foreach (ItemSlot slot in slots)
        {
            if (slot.IsSameItem(newItem) && !slot.IsFull)
            {
                emptySpace += (newItem.maxStack - slot.quantity);
            }
        }
        int emptySlots = maxSlots - slots.Count;
        emptySpace += emptySlots * newItem.maxStack;

        // 0-1. 빈 공간 없음
        if (emptySpace <= 0)
        {
            Debug.LogWarning($"[실패: 빈 공간 없음] {newItem.itemName} x{itemStack}");
            return itemStack;
        }
        else
        {
            // 1. 아이템을 기존 슬롯에 추가
            foreach (ItemSlot slot in slots)
            {
                if (slot.IsSameItem(newItem) && !slot.IsFull)
                {
                    // 남은 아이템 수량이 기존 슬롯에 꽉 찰 수 있는 경우
                    if (itemStack >= newItem.maxStack - slot.quantity)
                    {
                        itemStack -= (newItem.maxStack - slot.quantity);
                        slot.quantity = newItem.maxStack;

                        // 남은 아이템 수량 없음
                        if (itemStack <= 0) { return itemStack; }
                    }
                    // 남은 아이템 수량이 기존 슬롯에 꽉 차지 않는 경우
                    else
                    {
                        slot.quantity += itemStack;
                        itemStack = 0;
                        return itemStack;
                    }
                }
            }

            // 2. 남은 아이템을 새 슬롯에 추가
            while (maxSlots > slots.Count)
            {
                // 남은 아이템 수량이 새 슬롯에 꽉 찰 수 있는 경우
                if (itemStack >= newItem.maxStack)
                {
                    ItemSlot newSlot = new ItemSlot { itemData = newItem, quantity = newItem.maxStack };
                    slots.Add(newSlot);
                    itemStack -= newItem.maxStack;
                }
                // 남은 아이템 수량이 새 슬롯에 꽉 차지 않는 경우
                else if (itemStack > 0)
                {
                    ItemSlot newSlot = new ItemSlot { itemData = newItem, quantity = itemStack };
                    slots.Add(newSlot);
                    itemStack = 0;
                    return itemStack;
                }
                // 남은 아이템 수량 없음
                else
                {
                    return itemStack;
                }
            }

            return itemStack;
        }
    }

    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA == indexB) return;
        if (indexA < 0 || indexB < 0 || indexA >= slots.Count || indexB >= slots.Count) return;

        var temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;
    }
}