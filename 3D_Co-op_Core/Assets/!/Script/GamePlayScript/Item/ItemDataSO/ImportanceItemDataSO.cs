using UnityEngine;

public class ImportanceItemDataSO : ScriptableObject
{
    [Header("Importance Data")] //공통

    public string itemName;        //이름
    public GameObject itemPrefab;  //프리팹
    public Sprite itemIcon;        //아이콘
    public float itemPrice;        //가격
    public int itemMaxStack;       //최대 스택 수
    public string itemDescription; //설명
}

public class rechargeItemDataSO : ImportanceItemDataSO
{
    [Header("recharge Data")] //충전형 아이템

    public int itemPower;      //아이템 수치
    public float itemDistance; //사거리

    public int maxReloadCount;   //최대 장전 수

    public bool redResources;    //R자원 사용 여부
    public bool greenResources;  //G자원 사용 여부
    public bool blueResources;   //B자원 사용 여부

    public IItemEffect effect; // 인터페이스 참조
    public void Use(GameObject player)
    {
        effect.Apply(player); // 여기서 효과 실행
    }

public class DurabilityItemDataSO : ImportanceItemDataSO
{
    [Header("Durability Data")] //내구도 아이템

    public int itemPower;      //아이템 수치
    public float itemDistance; //사거리

    public int maxDurability;    //내구도
}

public class ConsumeItemDataSO : ImportanceItemDataSO
{
    [Header("Consume Data")] //소모형 아이템

    public int itemPower;      //아이템 수치
    public float itemDistance; //사거리
}