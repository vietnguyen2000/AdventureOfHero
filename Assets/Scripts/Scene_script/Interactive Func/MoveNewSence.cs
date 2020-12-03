using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MoveNewSence : Interactive
{
    public GameObject[] guard;
    public int targetSceneBuildIndex;
    private void Update()
    {
        CheckCanDo();
    }
    void CheckCanDo()
    {
        bool is_over = true;

        if (guard.Length != 0)
        {
            foreach (GameObject i in guard)
            {
                is_over = is_over && (i == null);
            }
        }
        if (is_over)
        {
            canDo = true;
            Transform effect = transform.GetChild(1);
            effect.gameObject.SetActive(true);
        }
    }
    protected override void DoWork(object arg = null)
    {
        if (canDo)
        {
            LoadSceneManager sceneManager = gameObject.AddComponent<LoadSceneManager>();
            sceneManager.LoadScene(targetSceneBuildIndex);
            canDo = false;
        }
    }
}

    