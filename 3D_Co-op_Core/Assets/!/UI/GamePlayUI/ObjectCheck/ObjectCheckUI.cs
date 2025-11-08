using UnityEngine;
using UnityEngine.UIElements;

public class ObjectCheckUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement ItemCheck, ItemImg;
    private Label ItemName;

    void Awake()
    {
        root = uiDocument.rootVisualElement;
        ItemCheck = root.Q<VisualElement>("ItemCheck");
        ItemImg = root.Q<VisualElement>("ItemImg");
        ItemName = root.Q<Label>("ItemName");
    }

    private void Start()
    {
        CloseItemCheckUI();
    }

    public void ItemCheckUI(ItemDataHolder item)
    {
        if (ItemCheck == null) return;
        ItemImg.style.backgroundImage = new StyleBackground(item.itemData.itemIcon);
        ItemName.text = $"{item.itemData.itemName} x{item.currentStack}";

        if (ItemCheck.style.visibility == Visibility.Visible) return;
        ItemCheck.style.visibility = Visibility.Visible;
    }

    public void CloseItemCheckUI()
    {
        if (ItemCheck == null || ItemCheck.style.visibility == Visibility.Hidden) return;
        ItemCheck.style.visibility = Visibility.Hidden;
    }
}
