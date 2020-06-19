using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public float timeInBetween;
    float lastTime;
    bool waveEnd = true;
    GameObject player;
    GameObject hub;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hub = GameObject.FindGameObjectWithTag("Hub");
        lastTime = 0;
    }
    void Start()
    {
        /*if (EnemiesDead())
        {
            StartCoroutine(waveSpawner());

        }*/
    }

    private void Update()
    {
        if (EnemiesDead())
        {
            Debug.Log("Start");
            SpawnWave();
        }
    }


   
   

    void SpawnWave()
    {
        if (player.GetComponent<TankCollision>().dead == false && hub.GetComponent<BaseScript>().dead == false)
        {
            if (waveEnd = true && Time.time - lastTime > timeInBetween)
            {
                waveEnd = false;
                this.GetComponent<WaveSpawner>().StartNextWave();
            }
        }
    }

    bool EnemiesDead()
    {
    
        GameObject[] normalEnemies = GameObject.FindGameObjectsWithTag("EnemyNormal");
        GameObject[] largeEnemies = GameObject.FindGameObjectsWithTag("EnemyLarge");

        foreach(GameObject normalEnemy in normalEnemies)
        {
            if(normalEnemy != null)
            {
                return false;
            }
        }
        foreach (GameObject largeEnemy in largeEnemies)
        {
            if (largeEnemy != null)
            {
                return false;
            }
        }

        return true;
    }

}
