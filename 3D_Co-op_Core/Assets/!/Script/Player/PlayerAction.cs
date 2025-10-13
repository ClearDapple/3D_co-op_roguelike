using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] TestUI testUI;
    public LayerMask ItemLayer;
    public float pickupRange = 3f;


    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, ItemLayer))
        {
            if (hit.collider.CompareTag("Item"))
            {
                if (testUI.Color!= Color.red) testUI.ChangeColor(Color.red); // test
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp(hit.collider.gameObject);
                }
            }
        }
        else if (testUI.Color != Color.black) testUI.ChangeColor(Color.black); // test
    }

    void PickUp(GameObject itemObject)
    {
        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();
        if (holder == null || holder.itemData == null)
        {
            Debug.Log("아이템 데이터가 없습니다.");
            return;
        }

        bool success = inventory.TryAddItem(holder.itemData);
        if (success)
        {
            Destroy(itemObject); // 성공 시 오브젝트 제거
        }
        else
        {
            Debug.Log("인벤토리 공간이 부족합니다.");
        }
    }
}