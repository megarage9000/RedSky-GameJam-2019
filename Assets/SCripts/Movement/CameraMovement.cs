using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Transform player;
    GameObject tank;
    float smoothing = 5f;
    Vector3 distance;
    Vector3 target;
    
    // Start is called before the first frame update
    void Awake()
    {
        //Finds tank object and uses its transform
        tank = GameObject.Find("Tank");
        player = tank.transform;

        //sets initial distance
        distance = transform.position - player.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        follow();
    }

    void follow()
    {
        if (tank != null)
        {
            
            target = player.position + distance;
            target.x = Mathf.Clamp(target.x, -140 , 140);
            target.y = Mathf.Clamp(target.y, -70, 70);
            transform.position = Vector3.Lerp(transform.position, target, smoothing * Time.deltaTime);
      
        }

    }
}

