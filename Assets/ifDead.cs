using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ifDead : MonoBehaviour
{
    GameObject player;
    GameObject hub;

    // Start is called before the first frame update
    void Awake()
    {

        hub = GameObject.FindGameObjectWithTag("Hub");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<TankCollision>().dead == true || hub.GetComponent<BaseScript>().dead == true)
        {
            SceneManager.LoadScene(0);
        }
    }
}
