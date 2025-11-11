using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private Inventory inventory;
    private InventoryUI inventoryUI;
    private ObjectCheckUI objectCheckUI;
    private SystemMessageUI systemMessageUI;

    [SerializeField] Transform cameraTransform;

    public float interactionRange = 3f; // 상호작용 가능 거리
    public LayerMask playerLayer, itemLayer, facilityLayer; // 참고 레이어

    private int allLayerMask; // 플레이어 제외 레이어
    private GameObject hitObject; // 레이캐스트 적중 오브젝트


    private void Start()
    {
        inventory = GetComponentInChildren<Inventory>();
        inventoryUI = GetComponentInChildren<InventoryUI>();
        objectCheckUI = GetComponentInChildren<ObjectCheckUI>();
        systemMessageUI = GetComponentInChildren<SystemMessageUI>();
        allLayerMask = ~playerLayer.value; // 플레이어 레이어 제외
    }

    private void Update()
    {
        GetInteractableRaycast();
    }

    void GetInteractableRaycast()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green);

        if (Physics.Raycast(ray, out hit, interactionRange, allLayerMask)) //충돌했는지 확인
        {
            #region 아이템 레이어
            if (itemLayer.Contains(hit.collider.gameObject.layer)) //레이어 확인
            {
                if (hit.collider.gameObject.activeInHierarchy) //오브젝트 활성화 여부 확인
                {
                    if (hitObject == null || hit.collider.gameObject != hitObject) //새로운 오브젝트에 레이캐스트 적중
                    {
                        hitObject = hit.collider.gameObject;
                        ItemDataHolder holder = hitObject.GetComponent<ItemDataHolder>();
                        if (holder != null)
                        {
                            objectCheckUI.ItemCheckUI(holder);
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.E)) //상호작용
                    {
                        PickUp(hit.collider.gameObject);
                    }
                    return;
                }
            }
            #endregion

    //수정중
            #region 시설 레이어
            if (facilityLayer.Contains(hit.collider.gameObject.layer)) //레이어 확인
            {
                if (hit.collider.gameObject.activeInHierarchy) //오브젝트 활성화 여부 확인
                {
                    if (hitObject == null || hit.collider.gameObject != hitObject) //새로운 오브젝트에 레이캐스트 적중
                    {
                        hitObject = hit.collider.gameObject;
                        FacilityDataHolder holder = hitObject.GetComponent<FacilityDataHolder>();
                        if (holder != null)
                        {
                            //objectCheckUI.ItemCheckUI(holder);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.E)) //상호작용
                    {
                        //PickUp(hit.collider.gameObject);
                    }
                    return;
                }
            }
            #endregion
        }
        if (hitObject != null) hitObject = null;
        objectCheckUI.CloseObjectCheckUI();
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

        if (remainItemStack <= 0) //음수 또는 0 이면 모두 획득
        {
            Debug.Log($"[성공] {holder.itemData.itemName} 아이템 {holder.currentStack} 모두 획득함. 남은 개수 없음.");
            holder.DestorySelf();
        }
        else if (remainItemStack == holder.currentStack) //남은 개수가 현재 스택과 같으면 하나도 못얻음
        {
            Debug.Log($"[실패] {holder.itemData.itemName} x {holder.currentStack}: 아이템 획득 불가능. 빈 공간 없음.");
            systemMessageUI.ShowMessage("공간이 부족합니다!");
        }
        else //일부만 획득
        {
            Debug.Log($"{holder.itemData.itemName} 아이템 {holder.currentStack - remainItemStack} 획득함. 남은 개수: {remainItemStack}.");
            holder.currentStack = remainItemStack;
            objectCheckUI.ItemCheckUI(holder);//UI 갱신
        }

        inventoryUI.RefreshSlotUI();
    }
}