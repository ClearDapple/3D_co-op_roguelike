using UnityEngine;
using UnityEngine.UIElements;

public class ObjectCheckUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement CheckContainer, ObjectImg;
    private Label ObjectName;

    void Awake()
    {
        root = uiDocument.rootVisualElement;
        CheckContainer = root.Q<VisualElement>("CheckContainer");//ObjectCheckContainer
        ObjectImg = root.Q<VisualElement>("ObjectImg");
        ObjectName = root.Q<Label>("ObjectName");
    }

    private void Start()
    {
        CloseObjectCheckUI();
    }

    public void ItemCheckUI(ItemDataHolder itemHolder)
    {
        if (CheckContainer == null) return;
        ObjectImg.style.backgroundImage = new StyleBackground(itemHolder.itemData.itemIcon);
        ObjectName.text = $"{itemHolder.itemData.itemName} x{itemHolder.currentStack}";

        if (CheckContainer.style.visibility == Visibility.Visible) return;
        CheckContainer.style.visibility = Visibility.Visible;
    }
    //¼öÁ¤Áß
    //public void FacilityCheckUI(FacilityDataHolder FacilityHolder)
    //{
    //    if (CheckContainer == null) return;
    //    ObjectImg.style.backgroundImage = new StyleBackground(FacilityHolder.itemData.itemIcon);
    //    ObjectName.text = $"{FacilityHolder.itemData.itemName} x{FacilityHolder.currentStack}";

    //    if (CheckContainer.style.visibility == Visibility.Visible) return;
    //    CheckContainer.style.visibility = Visibility.Visible;
    //}

    public void CloseObjectCheckUI()
    {
        if (CheckContainer == null || CheckContainer.style.visibility == Visibility.Hidden) return;
        CheckContainer.style.visibility = Visibility.Hidden;
    }
}
