using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        MySetting setting = new MySetting();
        OptionsScript.keys = new Dictionary<string, KeyCode>();
        if (OptionsScript.keys.Count == 0)
        {
            setting = setting.loadSetting();
        }
        Screen.fullScreen = setting.isFullScreen;
        if(setting.screenHeight == 0 && setting.screenWidth == 0)
        {
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, setting.isFullScreen);
        }
        else Screen.SetResolution(setting.screenWidth, setting.screenHeight, setting.isFullScreen);
    }
    public void PlayStoryMode()
    {
        PlayerData playerData = new PlayerData();
        string json = JsonUtility.ToJson(playerData);
        string path = Application.dataPath + "/SavePlayerData.txt";
        File.WriteAllText(path, json);
#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
        LoadSceneManager sceneManager = GameObject.Find("Main Camera").AddComponent<LoadSceneManager>();
        sceneManager.LoadScene(2);
    }

    public void PlayEndlessMode() {
        LoadSceneManager sceneManager = GameObject.Find("Main Camera").AddComponent<LoadSceneManager>();
        sceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        //Debug.Log("QUIT!");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }
}
