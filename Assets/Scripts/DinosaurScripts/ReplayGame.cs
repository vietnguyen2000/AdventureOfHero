using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReplayGame : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("DinosaurLikeGameplay");
        Time.timeScale = 1;
    }
}
