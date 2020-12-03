using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;
    private float timeScaleBeforePause;
    private float preTimeScale;
    bool inGame = true;
    private void OnEnable()
    {
        if (inGame)
        {
            preTimeScale = Time.timeScale;
            inGame = false;
        }
        Time.timeScale = 0f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ContinueGame();
        }
    }
    public void ContinueGame() {
        //Time.timeScale = timeScaleBeforePause;
        Time.timeScale = preTimeScale;
        canvas.SetActive(false);
        inGame = true;
    }

    public void QuitGame() {
        //Remove PlayerData.json
//        string path = Application.dataPath + "/Resources/Files/SavePlayerData.txt";
//        File.Delete(path);
//        path = Application.dataPath + "/Resources/Files/SavePlayerData.json.meta";
//        File.Delete(path);
//#if UNITY_EDITOR
//        UnityEditor.AssetDatabase.Refresh();
//#endif
        LoadSceneManager sceneManager = GameObject.Find("Main Camera").AddComponent<LoadSceneManager>();
        sceneManager.LoadScene(0);
        Time.timeScale = 1;

    }
}
