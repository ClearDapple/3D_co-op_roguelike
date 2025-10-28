using UnityEngine;

public class ItemDataHolder : MonoBehaviour
{
    public ItemDataSO itemData;
    public int currentStack;

    public void CurrentStackCheck(int stack)
    {
        currentStack = stack;
    }

    public void DestorySelf()
    {
        Destroy(this.gameObject);
    }
}
