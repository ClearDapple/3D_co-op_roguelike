using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    public LayerMask ItemLayer;
    GameObject hitItem;
    public float pickupRange = 3f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, ItemLayer))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (hit.collider.gameObject != hitItem || hitItem == null)
                {
                    hitItem = hit.collider.gameObject;
                    ItemDataHolder itemData = hitItem.GetComponent<ItemDataHolder>();
                    if (itemData != null)
                    {
                        UIManager.Instance.objectCheckUI.ItemCheckUI(itemData);
                    }
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp(hit.collider.gameObject);
                }
            }
        }
        else if (hitItem != null)
        {
            hitItem = null;
            UIManager.Instance.objectCheckUI.CloseItemCheckUI();
        }
    }

    void PickUp(GameObject itemObject)
    {
        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();
        if (holder == null || holder.itemData == null)
        {
            Debug.Log("아이템 데이터가 없습니다.");
            return;
        }

        int remainItemStack = inventory.TryAddItem(holder.itemData, holder.currentStack);
        if (remainItemStack <= 0)
        {
            Debug.Log("{holder.itemData} 아이템 모두 획득함.");
            holder.DestorySelf();
        }
        else
        {
            if (remainItemStack == holder.currentStack)
            {
                Debug.Log("{holder.itemData} 아이템 획득 불가능. 인벤토리 공간이 부족합니다.");
                UIManager.Instance.systemMessageUI.ShowMessage("공간이 부족합니다!");
            }
            else
            {
                Debug.Log($"{holder.itemData} 아이템 {holder.currentStack - remainItemStack} 획득함. 남은 개수: {remainItemStack}");
                holder.itemData.itemStack = remainItemStack;
            }
        }
        UIManager.Instance.inventoryUI.Refresh();
    }
}