using UnityEngine;

[CreateAssetMenu(fileName = "CubeR", menuName = "Scriptable Objects/CubeR")]
public class CubeR : ItemDataSO
{
    private Vector3 attackCenter;


    public override void ItemUse(GameObject player, ItemDataHolder myHolder)
    {
        Debug.Log($"{itemName} 아이템 사용");


        // 애니메이션
        Animator animator = player.GetComponentInChildren<Animator>();
        animator.SetTrigger("Attack");


        // 공격
        Camera cameraObject = player.GetComponentInChildren<Camera>();
        Transform camera = cameraObject.transform;

        Vector3 attackCenter = camera.position + camera.forward * 1f;

        int hitCount = 0;

        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, itemDistance, targetLayer);

        Debug.DrawRay(camera.position, camera.forward * itemDistance, Color.red, 2f);

        foreach (var hitCollider in hitColliders)
        {
            EnemyDataHolder enemy = hitCollider.GetComponent<EnemyDataHolder>();
            if (enemy != null)
            {
                hitCount++;
            }
        }

        if (hitCount > 0)
        {
            int appliedDamage = itemPower;
            string damageLog = "";
            string durabilityLog = "";

            if (hitCount >= myHolder.currentDurability) //명중 인원이 남은 내구도보다 많을 때 -> 대미지 2배 & 무기 파괴
            {
                appliedDamage *= 2;
                damageLog += "[크리티컬!] ";
            }

            foreach (var hitCollider in hitColliders) //데미지 적용
            {
                EnemyDataHolder enemy = hitCollider.GetComponent<EnemyDataHolder>();
                if (enemy != null)
                {
                    enemy.TakeDamage(appliedDamage);
                    damageLog += $"[공격] {enemy.enemyData.enemyName} - {appliedDamage} 데미지 입힘.";
                    Debug.Log(damageLog);
                }
            }

            // 내구도 감소 (명중 인원 수만큼)
            int temp = myHolder.currentDurability;
            myHolder.currentDurability = Mathf.Clamp(myHolder.currentDurability - hitCount, 0, maxDurability);

            if (myHolder.currentDurability <= 0) //무기 파괴
            {
                durabilityLog += $"[무기 파괴] {itemName} 아이템 파괴됨\n";
                myHolder.currentDurability = 0;
            }

            durabilityLog += $"[내구도 감소] {itemName} -{hitCount}(-{temp- myHolder.currentDurability})\n";
            durabilityLog += $"[{itemName}] 내구도: {temp}/{maxDurability} > {myHolder.currentDurability}/{maxDurability}";
            Debug.Log(durabilityLog);

            player.GetComponentInChildren<ItemUse>()?.DurabilityItem(myHolder.currentDurability);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, itemDistance);
    }
}
