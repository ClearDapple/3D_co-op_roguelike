using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

    public Inventory inventory;


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
    }

    void RegisterSlotEvents(VisualElement slot, int index)
    {
        slot.RegisterCallback<PointerDownEvent>(evt =>
        {
            draggedSlot = slot;
            draggedIndex = index;
            slot.style.borderTopColor = Color.yellow;
            slot.style.borderTopWidth = 2;
        });

        slot.RegisterCallback<PointerUpEvent>(evt =>
        {
            if (draggedSlot != null && draggedSlot != slot)
            {
                inventory.SwapSlots(draggedIndex, index);
                Refresh();
            }

            // √ ±‚»≠
            draggedSlot.style.borderTopWidth = 0;
            draggedSlot = null;
            draggedIndex = -1;
        });
    }

public void Refresh()
{
    for (int i = 0; i < slotElements.Count; i++)
    {
        if (i < inventory.slots.Count && inventory.slots[i].itemData != null)
        {
            slotElements[i].style.backgroundImage = inventory.slots[i].itemData.itemIcon.texture;
            quantityLabels[i].text = $"{inventory.slots[i].quantity}/{inventory.slots[i].itemData.maxStack}";
        }
        else
        {
            slotElements[i].style.backgroundImage = null;
            quantityLabels[i].text = "";
        }
    }
}
}