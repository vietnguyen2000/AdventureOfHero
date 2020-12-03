using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LastDoor : Interactive
{
    public GameObject[] guard;
    //public int targetSceneBuildIndex;
    bool winner;
    GameObject Winner;
    private void Awake()
    {
        Winner = GameObject.Find("WinnerMenu");
    }
    private void Update()
    {
        CheckCanDo();
        if(winner)
        {
            Transform camera = GameObject.Find("Main Camera").GetComponent<Transform>();
            if (Vector3.Distance(camera.position, Winner.transform.position) >= 2f)
                Winner.transform.position = Winner.transform.position + (camera.position - Winner.transform.position) * Time.deltaTime * 3f;
        }
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
            winner = true;
        }
    }
}

