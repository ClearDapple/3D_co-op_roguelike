using UnityEngine;
using UnityEngine.UIElements;

public class InventorySlotUI
{
    private VisualElement slotElement;
    private Sprite itemIcon;

    public InventorySlotUI(VisualElement element, Sprite icon)
    {
        slotElement = element;
        itemIcon = icon;

        slotElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
        slotElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    void OnPointerDown(PointerDownEvent evt)
    {
        DragIconController.Instance.Show(itemIcon);
    }

    void OnPointerUp(PointerUpEvent evt)
    {
        DragIconController.Instance.Hide();
    }
}