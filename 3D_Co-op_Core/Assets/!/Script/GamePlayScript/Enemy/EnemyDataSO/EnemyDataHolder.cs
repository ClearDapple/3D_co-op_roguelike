using UnityEngine;

public class EnemyDataHolder : MonoBehaviour
{
    public EnemyDataSO enemyData;
    public int currentHP;


    private void Start()
    {
        currentHP = enemyData.enemyMaxHP;
    }

    public void TakeDamage(int damage)
    {
        var temp = currentHP;
        currentHP = Mathf.Clamp(currentHP - damage, 0, enemyData.enemyMaxHP);
        Debug.Log($"[피격] 몬스터:{enemyData.enemyName} {damage} 피해입음.\n" + $"[{enemyData.enemyName}] HP: {temp} > {currentHP}");

        if (currentHP <= 0)
        {
            DropLoot();
            DestroySelf();
        }
    }

    public void TakeHeal(int heal)
    {
        var temp = currentHP;
        currentHP = Mathf.Clamp(currentHP + heal, 0, enemyData.enemyMaxHP);
        Debug.Log($"[회복] 몬스터:{enemyData.enemyName} {heal}({currentHP - temp}) 회복함.\n" + $"[{enemyData.enemyName}] HP: {temp} > {currentHP}");
    }

    public void DropLoot()
    {
        //죽으면 전리품 드롭하기 //지금은 몬스터 재생성:테스트용
        Transform EnemyParent = transform.parent;

        GameObject monsterPrefab = enemyData.enemyPrefab;
        GameObject monster = Instantiate(monsterPrefab, EnemyParent);
        Vector3 RespownPoint = transform.position + new Vector3(0,5,0);
        monster.transform.position = RespownPoint;
    }

    public void DestroySelf()
    {
        Debug.Log($"몬스터: {enemyData.enemyName} 사망함.");
        Destroy(gameObject);
    }
}
