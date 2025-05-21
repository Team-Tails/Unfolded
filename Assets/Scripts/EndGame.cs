using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] HealthDisplayManager blackScreen;
    [SerializeField] float fadeTime;
    [SerializeField] PlayerController controller;

    private float endTime;
    float elapsedTime = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.enabled = false;
            StartCoroutine(blackScreen.FadeScreen(fadeTime, 1));
            StartCoroutine(LoadScene("ToBeContinued"));
            endTime = Time.time;
        }
    }

    IEnumerator LoadScene(string scene)
    {

        while (elapsedTime < fadeTime) //make sure fade has completed
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadSceneAsync(scene);
    }    
}
