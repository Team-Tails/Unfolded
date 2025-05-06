using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private string damageTag;
    [SerializeField] private int maxHealth;
    [SerializeField] public float deathFadeTimer;
    [SerializeField] private float fullFadeWait;

    [HideInInspector] public UnityEvent<int, int> OnHealthChange;
    [HideInInspector] public UnityEvent OnDie;
    [HideInInspector] public UnityEvent<float> StartDeathFade;
    private int health;
    public int Health { get => health; private set
        {
            int oldHealth = health;
            health = value;
            OnHealthChange?.Invoke(oldHealth, health);

            if (health == 0)
            {
                StartDeathFade?.Invoke(deathFadeTimer);// Disable player controls when this happens and add the poof for example
            }
        }
    }

    void Start()
    {
        Health = maxHealth;
        StartDeathFade.AddListener((float time) => StartCoroutine(DeathFade(time)));
        OnDie.AddListener(OnDeath);
    }

    private void DamagePlayer(int amount)
    {
        Health -= amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(damageTag))
        {
            DamagePlayer(1);
        }
    }

    private IEnumerator DeathFade(float time)
    {
        yield return new WaitForSeconds(time + fullFadeWait);

        OnDie?.Invoke();
        // go to spawn point
    }

    private void OnDeath()
    {
        Health = maxHealth;
    }
}
