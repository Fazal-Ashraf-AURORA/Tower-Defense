using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemy;
    public int fastEnemy;
}

public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyPortal> enemyPortals;
    [SerializeField] private WaveDetails currentWave;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject enemyBasicPrefab;
    [SerializeField] GameObject enemyFastPrefab;

    private void Awake()
    {
        enemyPortals = new List<EnemyPortal>(FindObjectsOfType<EnemyPortal>());
    }

    private List<GameObject> NewEnemyWave()
    {
        List<GameObject> newEnemyList = new List<GameObject>();

        for (int i = 0; i < currentWave.basicEnemy; i++)
        {
            newEnemyList.Add(enemyBasicPrefab);
        }

        for (int i = 0; i < currentWave.fastEnemy; i++)
        {
            newEnemyList.Add(enemyFastPrefab);
        }

        return newEnemyList;
    }

}
