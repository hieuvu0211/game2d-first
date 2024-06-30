using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void GamePlay()
    {
        PlayerPrefs.SetInt("currentHeal", 300);
        PlayerPrefs.SetInt("currentScene", 1);
        PlayerPrefs.SetInt("YourPoint", 0);
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("currentScene"));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
