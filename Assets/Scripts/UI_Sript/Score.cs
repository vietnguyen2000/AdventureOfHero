using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;
    private PowerValue powerValue;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        GameObject player = GameObject.Find("Player");
        powerValue = player.GetComponent<PowerValue>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + powerValue.Score.ToString();
    }
}
