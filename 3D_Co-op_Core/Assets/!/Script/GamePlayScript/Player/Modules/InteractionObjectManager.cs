using UnityEngine;

public class InteractionObjectManager : MonoBehaviour
{
    [Header("PlayerCharacter")]
    [SerializeField] InteractableRaycastManager interactableRaycastManager;

    [Header("Modules")]
    [SerializeField] ItemPickUp itemPickUp;

    [Header("UI")]
    [SerializeField] ObjectCheckUI objectCheckUI;


    public void HandleItemInteraction(GameObject target) //아이템 상호작용
    {
        if (interactableRaycastManager.hitObject == null || target != interactableRaycastManager.hitObject)
        {
            interactableRaycastManager.hitObject = target;
            ItemDataHolder holder = target.GetComponent<ItemDataHolder>();
            if (holder != null)
            {
                objectCheckUI.ItemCheckUI(holder); //아이템 UI 표시
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) //아이템 획득
        {
            itemPickUp.PickUp(target);
        }
    }

    public void HandleFacilityInteraction(GameObject target) //시설 상호작용
    {
        if (interactableRaycastManager.hitObject == null || target != interactableRaycastManager.hitObject)
        {
            interactableRaycastManager.hitObject = target;
            FacilityDataHolder holder = target.GetComponent<FacilityDataHolder>();
            if (holder != null)
            {
                objectCheckUI.FacilityCheckUI(holder); // 시설 UI 표시 (직접 구현 필요)
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            FacilityDataHolder holder = target.GetComponent<FacilityDataHolder>();
            if (holder != null)
            {
                holder.InteractWithFacility(target); // 시설 작동
            }
        }
    }

}
