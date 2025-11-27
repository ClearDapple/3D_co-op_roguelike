using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] InventoryUI inventoryUI;

    public int maxSlots = 5;

    public List<InventorySlot> slots = new List<InventorySlot>();


    private void Start()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot { holder = null });
        }
    }

    public int TryAddItem(ItemDataHolder holder)
    {
        // 0. 빈 공간 계산
        string debugLog = "[빈 공간 계산] ";
        int emptySpace = 0;

        foreach (InventorySlot slot in slots)
        {
            if (slot.IsSameItem(holder.itemData) && !slot.IsFull)
            {
                emptySpace += (holder.itemData.itemMaxStack - slot.holder.currentStack);
            }
        }
        int spaceTemp = emptySpace;
        debugLog += $"{emptySpace}+";
        foreach (InventorySlot slot in slots)
        {
            if (slot.IsEmpty)
            {
                emptySpace += holder.itemData.itemMaxStack;
            }
        }
        debugLog += $"{emptySpace - spaceTemp}=";
        debugLog += $"빈 공간={emptySpace}칸";

        // 0. 빈 공간 없음
        if (emptySpace <= 0)
        {
            debugLog += $"빈 공간 없음.(end)";
            Debug.Log($"{debugLog}");
            return holder.currentStack;
        }
        else
        {
            // 1. 아이템을 기존 슬롯에 추가
            debugLog += $"\n[아이템을 기존 슬롯에 추가] ";
            int temp = 0;
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsSameItem(holder.itemData) && !slot.IsFull && holder.currentStack > 0)
                {
                    temp++;
                    // 1-1 남은 아이템 수량이 기존 슬롯에 꽉 찰 수 있는 경우
                    if (holder.currentStack >= holder.itemData.itemMaxStack - slot.holder.currentStack)
                    {
                        holder.currentStack -= (holder.itemData.itemMaxStack - slot.holder.currentStack);
                        slot.holder.currentStack = slot.holder.itemData.itemMaxStack;
                    }
                    // 1-2 남은 아이템 수량이 기존 슬롯에 꽉 차지 않는 경우
                    else
                    {
                        slot.holder.currentStack += holder.currentStack;
                        holder.currentStack = 0;
                        debugLog += $"{temp}회 반복함.(1-2)";
                        Debug.Log($"{debugLog}");
                        return 0;
                    }
                }
            }
            debugLog += $"{temp}회 반복함.";

            // 2. 남은 아이템을 새 슬롯에 추가
            debugLog += $"\n[남은 아이템을 새 슬롯에 추가] ";
            temp = 0;
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsEmpty && holder.currentStack > 0)
                {
                    temp++;
                    // 2-1 남은 아이템 수량이 새 슬롯에 꽉 찰 수 있는 경우
                    if (holder.currentStack >= holder.itemData.itemMaxStack)
                    {
                        slot.holder = new ItemDataHolder(holder.itemData, holder.currentReloadCount, holder.currentDurability, holder.itemData.itemMaxStack);
                        holder.currentStack -= holder.itemData.itemMaxStack;
                    }
                    // 2-2 남은 아이템 수량이 새 슬롯에 꽉 차지 않는 경우
                    else
                    {
                        slot.holder = new ItemDataHolder(holder.itemData, holder.currentReloadCount, holder.currentDurability, holder.currentStack);
                        holder.currentStack = 0;
                    }
                }
            }
            // 2-3 남은 아이템 수량이 0이 된 경우
            if (holder.currentStack <= 0)
            {
                holder.currentStack = 0;
                debugLog += $"{temp}회 반복함.(2-3)";
                Debug.Log($"{debugLog}");
                return 0;
            }

            // 3. 빈 칸이 없는 경우 > 남은 아이템 수량 반환
            debugLog += $"\n[빈 칸이 없는 경우 > 남은 아이템 수량 반환] (3-0)";
            Debug.Log($"{debugLog}");
            return holder.currentStack;
        }
    }

    public void ClearSlot(int index)
    {
        if (index < 0 || index >= slots.Count) return;
        slots[index].holder = null;
    }

    public void SwapSlots(int indexA, int indexB)
    {
        if (indexA == indexB) return;

        Debug.Log($"slots.Count={slots.Count}, A={indexA}, B={indexB}");
        if (indexA < 0 || indexB < 0 || indexA >= slots.Count || indexB >= slots.Count) return;

        var slotA = slots[indexA];
        var slotB = slots[indexB];

        // slotA에 아이템이 없으면 교환하지 않음
        if (slotA == null || slotA.holder == null) return;

        // slotB가 null이거나 itemData가 없어도 교환은 허용
        slots[indexA] = slotB;
        slots[indexB] = slotA;
        Debug.Log($"교환 실행됨: A={indexA}, B={indexB}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Inventory Slot Info:");
            for (int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                if (slot.holder != null)
                {
                    Debug.Log($"Slot {i + 1}: Item={slot.holder.itemData.itemName}, Stack={slot.holder.currentStack}");
                }
                else
                {
                    Debug.Log($"Slot {i + 1}: Empty");
                }
            }
        }
    }



}