using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRandom : MonoBehaviour
{
    [Range(0f, 1f)]
    public float HealthLv1, HealthLv2, HealthLv3, HealthLv4, DameLv1, DameLv2, DameLv3, DameLv4, SpeedLv1, SpeedLv2, SpeedLv3, SpeedLv4, JumpLv1, JumpLv2, JumpLv3, JumpLv4;
    public int DropMaximum = 999;
    private bool isDroped;
    private Dictionary<GameObject, float> Probability = new Dictionary<GameObject, float>();
    private PowerValue powerValue;
    // Start is called before the first frame update
    void Start()
    {
        powerValue = GetComponent<PowerValue>();
        AddProbability("GameObject/Item/Potions/Health1Potion",HealthLv1);
        AddProbability("GameObject/Item/Potions/Health2Potion",HealthLv2);
        AddProbability("GameObject/Item/Potions/Health3Potion",HealthLv3);
        AddProbability("GameObject/Item/Potions/Health4Potion",HealthLv4);
        AddProbability("GameObject/Item/Potions/Dame1Potion",DameLv1);
        AddProbability("GameObject/Item/Potions/Dame2Potion",DameLv2);
        AddProbability("GameObject/Item/Potions/Dame3Potion",DameLv3);
        AddProbability("GameObject/Item/Potions/Dame4Potion",DameLv4);
        AddProbability("GameObject/Item/Potions/Jump1Potion",JumpLv1);
        AddProbability("GameObject/Item/Potions/Jump2Potion",JumpLv2);
        AddProbability("GameObject/Item/Potions/Jump3Potion",JumpLv3);
        AddProbability("GameObject/Item/Potions/Jump4Potion",JumpLv4);
        AddProbability("GameObject/Item/Potions/Speed1Potion",SpeedLv1);
        AddProbability("GameObject/Item/Potions/Speed2Potion",SpeedLv2);
        AddProbability("GameObject/Item/Potions/Speed3Potion",SpeedLv3);
        AddProbability("GameObject/Item/Potions/Speed4Potion",SpeedLv4);
    }
    void AddProbability(string path, float probability)
    {
        GameObject Item = Resources.Load<GameObject>(path);
        Probability.Add(Item,probability);
    }
    // Update is called once per frame
    void Update()
    {
        if (powerValue.HP <= 0 && !isDroped)
        {
            isDroped = true;
            Invoke("DropItemRandom",0.8f);
        }
    }
    void DropItemRandom()
    {
        int DropCount = 0;
        foreach(var prob in Probability)
        {
            if (DropCount == DropMaximum) return;
            float random = Random.Range(0.000001f, 1f);
            if(random <= prob.Value)
            {
                Debug.Log("Success!!! Droppppppp " + prob.Key.ToString());
                prob.Key.GetComponent<Item>().isBounce = true;
                Instantiate(prob.Key, transform.position, new Quaternion());
                prob.Key.GetComponent<Item>().isBounce = false;
                DropCount++;
            }
        }
    }
}
