using NUnit.Framework.Interfaces;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public LayerMask interactableLayer;
    GameObject hitObject;
    public float interactionRange = 3f;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            if (interactableLayer.Contains(hit.collider.gameObject.layer)) //레이어가 상호작용 레이어인지 확인
            {
                if (hit.collider.gameObject.activeInHierarchy) //오브젝트가 활성화 되어있는지 확인
                {
                    if (hit.collider.CompareTag("Item")) //태그가 아이템인지 확인
                    {
                        if (hitObject == null || hit.collider.gameObject != hitObject) //새로운 오브젝트에 레이캐스트가 맞은경우
                        {
                            hitObject = hit.collider.gameObject;
                            ItemDataHolder itemData = hitObject.GetComponent<ItemDataHolder>();
                            if (itemData != null)
                            {
                                UIManager.Instance.objectCheckUI.ItemCheckUI(itemData);
                            }
                        }
                        if (hitObject != null && Input.GetKeyDown(KeyCode.E)) //E키를 눌렀을때
                        {
                            PickUp(hit.collider.gameObject);
                        }
                    }
                    else { ClearInteraction(); } //태그가 아이템이 아닌경우
                }
                else { ClearInteraction(); } //오브젝트가 비활성화 되어있는경우
            }
            else { ClearInteraction(); } //레이어가 상호작용 레이어가 아닌경우
        }
        else { ClearInteraction(); } //레이캐스트에 맞지 않은경우
    }

    void ClearInteraction()
    {
        hitObject = null;
        UIManager.Instance.objectCheckUI.CloseItemCheckUI();
    }


    void PickUp(GameObject itemObject)
    {
        ItemDataHolder holder = itemObject.GetComponent<ItemDataHolder>();

        if (holder == null || holder.itemData == null)
        {
            Debug.Log("아이템 데이터가 없습니다.");
            return;
        }

        int remainItemStack = GameManager.Instance.inventory.TryAddItem(holder.itemData, holder.currentStack);

        if (remainItemStack <= 0) //음수 또는 0 이면 모두 획득
        {
            Debug.Log($"[성공] {holder.itemData.itemName} 아이템 {holder.currentStack} 모두 획득함. 남은 개수 없음.");
            holder.DestorySelf();
        }
        else if (remainItemStack == holder.currentStack) //남은 개수가 현재 스택과 같으면 하나도 못얻음
        {
            Debug.Log($"[실패] {holder.itemData.itemName} x {holder.currentStack}: 아이템 획득 불가능. 빈 공간 없음.");
            UIManager.Instance.systemMessageUI.ShowMessage("공간이 부족합니다!");
        }
        else //일부만 획득
        {
            Debug.Log($"{holder.itemData.itemName} 아이템 {holder.currentStack - remainItemStack} 획득함. 남은 개수: {remainItemStack}.");
            holder.currentStack = remainItemStack;
            UIManager.Instance.objectCheckUI.ItemCheckUI(holder);//UI 갱신
        }

        UIManager.Instance.inventoryUI.Refresh();
    }
}