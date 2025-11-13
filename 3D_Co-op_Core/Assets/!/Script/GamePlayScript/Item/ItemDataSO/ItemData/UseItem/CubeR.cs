using UnityEngine;

[CreateAssetMenu(fileName = "CubeR", menuName = "Scriptable Objects/CubeR")]
public class CubeR : UseableItemDataSO
{
    [Header("UseableItemDataSO - CubeR")]
    public float itemRange;
    public int itemDamage;

    public int MaxDurability;
    public int CurrentDurability;

    public LayerMask enemyLayer;

    private float attackRadius;
    private Vector3 attackCenter;


    public override void ItemUse(GameObject player)
    {
        Debug.Log($"{itemName} 아이템 사용");

        // 애니메이션
        Animator animator = player.GetComponentInChildren<Animator>();
        animator.SetTrigger("Attack");


        // 공격
        Camera cameraObject = player.GetComponentInChildren<Camera>();
        Transform camera = cameraObject.transform;

        attackRadius = itemRange;
        attackCenter = camera.position + camera.forward * 1f;

        int hitCount = 0;

        Collider[] hitColliders = Physics.OverlapSphere(attackCenter, attackRadius, enemyLayer);

        Debug.DrawRay(camera.position, camera.forward * attackRadius, Color.red, itemRange);

        foreach (var hitCollider in hitColliders)
        {
            EnemyDataHolder holder = hitCollider.GetComponent<EnemyDataHolder>();
            if (holder != null)
            {
                hitCount++;
            }
        }

        if (hitCount > 0)
        {
            int appliedDamage = itemDamage;
            string damageLog = "";
            string durabilityLog = "";

            if (hitCount >= CurrentDurability) //명중 인원이 남은 내구도보다 많을 때 -> 대미지 2배 & 무기 파괴
            {
                appliedDamage *= 2;
                damageLog += "[크리티컬!] ";
            }

            foreach (var hitCollider in hitColliders) //데미지 적용
            {
                EnemyDataHolder holder = hitCollider.GetComponent<EnemyDataHolder>();
                if (holder != null)
                {
                    holder.TakeDamage(appliedDamage);
                    damageLog += $"[공격] {holder.enemyData.enemyName} - {appliedDamage} 데미지 입힘.";
                    Debug.Log(damageLog);
                }
            }

            // 내구도 감소 (명중 인원 수만큼)
            int temp = CurrentDurability;
            CurrentDurability = Mathf.Clamp( CurrentDurability - hitCount, 0, MaxDurability);

            if (CurrentDurability <= 0)
            {
                durabilityLog += $"[무기 파괴] {itemName} 아이템 파괴됨\n";
                //무기 파괴
            }

            durabilityLog += $"[내구도 감소] {itemName} -{hitCount}(-{temp-CurrentDurability})\n";
            durabilityLog += $"[{itemName}] 내구도: {temp}/{MaxDurability} > {CurrentDurability}/{MaxDurability}";
            Debug.Log(durabilityLog);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackCenter, attackRadius);
    }
}
