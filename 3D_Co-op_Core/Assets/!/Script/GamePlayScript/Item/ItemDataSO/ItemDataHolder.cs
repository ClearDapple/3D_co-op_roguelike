using UnityEngine;

public class ItemDataHolder : MonoBehaviour
{
    public CommonItemDataSO itemData;
    public int currentStack;
    public ItemAbility itemAbility;


    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}

public enum ItemAbility
{
    None,
    InfiniteDurability, //무한 내구도
    ReducedConsumption, //소모값 감소
    IncreasedDamage,    //데미지 증가
    IncreasedRange,     //사거리 증가
}
