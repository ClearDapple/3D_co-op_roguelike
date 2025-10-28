using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //inventoryUI
    public InventoryUI inventoryUI { get; private set; }
    public DragIconController dragIconController { get; private set; }
    public InventorySlotUI inventorySlotUI { get; private set; }

    //objectCheckUI
    public ObjectCheckUI objectCheckUI { get; private set; }

    //systemMessageUI
    public SystemMessageUI systemMessageUI { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        inventoryUI = GetComponentInChildren<InventoryUI>(true);
        objectCheckUI = GetComponentInChildren<ObjectCheckUI>(true);
        systemMessageUI = GetComponentInChildren<SystemMessageUI>(true);
    }
}