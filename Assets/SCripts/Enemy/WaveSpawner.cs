using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // --------------------
    // Difficulty Selection
    // --------------------
    public static float GetSpawnInterval(float x)
    {
        x /= .8f;
        return 1f / (Mathf.Pow(x, 1.2f) + 2f + Mathf.Sin(x)) * 8f;
    }

    public static int GetEnemyCount(float x)
    {
        x /= 0.6f;
        return (int)(Mathf.Pow(x, 1.4f) + 5f + Mathf.Sin(x));
    }

    public static int GetWeightedIndex(List<EnemyType> types)
    {
        float totalSpawnWeigth = 0f;
        foreach (EnemyType enemyType in types)
            totalSpawnWeigth += enemyType.weight;

        float pick = Random.value * totalSpawnWeigth;
        int index = 0;
        float cumulativeWeigth = types[0].weight;

        while (pick > cumulativeWeigth && index < types.Count - 1)
        {
            index++;
            cumulativeWeigth += types[index].weight;
        }

        return index;
    }

    [System.Serializable]
    public class EnemyType
    {
        public GameObject enemy;
        public int waveStart;
        public float weight;
    }


    // ----------
    // Variables
    // ----------
    public EnemyType[] enemyTypes;

    /*
    // controll time between waves;
    public enum SpawnState { Spawning, Waiting, Counting };
    public float timeBetweenWaves = 3f;
    public float waveCountDown;
    private SpawnState state = SpawnState.Counting;
    */
    public static int waveIndex;
    
    // change this for testing
    public int startWave;


    // --------------
    // Class Methods
    // --------------
    void Start()
    {
        // waveCountDown = timeBetweenWaves;
        waveIndex = startWave;
        StartCoroutine(SpawnWave());
    }

    public void StartNextWave()
    {
        StartCoroutine(SpawnWave());
    }

    /*
    void FixedUpdate()
    {
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }
    */

    private IEnumerator SpawnWave()
    {
        int enemyCount = GetEnemyCount(waveIndex);
        float spawnInterval = GetSpawnInterval(waveIndex);

        Debug.Log("Spawning wave " + waveIndex + " - Count: " + enemyCount + " - Interval: " + spawnInterval);

        while (enemyCount > 0)
        {
            SpawnEnemy();
            enemyCount--;
            yield return new WaitForSeconds(Random.Range(spawnInterval * .5f, spawnInterval * 2f));
        }
        
        waveIndex++; // increase difficulty
    }

    void SpawnEnemy()
    {
        List<EnemyType> spawnableTypes = GetSpawnableTypes();
        int index = GetWeightedIndex(spawnableTypes);

        GameObject enemy = spawnableTypes[index].enemy;
        Instantiate(enemy, GetSpawnPos(), Quaternion.identity);
    }


    /* Spawn enemy from 4 areas
     *   _____________________
     *  | 3  ______|______  0 |
     *  |   |             |   | 
     *  |___|             |___|
     *  |   |             |   | 
     *  |   |_____________|   |
     *  |_2________|________1_|
     * 
     */


    Vector2 GetSpawnPos()
    {
        int area = Random.Range(0,4);
        Vector2 pos = GameObject.FindGameObjectWithTag("Player").transform.position;

        float xOffset = Random.Range(30f, 100f);
        float yOffset = Random.Range(30f, 100f);

        switch (area)
        {
            case 0:
                pos.x += xOffset;
                pos.y += yOffset;
                break;

            case 1:
                pos.x += xOffset;
                pos.y -= yOffset;
                break;

            case 2:
                pos.x -= xOffset;
                pos.y -= yOffset;
                break;

            case 3:
                pos.x -= xOffset;
                pos.y += yOffset;
                break;

        }
        return new Vector2(pos.x, pos.y);
    }

    List<EnemyType> GetSpawnableTypes()
    {
        List<EnemyType> spawnableTypes = new List<EnemyType>();

        foreach (EnemyType enemyType in enemyTypes)
        {
            if (enemyType.waveStart <= waveIndex)
            {
                spawnableTypes.Add(enemyType);
            }
        }

        return spawnableTypes;
    }
    /*
    public enum SpawnState { Spawning, Waiting, Counting };
    [System.Serializable]
    class Wave
    {
        public Transform enemyType;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private SpawnState state = SpawnState.Counting;


    void Update()
    {
       if(waveCountDown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                // start spawning wave
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void 
    public GameObject normalEnemy;
    public GameObject largeEnemy;
    public float updateDelay = 5f;
    public float difficulty = 1;

    public bool spawning = false;

    void FixedUpdate()
    {
        Debug.Log("TimeElapsed: " + Time.time);
        if (waveCountDown <= 0)
            
    }

   

    void Spawn(GameObject enemyType)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(enemyType, pos, rot);
    }
    */
}
