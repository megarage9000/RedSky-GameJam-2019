using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private Transform hub;
    private GameObject[] spawns;
    private Transform target;

    private BoxCollider2D hubCollider;

    public float enemySpeed;
    public float stationTime = 10f; // sets how long the enemy needs to stay @Base
    public float timeStayed = 0f; // how long the enemy has been collecting energy

    public bool targetPlayer;
    public bool energyCollected = false;
    public bool stationed = false;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        hub = GameObject.FindGameObjectWithTag("Hub").GetComponent<Transform>();
        spawns = GameObject.FindGameObjectsWithTag("Spawner");
        hubCollider = GameObject.FindGameObjectWithTag("Hub").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        setTarget();

        if (!stationed || energyCollected)
        {
            moveToTarget();
        }
        if (energyCollected)
        {
            animator.SetBool("Charged", true);
        }
    }

    void setTarget()
    {
        if (energyCollected)
        {
            target = findClosestSpawn(spawns);
        }
        else
        {
            if (targetPlayer)
                target = player;
            else
                target = hub;
        }
    }

    void moveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.fixedDeltaTime);

        Vector2 targetPos = target.position - transform.position;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle - 90)), 100);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    // measures the x and y distance between two objects, returns sum of x and y
    float getDistance(Transform currentLoc, Transform targetLoc)
    {
        float x = Mathf.Abs(targetLoc.position.x - currentLoc.position.x);
        float y = Mathf.Abs(targetLoc.position.y - currentLoc.position.y);
        return x + y;
    }

    // find the relative closest point the monster can return to
    Transform findClosestSpawn(GameObject[] spawns)
    {
        float currentDist = getDistance(transform, spawns[0].transform);
        Transform spawn = spawns[0].transform;

        for(int i = 1; i < spawns.Length; ++i)
        {
            if(getDistance(transform,spawns[i].transform) < currentDist)
            {
                currentDist = getDistance(transform, spawns[i].transform);
                spawn = spawns[i].transform;
            }
        }
        return spawn;
    }


    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hub" && !energyCollected)
        {
            targetPlayer = false;
            stationed = true;
            timeStayed += Time.deltaTime;

            if (timeStayed >= stationTime) energyCollected = true;
        }

        stationed = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spawner")
        {
            Destroy(this.gameObject);
        }
    }
}