using UnityEngine;

public class ItemDataHolder : MonoBehaviour
{
    public ItemDataSO itemData;
    public int currentStack;
    public ItemAbility itemAbility;


    public void CurrentStackCheck(int stack)
    {
        currentStack = stack;
    }

    public void DestorySelf()
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
