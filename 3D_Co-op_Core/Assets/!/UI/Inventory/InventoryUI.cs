using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement slot1, slot2, slot3, slot4, slot5;


    void Awake()
    {
        root = uiDocument.rootVisualElement;
        slot1 = root.Q<VisualElement>("Slot1");
        slot2 = root.Q<VisualElement>("Slot2");
        slot3 = root.Q<VisualElement>("Slot3");
        slot4 = root.Q<VisualElement>("Slot4");
        slot5 = root.Q<VisualElement>("Slot5");
    }
}
