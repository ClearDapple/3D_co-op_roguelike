using UnityEngine;

[CreateAssetMenu(fileName = "FacilityDataSO", menuName = "Scriptable Objects/FacilityDataSO")]
public class FacilityDataSO : ScriptableObject
{
    public string facilityName;        //시설 이름
    public FacilityType facilityType;  //시설 타입
    public Sprite facilityIcon;        //시설 아이콘
    public string facilityDescription; //시설 설명
}

public enum FacilityType
{
    Store,  //상점
    Button, //버튼
}
