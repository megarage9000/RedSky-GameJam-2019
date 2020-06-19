using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public float health;
    public float damage;
    float energyGain;
    

    CircleCollider2D enemyCollider;

    TankCollision playerEnergy;


    // Start is called before the first frame update
    void Awake()
    {
        enemyCollider = GetComponent<CircleCollider2D>();
        energyGain = health / 4;
        playerEnergy = GameObject.FindGameObjectWithTag("Player").GetComponent<TankCollision>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        checkIfDead();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == 11) 
        {
            health -= coll.gameObject.GetComponent<Bullet>().damage;
            switch (coll.gameObject.tag)
            {
                case "Minigun":
                    Destroy(coll.gameObject);
                    break;
                case "EnergyBall":
                    Physics2D.IgnoreCollision(this.enemyCollider, coll);
                    break;
                default:
                    break;
            }   
        }
    }


    void checkIfDead()
    {
        if (health <= 0)
        {
            playerEnergy.regainEnergy(energyGain);
            Destroy(this.gameObject);
        }
    }
   }
