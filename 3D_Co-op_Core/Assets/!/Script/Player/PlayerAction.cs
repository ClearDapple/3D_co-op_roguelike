using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Inventory inventory;

    public LayerMask ItemLayer;
    //public Transform holdPoint;
    public float pickupRange = 3f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange, ItemLayer))
            {
                if (hit.collider.CompareTag("Item"))
                {
                    PickUp(hit.collider.gameObject);
                }
            }
        }
    }

    void PickUp(GameObject itemObject)
    {
        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();
        if (holder != null)
        {
            bool success = inventory.TryAddItem(holder.itemData);
            if (success)
            {
                Destroy(itemObject); // 성공 시 오브젝트 제거
            }
            else
            {
                Debug.Log("아이템을 줍지 못했습니다.");
            }
        }
    }
}