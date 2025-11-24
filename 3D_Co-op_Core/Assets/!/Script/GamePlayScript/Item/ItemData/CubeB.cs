using UnityEngine;

[CreateAssetMenu(fileName = "CubeB", menuName = "Scriptable Objects/CubeB")]
public class CubeB : rechargeItemDataSO
{
    [Header("UseableItemDataSO - CubeB")]
    public float itemRange;
    public int itemDamage;
    public LayerMask enemyLayer;


    public void ItemUse(GameObject player)
    {
        Debug.Log($"{itemName} 아이템 사용");

        //애니메이션
        Animator animator = player.GetComponentInChildren<Animator>();
        animator.SetTrigger("Attack");


        // 공격
        Camera camera = player.GetComponentInChildren<Camera>();
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * itemRange, Color.blue, 3f, true);

        int appliedDamage = itemDamage;

        if (Physics.Raycast(ray, out hit, itemRange, enemyLayer)) //충돌 감지 확인
        {
            GameObject target = hit.collider.gameObject;
            if (!target.activeInHierarchy) return;
            
            if (target.CompareTag("WeakPoint")) appliedDamage *= 2;

            EnemyDataHolder holder = target.GetComponent<EnemyDataHolder>();
            if (holder != null)
            {
                holder.TakeDamage(appliedDamage);
            }
            else
            {
                holder = target.transform.parent.GetComponent<EnemyDataHolder>();
                if (holder != null)
                {
                    holder.TakeDamage(appliedDamage);
                }
            }
        }
    }
}
