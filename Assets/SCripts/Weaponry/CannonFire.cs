using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{

    public GameObject Weapon1;
    public GameObject Weapon2;

    Transform firePoint;

   float rate;

    int selected;
    float delay;
    float lastFire;
    float refresh;
    
    void Awake()
    {
        firePoint = transform.Find("PointofFire");

        selected = 1;
        lastFire = Time.time;
        rate = Weapon1.GetComponent<Bullet>().fireRate;
        delay = 60 / rate;
        refresh = 0;
        
    }

    void FixedUpdate()
    {
        selectWeapon();
        if (Input.GetKey(KeyCode.Mouse0) && refresh - delay >= 0)
        {
            refresh = 0;
            shoot();
            lastFire = Time.time;
        }
        refresh++;
    }

    void shoot()
    {

        Vector3 firedPos = firePoint.position;
        Quaternion rotation = firePoint.rotation;
        switch (selected)
        {
            case 1:
                Instantiate(Weapon1, firedPos, rotation);
                break;
           case 2:
                Instantiate(Weapon2, firedPos, rotation);
                
                break;
            default:
                break;
        }  
    }

    void selectWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            selected = 1;
            rate = Weapon1.GetComponent<Bullet>().fireRate;
            delay = 60f / rate;

        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            selected = 2;
            rate = Weapon2.GetComponent<Bullet>().fireRate;
            delay = 60f / rate;

        } 
    }
}
