using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public int score;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;

    }

    // Update is called once per frame
    void Update()
    {

        score = Mathf.RoundToInt(Time.time);
        scoreText.text = score.ToString();
        

    }
}
