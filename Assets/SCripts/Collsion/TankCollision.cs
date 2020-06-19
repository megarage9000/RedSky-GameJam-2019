using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class TankCollision : MonoBehaviour
{
    //For UI
    public Slider energyBar;
    public Text status;

    BoxCollider2D playerCollider;
    public float energy;
    public float energyChange;
    public float energyMax;
    public bool regenerating;
    public bool dead;
    TankMovement moveScript;

    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        regenerating = false;
        energyMax = energy;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        moveScript = GetComponent<TankMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        deplete();
        calcExtraEnergy();
        checkDead();
        
    }

    //Finds if Enemy iss attacking
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
       if(coll.gameObject.layer == 12)
       {
            takeDamage(coll.gameObject.GetComponent<EnemyCollision>().damage * Time.deltaTime);
            checkDead();
       } 
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        Debug.Log(coll.gameObject.name);
        if (coll.gameObject.layer == 12)
        {
            takeDamage(coll.gameObject.GetComponent<EnemyCollision>().damage * Time.deltaTime);
            checkDead();
        }
    }


    //Finds Hub, where depleting stops
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Hub" && coll.isTrigger)
        {
            regenerating = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Hub" && coll.isTrigger)
        {
            regenerating = false;
        }
    }



    //All Functions which modify energy

    void updateBar()
    {
        energyBar.value = (energy/energyMax)*100;
    }

    void takeDamage(float amount)
    {
        
        energy -= amount;
        updateBar();
    }

    public void deplete()
    {
        if (regenerating == false)
        {
            decreaseEnergy(1);
        }
    }

    public void decreaseEnergy(float times)
    {
        energy -= energyChange * times * Time.deltaTime;
        updateBar();
    }

    public void regainEnergy(float amount)
    {
        if(energy + amount <= energyMax * 2)
        {
            energy += amount;
            updateBar();
        }
        else if(energy + amount > energyMax)
        {
            energy = energyMax;
            updateBar();
        }
        

    }

    void calcExtraEnergy()
    { 
        if(energy - energyMax > 0)
        {
            float temp = energy - energyMax;
            setExtraEnergy(temp);
        }
        else if(energy - energyMax <= 0)
        {
            status.text = " ";
        }   
    }

    void checkDead()
    {
        if (energy <= 0)
        {
            dead = true;
            Die();
        }
    }

    void setExtraEnergy(float over)
    {
        status.text = Mathf.RoundToInt(over) + " = Extra Energy";
    }

    void Die()
    {
        if(energy <= 0)
        {
            transform.GetComponent<TankMovement>().enabled = false;
            transform.GetComponentInChildren<CannonMovement>().enabled = false;
            transform.GetComponentInChildren<CannonFire>().enabled = false;
            transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            Time.timeScale = 0;

        }
    }
}
