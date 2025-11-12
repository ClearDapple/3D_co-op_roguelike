using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class ResourcesPointUI : MonoBehaviour
{
    [Header("UI - Document")]
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private Label PointNumber, AddPointNumber;

    [Header("Setting")]
    public int point;

    private string plusMinusSigns;
    private Coroutine addPointAnimation;


    void Awake()
    {
        root = uiDocument.rootVisualElement;
        PointNumber = root.Q<Label>("PointNumber");
        AddPointNumber = root.Q<Label>("AddPointNumber");
    }

    private void Start()
    {
        point = 0;
        PointNumber.text = $": {point}";
        AddPointNumber.text = null;
    }

    public void AddPoint(int addPoint)
    {
        if (addPointAnimation != null) StopCoroutine(addPointAnimation);

        if (addPoint >= 0)
        {
            AddPointNumber.style.color = Color.green;
            plusMinusSigns = "+";
        }
        else
        {
            AddPointNumber.style.color = Color.red;
            plusMinusSigns = "-";
        }

        if (point + addPoint < 0)
        {
            addPoint = -point;
            point = 0;
        }
        else point += addPoint;

        addPointAnimation = StartCoroutine(AddPointAnimation(Mathf.Abs(addPoint)));
    }

    IEnumerator AddPointAnimation(int addPoint)
    {
        PointNumber.text = $": {point}";
        AddPointNumber.text = $"{plusMinusSigns}{addPoint}";
        yield return new WaitForSeconds(1.5f);
        AddPointNumber.text = null;
    }
}
