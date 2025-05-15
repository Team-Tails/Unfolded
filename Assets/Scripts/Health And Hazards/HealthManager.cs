using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public float deathFadeTimer;
    [SerializeField] private float fullFadeWait;
    [SerializeField] private PlayerStateController stateController;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private ParticleSystem poofParticles;

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
                playerSprite.enabled = false;
                poofParticles.transform.rotation = Quaternion.Euler(poofParticles.transform.rotation.x, poofParticles.transform.rotation.z, 180);
                poofParticles.Play();
            }
        }
    }

    void Start()
    {
        Health = maxHealth;
        StartDeathFade.AddListener((float time) => StartCoroutine(DeathFade(time)));
        OnDie.AddListener(OnDeath);
    }

    public void DamagePlayer(int amount)
    {
        Health -= amount;
        
        // If losing health
        if (amount > 0)
        {

        }
    }

    private IEnumerator DeathFade(float time)
    {
        yield return new WaitForSeconds(time + fullFadeWait);

        OnDie?.Invoke();
        // go to spawn point
        playerSprite.enabled = true;
        poofParticles.transform.rotation = Quaternion.Euler(poofParticles.transform.rotation.x, poofParticles.transform.rotation.y, -180);
    }

    private void OnDeath()
    {
        Health = maxHealth;
        if (stateController.CurrentState == stateController.PlaneState)
        {
            stateController.PlaneState.EndPlaneState();
        } 
    }
}
