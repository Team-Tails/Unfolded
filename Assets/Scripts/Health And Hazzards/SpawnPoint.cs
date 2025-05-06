using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnPointManager spawnPointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnPointManager.SetNewSPawnPoint(this);
        }
    }
}
