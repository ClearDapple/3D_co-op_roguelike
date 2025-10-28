using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Scriptable Objects/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public int itemID;            //아이템 ID
    public string itemName;       //이름
    public ItemType itemType;     //아이템 타입
    public GameObject itemPrefab; //프리팹
    public Sprite itemIcon;       //아이콘
    public int maxStack;          //최대 쌓을 수 있는 개수
    public string description;    //설명
}

public enum ItemType
{
    Normal,       //일반
    Importance,   //중요
    PermanentUse, //영구사용
    Rechargeable, //충전식
    Consumable,   //소비형
    Resources     //자원(재료)
}