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
    [SerializeField] private WaveDetails[] levelWaves;
    private int waveIndex;

    [Header("Enemy Prefabs")]
    [SerializeField] GameObject enemyBasicPrefab;
    [SerializeField] GameObject enemyFastPrefab;

    private List<EnemyPortal> enemyPortals;

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

        if(newEnemies == null)
        {
            Debug.Log("Have no waves to setup");
            return; 
        }

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
        if(waveIndex >= levelWaves.Length)
        {
            // Check if all waves are completed; return null if no more waves are available
            return null;
        }

        List<GameObject> newEnemyList = new List<GameObject>();

        for (int i = 0; i < levelWaves[waveIndex].basicEnemy; i++)
        {
            newEnemyList.Add(enemyBasicPrefab);
        }

        for (int i = 0; i < levelWaves[waveIndex].fastEnemy; i++)
        {
            newEnemyList.Add(enemyFastPrefab);
        }

        waveIndex++;

        return newEnemyList;
    }

}
