using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [SerializeField] Transform handTransform; // 아이템을 들 위치
    private GameObject currentItem;           // 현재 손에 든 아이템
    private int selectedSlotIndex = 0;    // 현재 선택된 슬롯 인덱스

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))// 1번 키 입력 감지
        {
            selectedSlotIndex = 0;
            RefreshEquipItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))// 2번 키 입력 감지
        {
            selectedSlotIndex = 1;
            RefreshEquipItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))// 3번 키 입력 감지
        {
            selectedSlotIndex = 2;
            RefreshEquipItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))// 4번 키 입력 감지
        {
            selectedSlotIndex = 3;
            RefreshEquipItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))// 5번 키 입력 감지
        {
            selectedSlotIndex = 4;
            RefreshEquipItem();
        }
    }

    public void RefreshEquipItem()
    {
        var inventory = GameManager.Instance?.inventory;

        if (inventory == null || inventory.slots == null || selectedSlotIndex >= inventory.slots.Count)
        {
            Destroy(currentItem);
            currentItem = null;
            return;
        }

        var slots = inventory.slots[selectedSlotIndex];

        // 슬롯이 null이거나 아이템 데이터가 없으면 제거 후 종료
        if (slots == null || slots.itemData == null || slots.itemData.itemPrefab == null)
        {
            if (currentItem != null)
            {
                Destroy(currentItem);
                currentItem = null;
            }
            return;
        }

        // 현재 아이템이 존재할 경우 중복 장착 방지
        if (currentItem != null)
        {
            ItemDataHolder currentItemData = currentItem.GetComponent<ItemDataHolder>();

            // currentItemData가 없거나 itemID가 다르면 제거
            if (currentItemData == null || currentItemData.itemData == null || currentItemData.itemData.itemID != slots.itemData.itemID)
            {
                Destroy(currentItem);
                currentItem = null;
            }
            else return; // 같은 아이템이면 다시 장착하지 않음
        }

        // 아이템 인스턴스 생성 후 손에 장착
        currentItem = Instantiate(slots.itemData.itemPrefab, handTransform);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;

        // 중력 비활성화
        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true; // 물리 계산도 비활성화
        }

        // 충돌 비활성화
        Collider col = currentItem.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
    }
}
