using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaseScript : MonoBehaviour
{

    public Slider energyBar;
    public Text status;
    public float energy;
    float currentEnergy;
    public bool dead;
    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        energy = 200f;
        currentEnergy = energy;
        energyBar.value = currentEnergy;
    }

    void Update()
    {
        checkIfDead();
    }
    void hubToPlayer(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            var player = coll.gameObject.GetComponent<TankCollision>();
            //donate
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("donate");
                if (player.energy <= player.energyChange || currentEnergy >= energy)
                {
                    setStatus("Health too low/Base health too high.");
                }
                else
                {
                    setStatus("Donating...");
                    player.decreaseEnergy(2);
                    currentEnergy += player.energyChange * 2 * Time.deltaTime;
                    updateBar();
                }
            }
            //heal
            else if (Input.GetKey(KeyCode.F))
            {
                Debug.Log("heal");
                if (player.energy >= player.energyMax || currentEnergy <= player.energyChange)
                {
                    setStatus("Health too high/Base health too low");
                }
                else
                {
                    setStatus("Healing...");
                    player.regainEnergy(player.energyChange * 2 * Time.deltaTime);
                    currentEnergy -= player.energyChange * 2 * Time.deltaTime;
                    updateBar();
                }
            }
            else
            {
                setStatus("Press F to Heal, or Press E to Donate Energy.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        hubToPlayer(coll);
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        hubToPlayer(coll);
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            setStatus(" ");
        }    
    }

    void updateBar()
    {
        energyBar.value = (currentEnergy/energy) * 100;
    }
    //Bullets
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Projectile")
        {
            Destroy(coll.gameObject);
        }
    }
    void setStatus(string s)
    {
        status.text = s;
    }
    //Enemy
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.layer == 12)
        {
            currentEnergy -= coll.gameObject.GetComponent<EnemyCollision>().damage * Time.deltaTime;
            updateBar();
        }
    }

    void checkIfDead()
    {
        if(currentEnergy <= 0)
        {
            dead = true;
        }
    }
}
