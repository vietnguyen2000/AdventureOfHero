using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemySpawnerScriptSpike : MonoBehaviour
{
    public GameObject enemy;
    //private float spikeSpeed = 3f;
    float Y;
    float scale;
    Vector2 whereToSpawn;
    float nextSpawnSpike = 0.0f;
    GameObject Spike;
    float timeAlive = 7f;
    float randomTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnSpike)  {
            randomTime = UnityEngine.Random.Range(2f, 5f);
            if (ScoreScript.ScoreVal <= 100) { randomTime = UnityEngine.Random.Range(2f, 5f); }     // Normal Mode
            else if (ScoreScript.ScoreVal <= 500) { randomTime = UnityEngine.Random.Range(1f, 3f); }// Hard Mode
            else { randomTime = UnityEngine.Random.Range(0.75f, 2f); }                              // God Mode
            nextSpawnSpike = Time.time + randomTime;                                                // Thời gian tạo mới ngẫu nhiên.
            scale = UnityEngine.Random.Range(0.75f, 1.6f);
            whereToSpawn = new Vector2(8f, 1.01f);
            Spike = Instantiate (enemy, whereToSpawn, Quaternion.identity);                         // tạo mới 1 object
            Spike.gameObject.transform.localScale = new Vector3((float)Math.Sqrt(scale * 1.9f) / 1.9f, scale, 2);
            Destroy(Spike, timeAlive);                                                              // Sau timeAlive giây sẽ xóa object, lâu hơn eagle do tốc độ spike chậm hơn.
        }
    }
}
