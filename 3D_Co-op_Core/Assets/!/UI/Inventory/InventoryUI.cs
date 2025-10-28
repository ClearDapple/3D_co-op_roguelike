using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;

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

        for (int i = 0; i < slotElements.Count; i++)
        {
            RegisterSlotEvents(slotElements[i], i);
        }

        root.RegisterCallback<PointerUpEvent>(evt =>
        {
            DragIconController.Instance.Hide();

            if (draggedSlot != null && hoveredSlot != null && draggedSlot != hoveredSlot)
            {
                Debug.Log($"슬롯 교환 시도: {draggedIndex+1} ↔ {hoveredIndex+1}");
                GameManager.Instance.inventory.SwapSlots(draggedIndex, hoveredIndex);
                Refresh();
            }

            if (draggedSlot != null)
            {
                draggedSlot.style.borderTopWidth = 0;
                draggedSlot = null;
                draggedIndex = -1;
            }

            hoveredSlot = null;
        });
    }

    private void RegisterSlotEvents(VisualElement slot, int index)
    {
        slot.pickingMode = PickingMode.Position;

        slot.RegisterCallback<PointerDownEvent>(evt =>
        {
            Debug.Log($"슬롯 {index+1} 클릭됨");
            draggedSlot = slot;
            draggedIndex = index;

            // 인덱스 유효성 검사
            var slots = GameManager.Instance.inventory.slots;
            if (index >= 0 && index < slots.Count)
            {
                var itemData = slots[index].itemData;

                // 드래그 아이콘 표시
                if (itemData != null)
                {
                    DragIconController.Instance.Show(itemData.itemIcon);
                }
            }

            // 드래그 시작 시 시각적 피드백
            slot.style.borderTopColor = Color.yellow;
            slot.style.borderTopWidth = 2;
        });

        slot.RegisterCallback<PointerEnterEvent>(evt =>
        {
            hoveredSlot = slot;
            hoveredIndex = index;
            Debug.Log($"PointerEnter 발생: {slot.name}, index={index}");
        });

        slot.RegisterCallback<PointerLeaveEvent>(evt =>
        {
            if (hoveredSlot == slot)
            {
                hoveredSlot = null;
                hoveredIndex = -1;
            }
        });
    }

    public void Refresh()
    {
        var slots = GameManager.Instance.inventory.slots;

        for (int i = 0; i < slotElements.Count; i++) // 모든 UI 슬롯 요소를 순회하면서 UI를 갱신
        {
            if (i < slots.Count && slots[i] != null) // 슬롯이 존재하는 경우
            {
                var itemData = slots[i].itemData;

                if (itemData != null) // 아이템이 있는 경우
                {
                    slotElements[i].style.backgroundImage = itemData.itemIcon.texture; // 아이콘 표시
                    quantityLabels[i].text = $"{slots[i].quantity}/{itemData.maxStack}"; // 수량 표시
                    Debug.Log($"[Refresh] 슬롯 {i}: {itemData.itemName} ({slots[i].quantity}/{itemData.maxStack})");
                }
                else // 아이템이 없는 경우 (빈 슬롯)
                {
                    slotElements[i].style.backgroundImage = null; // 아이콘 제거
                    quantityLabels[i].text = ""; // 수량 제거
                    Debug.Log($"[Refresh] 슬롯 {i}: 빈 슬롯");
                }
            }
            else // 슬롯 자체가 없거나 범위를 벗어난 경우
            {
                slotElements[i].style.backgroundImage = null; // 아이콘 제거
                quantityLabels[i].text = ""; // 수량 제거
                Debug.Log($"[Refresh] 슬롯 {i}: 슬롯 없음");
            }
        }
        GameManager.Instance.equipmentManager.RefreshEquipItem(); // 장비창 UI도 함께 갱신
    }
}
