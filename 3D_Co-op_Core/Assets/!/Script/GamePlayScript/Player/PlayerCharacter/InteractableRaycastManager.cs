using UnityEngine;

public class InteractableRaycastManager : MonoBehaviour
{
    [Header("Modules")]
    [SerializeField] InteractionObjectManager interactionObjectManager;

    [Header("UI")]
    [SerializeField] ObjectCheckUI objectCheckUI;

    [Header("Camera")]
    [SerializeField] Transform cameraTransform;

    [Header("Setting")]
    public float interactionRange = 3f; // 상호작용 가능 거리
    public LayerMask playerLayer, itemLayer, facilityLayer; // 참고 레이어
    public GameObject hitObject; // 레이캐스트 적중 오브젝트

    private int allLayerMask; // 플레이어 제외 레이어


    private void Start()
    {
        allLayerMask = ~playerLayer.value; // 플레이어 레이어 제외
    }

    private void Update()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green);

        if (Physics.Raycast(ray, out hit, interactionRange, allLayerMask)) //충돌 감지 확인
        {
            GameObject target = hit.collider.gameObject;
            if (!target.activeInHierarchy) return;
            int targetLayer = target.layer;

            // 아이템 상호작용
            if (itemLayer.Contains(targetLayer))
            {
                interactionObjectManager.HandleItemInteraction(target);
                return;
            }

            // 시설 상호작용
            if (facilityLayer.Contains(targetLayer))
            {
                interactionObjectManager.HandleFacilityInteraction(target);
                return;
            }

        }

        //충돌 감지 실패
        if (hitObject != null) hitObject = null;
        objectCheckUI.CloseObjectCheckUI();
    }
}
