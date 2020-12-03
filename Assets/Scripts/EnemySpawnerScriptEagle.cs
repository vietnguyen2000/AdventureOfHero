using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScriptEagle : MonoBehaviour
{
    public GameObject enemy;
    float randY;
    Vector2 whereToSpawn;
    float nextSpawnEagle = 0.0f;
    GameObject Eagle;
    float timeAlive = 3.5f;
    float randomTime;

    GameObject[] spikes;
    EnemyController spikeControllerScript;
    bool ableToSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnEagle)  {
            ableToSpawn = true;
            spikes = GameObject.FindGameObjectsWithTag("Spike");
            foreach (var spike in spikes)    {  // kiểm tra có object spike nào nằm trong vùng mà nếu ta spawn eagle vào lúc này thì người chơi không thể đi qua được
                spikeControllerScript = spike.GetComponent<EnemyController>();
                if (spikeControllerScript.timeAlive > 1.35f && spikeControllerScript.timeAlive < 2.5f)  // Math here: bit.ly/2OWDQ2S
                {
                    ableToSpawn = false;
                    break;
                }
            }
            
            if (ableToSpawn) {
                if (ScoreScript.ScoreVal <= 100) { randomTime = Random.Range(3f, 6f); }         // Normal Mode
                else if (ScoreScript.ScoreVal <= 500)  { randomTime = Random.Range(2f, 4f); }   // Hard Mode
                else { randomTime = Random.Range(1f, 3f); }                                     // God Mode
                nextSpawnEagle = Time.time + randomTime;                                        // Thời gian tạo mới ngẫu nhiên. 
                randY = Random.Range(2.65f, 3.75f);                                             // Tọa độ xuất hiện ngẫu nhiên.
                whereToSpawn = new Vector2(8f, randY);
                Eagle = Instantiate(enemy, whereToSpawn, Quaternion.identity);                  // Tạo mới 1 object
                Destroy(Eagle, timeAlive);                                                      // Sau timeAlive giây sẽ xóa object
            }
        }
    }
}
