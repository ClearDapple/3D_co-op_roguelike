using UnityEngine;
using UnityEngine.UIElements;

public class TestUI : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement Test;
    public Color Color;

    void Awake()
    {
        root = uiDocument.rootVisualElement;
        Test = root.Q<VisualElement>("Test");
    }

    public void ChangeColor(Color color)
    {
        Test.style.backgroundColor = color;
        Color = color;
    }
}
