using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Canvas menu;
    [SerializeField]
    Canvas help;
    [SerializeField]
    Button playButton;
    

    public void StartClick()
    {
        playButton.enabled = false;
        //needs to be the name of the game scene we want to play
        //must also be in the build scenes
        StartCoroutine(LoadScene("newLevelBlockOut")); 
    }

    public void HelpClick()
    {
        menu.enabled = !menu.enabled;
        help.enabled = !help.enabled;
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
        AsyncOperation load = SceneManager.LoadSceneAsync(scene);

        while(!load.isDone)
        {
            yield return null;
        }

    }
}
