using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class InventoryUI : MonoBehaviour
{
    [Header("UI - Document")]
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;

    [Header("Modules")]
    [SerializeField] ItemEquipment itemEquipment;
    [SerializeField] ItemDrop itemDrop;

    [Header("UI")]
    [SerializeField] Inventory inventory;

    private List<VisualElement> slotElements = new();
    private List<Label> quantityLabels = new();

    private VisualElement draggedSlot = null;
    private int draggedIndex = -1;
    
    private VisualElement hoveredSlot = null;
    private int hoveredIndex = -1;


    void Awake()
    {
        root = uiDocument.rootVisualElement;

        slotElements.Add(root.Q<VisualElement>("Slot1"));
        slotElements.Add(root.Q<VisualElement>("Slot2"));
        slotElements.Add(root.Q<VisualElement>("Slot3"));
        slotElements.Add(root.Q<VisualElement>("Slot4"));
        slotElements.Add(root.Q<VisualElement>("Slot5"));

        quantityLabels.Add(root.Q<Label>("Text1"));
        quantityLabels.Add(root.Q<Label>("Text2"));
        quantityLabels.Add(root.Q<Label>("Text3"));
        quantityLabels.Add(root.Q<Label>("Text4"));
        quantityLabels.Add(root.Q<Label>("Text5"));
    }

    private void Start()
    {
        for (int i = 0; i < slotElements.Count; i++)
        {
            RegisterSlotEvents(slotElements[i], i);
        }

        root.RegisterCallback<PointerUpEvent>(evt => // 클릭 UP
        {
            if (Cursor.lockState == CursorLockMode.Locked) return;

            DragIconController.instance.Hide();

            if (draggedSlot == null)
            {
                Debug.Log("[실패] 선택된 슬롯이 없습니다.");
                return;
            }

            if (inventory.slots[draggedIndex].holder == null)
            {
                Debug.Log($"[실패] {draggedIndex + 1}번 슬롯은 빈 슬롯 입니다.");
                ClearSlotTopColor();
                return;
            }

            if (draggedSlot != hoveredSlot)
            {
                if (hoveredSlot != null)
                {
                    Debug.Log($"슬롯 교환 시도: {draggedIndex + 1} ↔ {hoveredIndex + 1}");
                    inventory.SwapSlots(draggedIndex, hoveredIndex);
                }
                else
                {
                    Debug.Log($"슬롯 아이템 드롭 시도: {draggedIndex + 1} ↔ 드롭됨");
                    var slots = inventory.slots[draggedIndex];
                    var itemData = slots.holder.itemData;
                    var quantity = slots.holder.currentStack;

                    Debug.Log($"[아이템 드롭됨] {draggedIndex + 1}번 슬롯: {itemData.itemName} x{quantity}");
                    itemDrop.DropItem(itemData, quantity);
                    inventory.ClearSlot(draggedIndex);
                }
                RefreshSlotUI();
            }
            ClearSlotTopColor();
        });
    }

    void ClearSlotTopColor()
    {
        if (draggedSlot != null)
        {
            draggedSlot.style.borderTopWidth = 0;
            draggedSlot = null;
            draggedIndex = -1;
        }
        hoveredSlot = null;
    }

    private void RegisterSlotEvents(VisualElement slot, int index)
    {
        slot.pickingMode = PickingMode.Position;

        slot.RegisterCallback<PointerDownEvent>(evt => // 클릭 DOWN
        {
            if (Cursor.lockState == CursorLockMode.Locked) return;

            if (index < 0 || inventory.slots.Count <= index) return;

            Debug.Log($"슬롯 {index+1} 클릭됨");
            draggedSlot = slot;
            draggedIndex = index;

            // 인덱스 유효성 검사

            // 드래그 아이콘 표시
            if (inventory.slots[index].holder != null)
            {
                var itemData = inventory.slots[index].holder.itemData;
                DragIconController.instance.Show(itemData.itemIcon);
            }

            // 드래그 시작 시 시각적 피드백
            slot.style.borderTopColor = Color.yellow;
            slot.style.borderTopWidth = 2;
        });

        slot.RegisterCallback<PointerEnterEvent>(evt =>
        {
            if (Cursor.lockState == CursorLockMode.Locked) return;

            hoveredSlot = slot;
            hoveredIndex = index;
            //Debug.Log($"PointerEnter 발생: name={slot.name}, index={index}");
        });

        slot.RegisterCallback<PointerLeaveEvent>(evt =>
        {
            if (Cursor.lockState == CursorLockMode.Locked) return;

            if (hoveredSlot == slot)
            {
                hoveredSlot = null;
                hoveredIndex = -1;
            }
        });
    }

    public void RefreshSlotUI()
    {
        var slots = inventory.slots;
        string debugLog = "[RefreshSlotUI] 슬롯 상태: ";

        for (int i = 0; i < slotElements.Count; i++) // 모든 UI 슬롯 요소를 순회하면서 UI를 갱신
        {
            if (i < slots.Count && slots[i] != null) // 슬롯이 존재하는 경우
            {
                var holder = slots[i].holder;
                if (holder != null && holder.itemData != null) // 아이템이 있는 경우
                {
                    var itemData = holder.itemData;
                    slotElements[i].style.backgroundImage = itemData.itemIcon.texture; // 아이콘 표시

                    switch (itemData.itemType)
                    {
                        case ItemType.ImportanceItem:// 중요 아이템
                            quantityLabels[i].text = ""; // 텍스트 제거
                            debugLog += $"\n슬롯 {i + 1}: {itemData.itemName}[중요 아이템]";
                            break;

                        case ItemType.rechargeItem:// 충전 아이템
                            quantityLabels[i].text = $"{holder.currentReloadCount}/{itemData.maxReloadCount}"; // 충전량 표시
                            debugLog += $"\n슬롯 {i + 1}: {itemData.itemName}[충전 아이템] ({holder.currentReloadCount}/{itemData.maxReloadCount})";
                            break;

                        case ItemType.DurabilityItem:// 내구도 아이템
                            quantityLabels[i].text = $"{holder.currentDurability}/{itemData.maxDurability}"; // 내구도 표시
                            debugLog += $"\n슬롯 {i + 1}: {itemData.itemName}[내구도 아이템] ({holder.currentDurability}/{itemData.maxDurability})";
                            break;

                        case ItemType.ConsumeItem:// 소모 아이템
                            quantityLabels[i].text = $"{holder.currentStack}/{itemData.itemMaxStack}"; // 수량 표시
                            debugLog += $"\n슬롯 {i + 1}: {itemData.itemName}[소모 아이템] ({holder.currentStack}/{itemData.itemMaxStack})";
                            break;

                        default:// 기타 아이템(예외상황)
                            quantityLabels[i].text = ""; // 텍스트 제거
                            debugLog += $"\n슬롯 {i + 1}: {itemData.itemName}[기타 아이템(예외상황)]";
                            break;
                    }
                }
                else // 아이템이 없는 경우 (빈 슬롯)
                {
                    slotElements[i].style.backgroundImage = null; // 아이콘 제거
                    quantityLabels[i].text = ""; // 텍스트 제거
                    debugLog += $"\n슬롯 {i + 1}: 빈 슬롯";
                }
            }
            else // 슬롯 자체가 없거나 범위를 벗어난 경우
            {
                slotElements[i].style.backgroundImage = null; // 아이콘 제거
                quantityLabels[i].text = ""; // 텍스트 제거
                debugLog += $"\n슬롯 {i + 1}: 슬롯 없음";
            }
        }
        Debug.Log($"{debugLog}");
        itemEquipment.RefreshEquipItem(); // 착용 아이템도 함께 갱신
    }
}
