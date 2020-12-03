using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{

    public int NumberOfEnemy;
    public GameObject[] Enemy;

    private int number;

    public float timeDelay;
    private float delay;

    // Start is called before the first frame update
    void Start()
    {
        number = NumberOfEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (delay > timeDelay&&number>0)
        {
            delay = 0;
            SpawnEnemyRanDom();
        }
    }
    private void SpawnEnemyRanDom()
    {
        number--;
        int index = Random.Range(0, Enemy.Length);
        Instantiate(Enemy[index],transform.position,new Quaternion());
        
    }
}
