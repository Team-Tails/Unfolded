using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HealthDisplayManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float heartDisplayOffset;
    [SerializeField] private Transform heartDisplayPos;
    [SerializeField] private Image deathScreen;
    private List<GameObject> displayHearts = new List<GameObject>();

    private void Awake()
    {
        healthManager.OnHealthChange.AddListener(OnHealthUpdate);
    }

    private void Start()
    {
        // Disables the editor text
        heartDisplayPos.gameObject.SetActive(false);
        healthManager.StartDeathFade.AddListener((float time) => StartCoroutine(FadeScreen(time, 1)));
        healthManager.OnDie.AddListener(() => StartCoroutine(FadeScreen(healthManager.deathFadeTimer, -1)));
    }

    private void OnHealthUpdate(int oldHealth, int newHealth)
    {
        if (oldHealth == newHealth) return;

        UpdateHeartDisplay(newHealth);
    }

    private void UpdateHeartDisplay(int newHealth)
    {
        foreach (GameObject heart in displayHearts)
        {
            Destroy(heart);
        }

        displayHearts.Clear();

        for (int i = 0; i < newHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform);
            Vector3 heartPos = heartDisplayPos.localPosition + new Vector3(heartDisplayOffset * i, 0, 0);
            newHeart.transform.localPosition = heartPos;
            displayHearts.Add(newHeart);
        }
    }

    /// <summary>
    /// Fades screen to or from black over the time, the direction picks what way it fades, positive 1 for black, negative 1 for clear.
    /// </summary>
    /// <param name="time"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public IEnumerator FadeScreen(float time, int direction)
    {
        direction = Mathf.Clamp(direction, -1, 1);

        // Checks if the colour is 1 or 0 alpha
        while (deathScreen.color.a != ((direction + 1) / 2))
        {
            deathScreen.color = deathScreen.color + new Color(0, 0, 0, (direction * Time.deltaTime) / time);
            //Debug.Log((direction * Time.deltaTime) / time);
            deathScreen.color = new Color(0, 0, 0, Mathf.Clamp(deathScreen.color.a, 0, 1));
            yield return null;
        }
    }
}
