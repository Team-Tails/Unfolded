using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthDisplayManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float heartDisplayOffset;
    [SerializeField] private Transform heartDisplayPos;
    private List<GameObject> displayHearts = new List<GameObject>();

    private void Awake()
    {
        healthManager.OnHealthChange.AddListener(OnHealthUpdate);
    }

    private void OnHealthUpdate(int oldHealth, int newHealth)
    {
        UpdateHeartDisplay(newHealth);
    }

    private void UpdateHeartDisplay(int newHealth)
    {
        foreach (GameObject heart in displayHearts)
        {
            Destroy(heart);
        }

        for (int i = 0; i < newHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, transform);
            Vector3 heartPos = heartDisplayPos.localPosition + new Vector3(50 * i, 0, 0);
            newHeart.transform.localPosition = heartPos;
        }
    }
}
