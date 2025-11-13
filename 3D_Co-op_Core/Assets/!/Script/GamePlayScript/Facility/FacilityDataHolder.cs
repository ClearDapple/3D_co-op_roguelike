using UnityEngine;

public class FacilityDataHolder : MonoBehaviour
{
    public FacilityDataSO facilityData;


    public void InteractWithFacility(GameObject facility)
    {
        if (facilityData == null) return;

        switch (facilityData.facilityType)
        {
            case FacilityType.Store:
                Debug.Log("스토어 작동!");
                facility.GetComponent<Store>()?.GetStore();
                break;

            case FacilityType.Button:
                Debug.Log("버튼 작동!");
                //facility.GetComponent<Button>()?.GetButton();
                break;

            default:
                Debug.Log("알 수 없는 시설 타입");
                break;
        }
    }
}
