using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    public UnityEvent<int> OnHealthChange;

    [SerializeField] private int maxHealth;
    private int health;
    public int Health { get => health; private set
        {
            health = value;
            OnHealthChange?.Invoke(health);
        }
    }

    void Start()
    {
        Health = maxHealth;
    }
}
