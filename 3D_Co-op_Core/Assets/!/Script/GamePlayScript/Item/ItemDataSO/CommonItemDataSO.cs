using UnityEngine;

public abstract class CommonItemDataSO : ScriptableObject
{
    [Header("Common Data")]
    public int itemID;             //아이템 ID
    public string itemName;        //이름
    public ItemType itemType;      //아이템 타입
    public GameObject itemPrefab;  //프리팹
    public Sprite itemIcon;        //아이콘
    public float itemPrice;        //가격
    public int itemMaxStack;       //최대 스택 수
    public string itemDescription; //설명
}
public enum ItemType
{
    Normal,       //일반, 내구도
    Rechargeable, //충전식, 자원소모
    Consumable,   //소비형, 투사체

    Importance,   //중요, 퀘스트 아이템
    Resources     //자원(재료)
}
