using System.Collections;
using UnityEditor;
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
    [SerializeField] private ParticleSystem respawnParticles;
    [SerializeField] private PlayerController controller;

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
                controller.enabled = false;
                StartDeathFade?.Invoke(deathFadeTimer);// Disable player controls when this happens and add the poof for example
                playerSprite.enabled = false;
                respawnParticles.Play();
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
            StartCoroutine(PlayerDamageFlash());
        }
    }

    private IEnumerator DeathFade(float time)
    {
        yield return new WaitForSeconds(time + fullFadeWait);

        OnDie?.Invoke();
        // go to spawn point
        playerSprite.enabled = true;
        controller.enabled = true;
    }

    private void OnDeath()
    {
        Health = maxHealth;
        if (stateController.CurrentState == stateController.PlaneState)
        {
            stateController.PlaneState.EndPlaneState();
        } 
    }

    // Flashes the player for when damage is taken.
    private IEnumerator PlayerDamageFlash()
    {
        const float timeToWait = 0.1f;
        float startTime = Time.time;

        do
        {
            float done = Mathf.Clamp(Time.time - startTime, 0.01f, timeToWait) / timeToWait;
            float lerp = Mathf.Lerp(1, 0.5f, done);
            playerSprite.color = new Color(1.0f, lerp, lerp);
            yield return null;
        } while (Time.time - startTime < timeToWait);

        startTime = Time.time;

        do
        {
            float done = Mathf.Clamp(Time.time - startTime, 0.01f, timeToWait) / timeToWait;
            float lerp = Mathf.Lerp(0.5f, 1, done);
            playerSprite.color = new Color(1.0f, lerp, lerp);
            yield return null;
        } while (Time.time - startTime < timeToWait);
        
        playerSprite.color = new Color(1.0f, playerSprite.color.b, playerSprite.color.g);
    }
}
