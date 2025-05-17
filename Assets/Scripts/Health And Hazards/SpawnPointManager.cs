using UnityEngine;
using System.Collections.Generic;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;

    private SpawnPoint spawnPoint;

    private void Start()
    {
        healthManager.OnDie.AddListener(Respawn);
    }

    public void SetNewSPawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }

    public void Respawn()
    {
        healthManager.transform.position = spawnPoint.transform.position + Vector3.up;
    }
}
