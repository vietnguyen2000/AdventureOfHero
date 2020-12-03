using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    // Start is called before the first frame update
    string[] tips;
    void Start()
    {
        tips = new string[10];
        tips[0] = "Find the supporter, its will help you defeat Boss!";
        tips[1] = "When monsters die, they will drop potions ramdom!";
        tips[2] = "Kill the monster to get coins.";
        tips[3] = "Potions is powerful, so you should kill monster.";
        tips[4] = "Jump attack can kill the monsters easyly.";
        tips[5] = "Some monster is run away form you, be careful!";
        tips[6] = "Each potions has 4 level.";
        tips[7] = "Some trap will kill you, be careful!";
        tips[8] = "The Attack3 has the most Damage";
        tips[9] = "Red is health, yellow is damage, green is jump, purple is speed";
        int i = Random.Range(0, 9);
        Text text = GetComponent<Text>();
        text.text = tips[i];
    }

}
