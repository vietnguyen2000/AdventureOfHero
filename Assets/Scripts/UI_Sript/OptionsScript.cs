using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class MySetting
{
    public KeyCode up = KeyCode.W , sitdown = KeyCode.S , left = KeyCode.A, right = KeyCode.D, jump = KeyCode.Space, attack = KeyCode.H, defense = KeyCode.J, slide = KeyCode.K;
    public float Volume = 1f;
    public bool isFullScreen = true;
    public int screenWidth, screenHeight;

    public  MySetting save()
    {
        MySetting setting = new MySetting();
        setting.up = OptionsScript.keys["Up"];
        setting.sitdown = OptionsScript.keys["SitDown"];
        setting.left = OptionsScript.keys["Left"];
        setting.right = OptionsScript.keys["Right"];
        setting.jump = OptionsScript.keys["Jump"];
        setting.attack = OptionsScript.keys["Attack"];
        setting.defense = OptionsScript.keys["Defense"];
        setting.slide = OptionsScript.keys["Slide"];
        setting.Volume = OptionsScript.Volume;
        setting.isFullScreen = OptionsScript.isFullScreen;
        setting.screenWidth = OptionsScript.screenWidth;
        setting.screenHeight = OptionsScript.screenHeight;
        return setting;
    }
    private void Load(MySetting setting)
    {
        if (OptionsScript.keys.Count == 0)
        {
            OptionsScript.keys.Add("Up", KeyCode.W);
            OptionsScript.keys.Add("SitDown", KeyCode.S);
            OptionsScript.keys.Add("Left", KeyCode.A);
            OptionsScript.keys.Add("Right", KeyCode.D);
            OptionsScript.keys.Add("Jump", KeyCode.Space);
            OptionsScript.keys.Add("Attack", KeyCode.H);
            OptionsScript.keys.Add("Defense", KeyCode.J);
            OptionsScript.keys.Add("Slide", KeyCode.K);
        }
        OptionsScript.keys["Up"] = setting.up;
        OptionsScript.keys["SitDown"] = setting.sitdown;
        OptionsScript.keys["Left"] = setting.left;
        OptionsScript.keys["Right"] = setting.right;
        OptionsScript.keys["Jump"] = setting.jump;
        OptionsScript.keys["Attack"] = setting.attack;
        OptionsScript.keys["Defense"] = setting.defense;
        OptionsScript.keys["Slide"] = setting.slide;
        OptionsScript.Volume = setting.Volume;
        OptionsScript.isFullScreen = setting.isFullScreen;
        if (setting.screenHeight == 0 && setting.screenWidth == 0)
        {
            //Debug.Log(Display.main.systemHeight);
            OptionsScript.screenHeight = Display.main.renderingHeight;
            OptionsScript.screenWidth = Display.main.renderingWidth;
        }
        else
        {
            OptionsScript.screenHeight = setting.screenHeight;
            OptionsScript.screenWidth = setting.screenWidth;
        }
    }
    public MySetting loadSetting()
    {
        string json;
        MySetting fileSetting = new MySetting();
        Load(fileSetting);
        string path = Application.dataPath + "/Settings.txt";
        try
        {
            json = File.ReadAllText(path);
        }
        catch (Exception e)
        {
            json = JsonUtility.ToJson(fileSetting);
            File.WriteAllText(path, json);
        }
        //TextAsset temp = Resources.Load<TextAsset>("Files/Settings");
        //json = temp.text;
        fileSetting = JsonUtility.FromJson<MySetting>(json);
        Load(fileSetting);
        Debug.Log("Load Settings Success!!");
        return fileSetting;
    }
}
public class OptionsScript : MonoBehaviour {
    // Store the value for all the button control keys:
    MySetting setting = new MySetting();
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public static float Volume;
    public Image up, sitdown, left, right, jump, attack, defense, slide;
    private Sprite[] Keyboard;
    private GameObject currentKey;
    private Animator anim;
    public static bool isFullScreen;
    public static int screenWidth, screenHeight;
    private void OnEnable() {
        // Initial values for control keys:
        setting = setting.loadSetting();
        Keyboard = Resources.LoadAll<Sprite>("KeyBoard/Keyboard");
        left.sprite = setSprite(keys["Left"]);
        right.sprite = setSprite(keys["Right"]);
        jump.sprite = setSprite(keys["Jump"]);
        slide.sprite = setSprite(keys["Slide"]);
        attack.sprite = setSprite(keys["Attack"]);
        defense.sprite = setSprite(keys["Defense"]);
        sitdown.sprite = setSprite(keys["SitDown"]);
        up.sprite = setSprite(keys["Up"]);
        anim = GetComponent<Animator>();
        anim.SetTrigger("attackTrigger");
        Toggle fullScreenToggle = GameObject.Find("FullScreenToggle").GetComponent<Toggle>();
        fullScreenToggle.isOn = isFullScreen;
        Slider slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = OptionsScript.Volume;
        Text nowResolution = GameObject.Find("NowResolution").GetComponent<Text>();
        nowResolution.text = OptionsScript.screenWidth.ToString() + "x" + OptionsScript.screenHeight.ToString();
        //anim["attack1"].layer = 222;
    }

    void Update() {

    }
    Sprite setSprite(KeyCode key)
    {
        if (((int)key >= 97 && (int)key <= 122) || ((int)key >= 48 && (int)key <= 57) || ((int)key >= 96 && (int)key <= 105) || (key == KeyCode.Space) || ((int)key>=273 && (int)key<=276))
        {
            if ((int)key >= 97 && (int)key <= 122)
            {
                return Keyboard[key - KeyCode.A];
            }
            else if ((int)key >= 48 && (int)key <= 57)
            {
                if (key == KeyCode.Alpha0)
                    return Keyboard[37];
                else
                    return Keyboard[key - KeyCode.Alpha1 + 28];
            }
            else if ((int)key >= 96 && (int)key <= 105)
            {
                if (key == KeyCode.Keypad0)
                    return Keyboard[37];
                else
                    return Keyboard[key - KeyCode.Keypad1 + 28];
            }
            else if ((int)key >= 273 && (int)key <= 276)
            {
                switch ((int)key)
                {
                    case 273: return Keyboard[49];
                    case 274: return Keyboard[51];
                    case 275: return Keyboard[52];
                    default: return Keyboard[50];
                }
            }
            else return Keyboard[38];
        }
        else return Keyboard[63] ;
    }
    // Better than Update Function
    void OnGUI() {
        if (currentKey != null) {
            //Animation.Pla
            Event e = Event.current;
            if (e.isKey)
            {
                
                Debug.Log((int)e.keyCode);
                if (((int)e.keyCode >= 97 && (int)e.keyCode <= 122) || ((int)e.keyCode >= 48 && (int)e.keyCode <= 57) || ((int)e.keyCode >= 96 && (int)e.keyCode <= 105) || (e.keyCode == KeyCode.Space) || ((int)e.keyCode >= 273 && (int)e.keyCode <= 276))
                {
                    string keyName = currentKey.name.Substring(0, currentKey.name.IndexOf("Butt"));
                    Debug.Log(keyName);
                    OptionsScript.keys[keyName] = e.keyCode;
                    Image key = currentKey.gameObject.GetComponent<Image>();
                    key.sprite = setSprite(e.keyCode);
                }
            }
            
        }
    }

    public void ChangeKey(GameObject clicked) {
        currentKey = clicked;
    }
    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
        OptionsScript.isFullScreen = isFullScreen;
        setting.isFullScreen = isFullScreen;
    }
    public void ChangeResolutions(Text Resolution)
    {
        switch (Resolution.text)
        { 
            case "720x480":
                Screen.SetResolution(720, 480, isFullScreen);
                OptionsScript.screenWidth = 720;
                OptionsScript.screenHeight = 480;
                break;
            case "1024x576":
                Screen.SetResolution(1024, 576, isFullScreen);
                OptionsScript.screenWidth = 1024;
                OptionsScript.screenHeight = 576;
                break;
            case "1600x900":
                Screen.SetResolution(1600, 900, isFullScreen);
                OptionsScript.screenWidth = 1600;
                OptionsScript.screenHeight = 900;
                break;
            case "1920x1080":
                Screen.SetResolution(1920, 1080, isFullScreen);
                OptionsScript.screenWidth = 1920;
                OptionsScript.screenHeight = 1080;
                break;
            case "2560x1440":
                Screen.SetResolution(2560, 1440, isFullScreen);
                OptionsScript.screenWidth = 2560;
                OptionsScript.screenHeight = 1440;
                break;
        }
    }
    public void OpenChangeResolutions(GameObject OptionsResolutions)
    {
        for (int i = 0; i < 5; i++)
        {
            Transform temp = OptionsResolutions.transform.GetChild(i);
            string resolutions = temp.GetChild(0).gameObject.GetComponent<Text>().text;
            int width = int.Parse(resolutions.Substring(0, resolutions.IndexOf("x")));
            int height = int.Parse(resolutions.Substring(resolutions.IndexOf("x")+1));
            if(Display.main.systemHeight >= height || Display.main.systemWidth >= width )
            {
                temp.gameObject.SetActive(true);
            }
        }
    }
    public void SetVolume(Slider slider)
    {
        OptionsScript.Volume = slider.value;
        setting.Volume = OptionsScript.Volume;
    }
    public void saveSetting()
    {
        MySetting mySetting = new MySetting();
        mySetting = mySetting.save();
        string json = JsonUtility.ToJson(mySetting);
        string path = Application.dataPath + "/Settings.txt";
        File.WriteAllText(path, json);
        Debug.Log("Save Settings Success!!!");
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}
