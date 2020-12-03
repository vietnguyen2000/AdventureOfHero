using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    Text GameOver;
    Image replayButtonImage;
    DinoController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<DinoController>();
        GameOver = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isDead) {
            GameOver.text = "Game Over!";
            
            // Hiện button replay:
            GameObject temp = GameObject.FindGameObjectWithTag("UI");
            replayButtonImage = temp.GetComponent<Image>();
            replayButtonImage.enabled = true;
        }
    }
}
