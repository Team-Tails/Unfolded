using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int, int> OnHealthChange;

    [SerializeField] private int maxHealth;
    private int health;
    public int Health { get => health; private set
        {
            int oldHealth = health;
            health = value;
            OnHealthChange?.Invoke(oldHealth, health);
        }
    }

    void Start()
    {
        Health = maxHealth;
    }
}
