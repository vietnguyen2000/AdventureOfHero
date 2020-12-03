using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    PowerValue powerValue;
    void Start()
    {
        powerValue = GameObject.Find("Player").GetComponent<PowerValue>();
    }

    // Update is called once per frame
    void Update()
    {
        Text score = transform.GetChild(1).gameObject.GetComponent<Text>();
        score.text = "Score: " + powerValue.Score;

    }
    public void QuitGame()
    {
        LoadSceneManager sceneManager = GameObject.Find("Main Camera").AddComponent<LoadSceneManager>();
        sceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
