using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    Canvas pause;
    [SerializeField]
    Canvas help;
    [SerializeField]
    PlayerController controller;
    [SerializeField]
    GameObject pauseParent;

    public void ClickResume()
    {
        controller.enabled = true;
        pauseParent.SetActive(false);
    }

    public void ClickHelp()
    {
        pause.enabled = false;
        help.enabled = true;
    }

    public void ClickMainMenu()
    {
        //needs to be the name of the main menu
        //must also be in the build scenes
        StartCoroutine(LoadScene("MainMenu")); 
    }
    public void ClickExit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);

        while(!load.isDone)
        {
            yield return null;
        }

    }
}
