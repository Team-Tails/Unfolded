using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private int damage = 1;
    
    private void OnTriggerEnter(Collider collision)
    {
        print("collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            healthManager.DamagePlayer(damage);
        }
    }
}
