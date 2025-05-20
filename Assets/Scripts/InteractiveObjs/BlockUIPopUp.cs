using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlockUIPopUp : MonoBehaviour
{
    [Header("UI Settings")]
    public Canvas uiCanvas;
    public Image image1;
    public Image image2;

    [Tooltip("Total time the UI should remain visible.")]
    public float displayDuration = 5f;

    [Tooltip("Time between switching images.")]
    public float imageSwitchInterval = 0.5f;

    private bool hasBeenTriggered = false;

    private void Start()
    {
        if (uiCanvas != null)
            uiCanvas.enabled = false;

        if (image1 != null)
            image1.gameObject.SetActive(false);

        if (image2 != null)
            image2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasBeenTriggered = true;
            StartCoroutine(ShowUIRoutine());
        }
    }

    private IEnumerator ShowUIRoutine()
    {
        uiCanvas.enabled = true;
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(false);

        float elapsed = 0f;
        bool showImage1 = false;

        while (elapsed < displayDuration)
        {
            yield return new WaitForSeconds(imageSwitchInterval);
            showImage1 = !showImage1;
            image1.gameObject.SetActive(showImage1);
            image2.gameObject.SetActive(!showImage1);
            elapsed += imageSwitchInterval;
        }

        uiCanvas.enabled = false;
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
    }
}
