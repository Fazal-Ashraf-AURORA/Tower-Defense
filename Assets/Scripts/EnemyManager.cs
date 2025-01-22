using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveDetails
{
    public int basicEnemy;
    public int fastEnemy;
}

public class EnemyManager : MonoBehaviour
{
    public bool waveCompleted = false;

    public float timeBetweenWaves = 10;
    public float waveTimer;

    [SerializeField] private WaveDetails[] levelWaves;
    private int waveIndex;

    private float checkInterval = 0.5f;
    private float nextCheckTime;

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

    private void Update()
    {
        HandleWaveCompletion();

        HandleWaveTiming();
    }

    private void HandleWaveCompletion()
    {
        if(ReadyToCheck() == false)
            return;

        if (waveCompleted == false && AllEnemiesDefeated())
        {
            waveCompleted = true;
            waveTimer = timeBetweenWaves;
        }
    }

    private void HandleWaveTiming()
    {
        if (waveCompleted)
        {
            waveTimer -= Time.deltaTime;

            if (waveTimer <= 0)
                SetupNextWave();
        }
    }

    public void ForceNextWave()
    {
        if(AllEnemiesDefeated() == false)
        {
            Debug.LogWarning("Can't force while there is enemies in the game!");
            return;
        }

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

        waveCompleted = false;
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

    private bool AllEnemiesDefeated()
    {
        foreach(EnemyPortal portal in enemyPortals)
        {
            if(portal.GetActiveEnemies().Count > 0)
                return false;
        }

        return true;
    }

    private bool ReadyToCheck()
    {
        if(Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + checkInterval;
            return true;
        }
        return false;
    }
}
