using UnityEngine;

public class ItemDataHolder : MonoBehaviour
{
    public ImportanceItemDataSO itemData;
    public 
    public int currentStack;


    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}