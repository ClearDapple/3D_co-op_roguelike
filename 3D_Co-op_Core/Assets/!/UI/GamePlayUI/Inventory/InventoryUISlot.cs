using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUISlot
{
    private VisualElement slotElement;
    private Sprite itemIcon;


    public InventoryUISlot(VisualElement element, Sprite icon)
    {
        slotElement = element;
        itemIcon = icon;

        slotElement.RegisterCallback<PointerDownEvent>(OnPointerDown);
        slotElement.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    void OnPointerDown(PointerDownEvent evt)
    {
        DragIconController.instance.Show(itemIcon);
    }

    void OnPointerUp(PointerUpEvent evt)
    {
        DragIconController.instance.Hide();
    }
}