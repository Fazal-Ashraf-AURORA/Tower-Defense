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

    private void Start()
    {
        SetupNextWave();
    }

    [ContextMenu("Setup Next Wave")]
    private void SetupNextWave()
    {
        List<GameObject> newEnemies = NewEnemyWave();
        int portalIndex = 0;

        for (int i = 0; i < newEnemies.Count; i++)
        {
            GameObject enemyToAdd = newEnemies[i];
            EnemyPortal portalToRecieveEnemy = enemyPortals[portalIndex];

            portalToRecieveEnemy.AddEnemy(enemyToAdd);

            portalIndex++;

            if(portalIndex >= enemyPortals.Count)
                portalIndex = 0;
        }
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
