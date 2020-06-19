using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject normalEnemy;
    public GameObject largeEnemy;
    public float updateDelay = 5f;
    public float difficulty = 1;

    public bool spawning = false;

    void FixedUpdate()
    {
        if(!spawning)
            StartCoroutine(BatchSpawner());
    }

    private IEnumerator BatchSpawner()
    {
        spawning = true;

        difficulty += Time.time/50;
        for(int i = 0; i < Mathf.Floor(Mathf.Sqrt(difficulty)); ++i)
        {
            float rng = Random.Range(0f, 1f);
            if (rng > 0.6)
                Spawn(normalEnemy);
            else if (rng > 0.4)
                Spawn(largeEnemy);
        }

        yield return new WaitForSeconds(updateDelay);
        spawning = false;
    }

    void Spawn(GameObject enemyType)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;

        Instantiate(enemyType, pos, rot);
    }
}
