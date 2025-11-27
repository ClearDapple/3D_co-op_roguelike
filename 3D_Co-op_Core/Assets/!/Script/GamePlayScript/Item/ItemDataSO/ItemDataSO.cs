using UnityEngine;

public enum ItemType
{
    ImportanceItem,
    rechargeItem,
    DurabilityItem,
    ConsumeItem
}

public class ItemDataSO : ScriptableObject
{
    //공통 아이템 데이터
    [Header("Common Item Data(공통)")]
    public string itemName;        //이름
    public ItemType itemType;      //아이템 타입
    public GameObject itemPrefab;  //프리팹
    public Sprite itemIcon;        //아이콘
    public float itemPrice;        //가격
    public int itemMaxStack;       //최대 스택 수
    public string itemDescription; //설명

    //사용 아이템 전용
    [Header("Item Use Data(사용)")]
    public int itemPower;      //아이템 수치
    public float itemDistance; //사거리
    public LayerMask targetLayer; //타겟 레이어 마스크
    public virtual void ItemUse(GameObject player, ItemDataHolder holder) { }

    //충전형 아이템 전용
    [Header("recharge Item Data(충전)")]
    public int maxReloadCount; //최대 장전 수
    public bool redResources;    //R자원 사용 여부
    public bool greenResources;  //G자원 사용 여부
    public bool blueResources;   //B자원 사용 여부

    //내구도 아이템 전용
    [Header("Durability Item Data(내구도)")]
    public int maxDurability; //내구도
}