using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SystemMessageUI : MonoBehaviour
{
    [Header("UI - Document")]
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement ToastMessage;
    private Label Message;

    [Header("Setting")]
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float showDuration = 1.5f;

    private Coroutine currentRoutine;

    void Awake()
    {
        root = uiDocument.rootVisualElement;
        ToastMessage = root.Q<VisualElement>("ToastMessage");
        Message = root.Q<Label>("Message");
    }

    private void Start()
    {
        root.pickingMode = PickingMode.Ignore;
        ToastMessage.style.opacity = 0;
        ToastMessage.style.display = DisplayStyle.None;
    }

    public void ShowMessage(string text)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        currentRoutine = StartCoroutine(ShowRoutine(text));
    }

    private IEnumerator ShowRoutine(string text)
    {
        Message.text = text;
        ToastMessage.style.display = DisplayStyle.Flex;

        // Fade In
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            ToastMessage.style.opacity = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        yield return new WaitForSeconds(showDuration);

        // Fade Out
        t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            ToastMessage.style.opacity = Mathf.Lerp(1, 0, t / fadeDuration);
            yield return null;
        }

        ToastMessage.style.display = DisplayStyle.None;
        currentRoutine = null;
    }
}
