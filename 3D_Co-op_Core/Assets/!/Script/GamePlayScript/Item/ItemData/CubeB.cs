using UnityEngine;

[CreateAssetMenu(fileName = "CubeB", menuName = "Scriptable Objects/CubeB")]
public class CubeB : ItemDataSO
{
    public override void ItemUse(GameObject player, ItemDataHolder myHolder)
    {
        //장전 여부 체크
        if (myHolder.currentReloadCount <= 0)
        {
            Debug.Log($"{itemName} 아이템 충전량 부족 ({myHolder.currentReloadCount}/{maxReloadCount})");
        }
        Debug.Log($"{itemName} 아이템 사용");


        //애니메이션
        Animator animator = player.GetComponentInChildren<Animator>();
        animator.SetTrigger("Attack");


        // 공격
        Camera camera = player.GetComponentInChildren<Camera>();
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * itemDistance, Color.blue, 2f, true);

        int appliedDamage = itemPower;

        if (Physics.Raycast(ray, out hit, itemPower, targetLayer)) //충돌 감지 확인
        {
            GameObject target = hit.collider.gameObject;
            if (!target.activeInHierarchy) return;
            
            if (target.CompareTag("WeakPoint")) appliedDamage *= 2;

            EnemyDataHolder holder = target.GetComponent<EnemyDataHolder>();
            if (holder != null)
            {
                holder.TakeDamage(appliedDamage);
                myHolder.currentReloadCount = Mathf.Clamp(myHolder.currentReloadCount--, 0, maxReloadCount);
                player.GetComponentInChildren<ItemUse>()?.rechargeItem();
            }
            else
            {
                holder = target.transform.parent.GetComponent<EnemyDataHolder>();
                if (holder != null)
                {
                    holder.TakeDamage(appliedDamage);
                    myHolder.currentReloadCount = Mathf.Clamp(myHolder.currentReloadCount--, 0, maxReloadCount);
                    player.GetComponentInChildren<ItemUse>()?.rechargeItem();
                }
            }
        }
    }
}
