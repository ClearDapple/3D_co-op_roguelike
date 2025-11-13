using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;       //이름
    public EnemyRank enemyRank;    //몬스터 랭크
    public GameObject enemyPrefab; //프리팹
    public Sprite enemyIcon;       //아이콘
    public string itemDescription; //설명

    public int enemyMaxHP;   //체력
    public int enemyATK;     //공격력
    public float enemySpeed; //이동 속도
}

public enum EnemyRank
{
    Nomal,
    Rere,
    Boss,
}
