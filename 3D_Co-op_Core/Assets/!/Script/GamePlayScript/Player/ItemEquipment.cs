using UnityEngine;

public class ItemEquipment : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    [SerializeField] Transform handTransform; // 아이템을 들 위치
    public int selectedSlotIndex; // 현재 선택된 슬롯 인덱스
    private GameObject currentItem; // 현재 손에 든 아이템


    private void Start()
    {
        selectedSlotIndex = 0;
    }

    public void GetEquipment(int index)
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        if (selectedSlotIndex == index || inventory.slots.Count <= index || index < 0)
        {
            selectedSlotIndex = -1;
        }
        else
        {
            selectedSlotIndex = index;
        }
        RefreshEquipItem();
    }

    public void RefreshEquipItem()
    {
        var index = selectedSlotIndex;

        if (inventory.slots == null || index >= inventory.slots.Count || index < 0)
        {
            Destroy(currentItem);
            currentItem = null;
            return;
        }

        var slots = inventory.slots[index];

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
