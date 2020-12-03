using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int ScoreVal = 0;
    public float StartTime = 0.0f;
    Text Score;

    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        Score = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score: " + ScoreVal;
        ScoreVal = Mathf.RoundToInt(4 * (Time.time - StartTime));   // Update Score
    }
}
