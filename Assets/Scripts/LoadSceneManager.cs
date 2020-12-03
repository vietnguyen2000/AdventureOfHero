using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public static int scene;
    static bool isLoading;
    public void LoadScene(int scene)
    {
        if (!isLoading)
        {
            isLoading = true;
            LoadSceneManager.scene = scene;
            StartCoroutine(LoadScenceLoadingScreen());
        }
    }
    IEnumerator LoadScenceLoadingScreen()
    {

        AsyncOperation async = SceneManager.LoadSceneAsync("LoadingScreen");
        async.allowSceneActivation = false;
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (async.progress < 0.9f)
        {
            yield return null;
        }
        GameObject Fade = GameObject.Find("Fade");
        Fade.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
        isLoading = false;
    }
}
