using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private bool on_target = false;
    public Vector3 destination;
    public int Count_down;
    private int counter = -1;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        counter = Count_down;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            on_target = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on_target = false;
        counter = Count_down;
    }

    void Update()
    {
        if (on_target && counter > 0) counter--;
        else if (counter == 0) player.transform.position = destination;
    }
}
