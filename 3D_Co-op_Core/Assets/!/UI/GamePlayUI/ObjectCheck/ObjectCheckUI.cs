using UnityEngine;
using UnityEngine.UIElements;

public class ObjectCheckUI : MonoBehaviour
{
    [Header("UI - Document")]
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

    public void CloseObjectCheckUI()
    {
        if (CheckContainer == null || CheckContainer.style.visibility == Visibility.Hidden) return;
        CheckContainer.style.visibility = Visibility.Hidden;
    }

    private void OpenObjectCheckUI()
    {
        if (CheckContainer == null || CheckContainer.style.visibility == Visibility.Visible) return;
        CheckContainer.style.visibility = Visibility.Visible;
    }

    public void ItemCheckUI(ItemDataHolder itemHolder) //아이템 UI 표시
    {
        if (CheckContainer == null) return;
        ObjectImg.style.backgroundImage = new StyleBackground(itemHolder.itemData.itemIcon);
        ObjectName.text = $"{itemHolder.itemData.itemName} x{itemHolder.currentStack}";
        OpenObjectCheckUI();
    }

    public void FacilityCheckUI(FacilityDataHolder FacilityHolder) //시설 UI 표시
    {
        if (CheckContainer == null) return;
        ObjectImg.style.backgroundImage = new StyleBackground(FacilityHolder.facilityData.facilityIcon);
        ObjectName.text = $"{FacilityHolder.facilityData.facilityName} - [E키를 눌러 상호작용하기]";
        OpenObjectCheckUI();
    }

}
