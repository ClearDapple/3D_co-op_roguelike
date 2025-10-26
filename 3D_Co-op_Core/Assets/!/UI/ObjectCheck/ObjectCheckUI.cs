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
        ItemCheck.style.visibility = Visibility.Visible;
        ItemImg.style.backgroundImage = new StyleBackground(item.itemData.itemIcon);
        ItemName.text = $"{item.itemData.itemName} x{item.currentStack}";
    }

    public void CloseItemCheckUI()
    {
        ItemCheck.style.visibility = Visibility.Hidden;
    }
}
