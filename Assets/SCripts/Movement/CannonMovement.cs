using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{

    Transform turret;
    //angle to move around
    float angle;

    //vector3
    Vector3 mousepos;
    Vector3 objectpos;
    Vector3 distance;

    

    public Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        turret = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        look();
    }

    void look()
    {
        mousepos = Input.mousePosition;

        //Idk what this means LMAOO
        mousepos.z = 5.23f;
        objectpos = cam.WorldToScreenPoint(turret.position);
        distance = mousepos - objectpos;

        //calculating angle based on where the mouse is pointed to and the position of the cannon
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg - 90;
        turret.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
