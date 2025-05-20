using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Canvas menu;
    [SerializeField]
    Canvas help;
    [SerializeField]
    Button playButton;
    [SerializeField]
    TMP_Text playButtonText;
    

    public void StartClick()
    {
        //needs to be the name of the game scene we want to play
        //must also be in the build scenes
        StartCoroutine(LoadScene("newLevelBlockOut")); 
    }

    public void HelpClick()
    {
        menu.enabled = false;
        help.enabled = true;
    }
    
    public void ExitClick()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    IEnumerator LoadScene(string scene)
    {
        playButtonText.text = "Loading...";
        playButton.enabled = false;
        yield return null;

        AsyncOperation load = SceneManager.LoadSceneAsync(scene);

        while(!load.isDone)
        {
            yield return null;
        }

    }
}
