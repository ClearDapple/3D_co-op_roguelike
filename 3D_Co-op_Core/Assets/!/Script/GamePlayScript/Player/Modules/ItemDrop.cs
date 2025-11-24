using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField] ItemEquipment itemEquipment;

    [Header("UI")]
    [SerializeField] Inventory inventory;
    [SerializeField] InventoryUI inventoryUI;

    [Header("Camera - Drop Point")]
    [SerializeField] Transform dropPoint;  // 아이템 드롭 기본 위치

    [Header("World - Item")]
    [SerializeField] Transform dropParent; // 드롭될 아이템의 부모 오브젝트

    [Header("Setting")]
    public float maxDistance = 3.0f;       // 아이템 드롭 거리
    public float dropOffset = 0.25f;       // 아이템 드롭 오프셋
    public LayerMask playerLayer;          // 충돌 검사용 레이어 (플레이어만 포함)

    private int allLayerMask; // 플레이어 레이어 제외한 모든 레이어 마스크
    private GameObject dropObject;


    private void Start()
    {
        allLayerMask = ~playerLayer.value; // 플레이어 레이어 제외
    }

    public void GetDropItem()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        var index = itemEquipment.selectedSlotIndex;
        if (inventory.slots.Count <=index || index < 0) return;
        if (inventory.slots[index] == null) return;

        var slot = inventory.slots[index];
        if (slot.itemData == null || slot.quantity <= 0)
        {
            Debug.Log($"[실패] {index + 1}번 아이템 드롭 불가능.");
            return;
        }
        var itemData = slot.itemData;
        var quantity = slot.quantity;
        DropItem(itemData, quantity);

        itemEquipment.GetEquipment(-1);

        inventory.ClearSlot(index);
        inventoryUI.RefreshSlotUI();
    }

    public void DropItem(ImportanceItemDataSO itemData, int quantity)
    {
        Vector3 dropPosition; // 아이템 드롭 위치

        Ray ray = new Ray(dropPoint.position, dropPoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, allLayerMask))
        {
            // 충돌한 경우: 충돌 지점에서 약간 떨어진 위치에 생성
            dropPosition = hit.point + hit.normal * dropOffset;
        }
        else
        {
            // 충돌하지 않은 경우: 최대 거리 위치에 생성
            dropPosition = dropPoint.position + dropPoint.forward * maxDistance;
        }

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);

        dropObject = itemData.itemPrefab;
        Vector3 flatForward = Vector3.ProjectOnPlane(dropPoint.forward, Vector3.up);
        Quaternion rotation = Quaternion.LookRotation(flatForward);
        dropObject = Instantiate(dropObject, dropPosition, rotation, dropParent);

        var itemDataHolder = dropObject.GetComponent<ItemDataHolder>();
        if (itemDataHolder != null)
        {
            itemDataHolder.currentStack = quantity;
            Debug.Log($"[성공] {itemData.itemName} x{quantity} 아이템 드롭됨.");
        }
        else
        {
            Debug.LogError("[실패] ItemDataHolder 컴포넌트가 없습니다.");
        }
    }
}