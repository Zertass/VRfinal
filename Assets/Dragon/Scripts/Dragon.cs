using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public GameObject deathEffectPrefab;
    public AudioClip deathSound;
    public AudioClip[] hitSounds;
    public AudioClip[] flapSounds;
    private AudioSource audioSource;
    public HealthBar healthBar;
    public DragonMovement dragonMovement;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        if (flapSounds.Length > 0)
        {
            StartCoroutine(PlayFlapSounds());
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBar != null)
        {
            healthBar.UpdateHealth(currentHealth);
        }

        PlayHitSound();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayHitSound()
    {
        if (hitSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, hitSounds.Length); // —лучайный звук
            audioSource.PlayOneShot(hitSounds[randomIndex]);
        }
    }

    private IEnumerator PlayFlapSounds()
    {
        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(1.666f); // ∆дем 5 секунд
            if (audioSource != null && flapSounds.Length > 0)
            {
                int randomIndex = Random.Range(0, flapSounds.Length); // —лучайный звук
                audioSource.PlayOneShot(flapSounds[randomIndex]);
            }
        }
    }

    private void Die()
    {
        if (deathEffectPrefab)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        if (dragonMovement)
        {
            dragonMovement.Die();
        }

        if (deathSound && audioSource)
        {
            audioSource.PlayOneShot(deathSound);
            Destroy(gameObject, deathSound.length);
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}