using UnityEngine;
using UnityEngine.UIElements;

public class DragIconController : MonoBehaviour
{
    public static DragIconController instance;

    private UIDocument uiDocument;
    private VisualElement dragIcon;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        dragIcon = root.Q<VisualElement>("DragIcon");
        dragIcon.style.display = DisplayStyle.None;
        dragIcon.pickingMode = PickingMode.Ignore;

        root.RegisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    void OnPointerMove(PointerMoveEvent evt)
    {
        if (dragIcon == null) return;

        if (dragIcon.style.display == DisplayStyle.Flex)
        {
            dragIcon.style.left = evt.position.x - 32;
            dragIcon.style.top = evt.position.y - 32;
        }
    }

    public void Show(Sprite icon)
    {
        if (dragIcon == null) return;
        dragIcon.style.backgroundImage = new StyleBackground(icon.texture);
        dragIcon.style.display = DisplayStyle.Flex;
    }

    public void Hide()
    {
        if(dragIcon == null) return;
        dragIcon.style.display = DisplayStyle.None;
    }
}