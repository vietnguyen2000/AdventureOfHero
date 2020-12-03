using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(LoadNewScene());
    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        Debug.Log(Time.time);

        // This line waits for 1.5 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(2f);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        
        AsyncOperation async = SceneManager.LoadSceneAsync(LoadSceneManager.scene);
        async.allowSceneActivation = false;
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (async.progress < 0.9f)
        {
            yield return null;
        }
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f);
        async.allowSceneActivation = true;
    }
    IEnumerator FadeOut()
    {
        GameObject Fade = GameObject.Find("Fade");
        Fade.GetComponent<Animator>().SetBool("FadeOut", true);
        yield return new WaitForSeconds(1f);
    }


}