using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DONTCAREABOUTTHIS : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MySetting setting = new MySetting();
        OptionsScript.keys = new Dictionary<string, KeyCode>();
        if (OptionsScript.keys.Count == 0)
        {
            setting = setting.loadSetting();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
