using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Setting")]
    public int maxSlots = 5;

    public List<InventorySlot> slots = new List<InventorySlot>();


    private void Start()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot { itemData = null, quantity = 0 });
        }
    }

    public int TryAddItem(ImportanceItemDataSO newItem, int itemStack)
    {
        // 0. 빈 공간 계산
        int emptySpace = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsSameItem(newItem) && !slot.IsFull)
            {
                emptySpace += (newItem.itemMaxStack - slot.quantity);
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                emptySpace += newItem.itemMaxStack;
            }
        }

        Debug.Log($"빈 공간 계산: emptySpace={emptySpace}");

        // 0. 빈 공간 없음
        if (emptySpace <= 0)
        {
            Debug.Log("0. 빈 공간 없음");
            return itemStack;
        }
        else
        {
            // 1. 아이템을 기존 슬롯에 추가
            Debug.Log("1. 아이템을 기존 슬롯에 추가");
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsSameItem(newItem) && !slot.IsFull && itemStack > 0)
                {
                    // 1-1 남은 아이템 수량이 기존 슬롯에 꽉 찰 수 있는 경우
                    if (itemStack >= newItem.itemMaxStack - slot.quantity)
                    {
                        Debug.Log("1-1 남은 아이템 수량이 기존 슬롯에 꽉 찰 수 있는 경우");
                        itemStack -= (newItem.itemMaxStack - slot.quantity);
                        slot.quantity = newItem.itemMaxStack;
                    }
                    // 1-2 남은 아이템 수량이 기존 슬롯에 꽉 차지 않는 경우
                    else
                    {
                        Debug.Log("1-2 남은 아이템 수량이 기존 슬롯에 꽉 차지 않는 경우");
                        slot.quantity += itemStack;
                        return 0;
                    }
                }
            }

            // 2. 남은 아이템을 새 슬롯에 추가
            Debug.Log("2. 남은 아이템을 새 슬롯에 추가");
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsEmpty && itemStack > 0)
                {
                    // 2-1 남은 아이템 수량이 새 슬롯에 꽉 찰 수 있는 경우
                    if (itemStack >= newItem.itemMaxStack)
                    {
                        Debug.Log("2-1 남은 아이템 수량이 새 슬롯에 꽉 찰 수 있는 경우");
                        slot.itemData = newItem;
                        slot.quantity = newItem.itemMaxStack;
                        itemStack -= newItem.itemMaxStack;
                    }
                    // 2-2 남은 아이템 수량이 새 슬롯에 꽉 차지 않는 경우
                    else
                    {
                        Debug.Log("2-2 남은 아이템 수량이 새 슬롯에 꽉 차지 않는 경우");
                        slot.itemData = newItem;
                        slot.quantity = itemStack;
                        return 0;
                    }
                }
            }

            // 3. 빈 칸이 없는 경우 > 남은 아이템 수량 반환
            Debug.Log("3. 빈 칸이 없는 경우 > 남은 아이템 수량 반환");
            return itemStack;
        }
    }

    public void ClearSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;
        slots[index].itemData = null;
        slots[index].quantity = 0;
    }

    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA == indexB) return;
        Debug.Log($"slots.Count={slots.Count}, A={indexA}, B={indexB}");
        if (indexA < 0 || indexB < 0 || indexA >= slots.Count || indexB >= slots.Count) return;

        var slotA = slots[indexA];
        var slotB = slots[indexB];

        // slotA에 아이템이 없으면 교환하지 않음
        if (slotA == null || slotA.itemData == null) return;

        // slotB가 null이거나 itemData가 없어도 교환은 허용
        slots[indexA] = slotB;
        slots[indexB] = slotA;
        Debug.Log($"교환 실행됨: A={indexA}, B={indexB}");
    }
}