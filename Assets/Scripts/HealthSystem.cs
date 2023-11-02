using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSystem : MonoBehaviour
{
    // For Bubble Pop
    private const string ANIMATION_POP = "Pop";

    [SerializeField] private float maxHealth = 100f;
    public float GetMaxHealth() { return maxHealth; }
    [SerializeField] private float currentHealth;
    public float GetCurrentHealth() { return currentHealth; }
    [SerializeField] private Image healthBar;
    private Rigidbody2D rb;
    [Header("DoT")]
    [SerializeField] private bool isDoTActive = false;
    [SerializeField] private float dotDamage = 0.75f;
    [SerializeField] private float dotTickInterval = 1.5f;
    [SerializeField] private float dotTimer = 0f;
    [SerializeField] private float dotDamageIncreaseOverTime = 0.25f;

    [Header("Slowness")]
    [SerializeField] private bool isSlowed = false;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 5f;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / 100f;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void RegainHealth(float regainHealth)
    {
        currentHealth += regainHealth;
        healthBar.fillAmount = currentHealth / 100f;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0f)
        {
            Die();
        }

        if (isDoTActive)
        {
            UpdateDoTColor();
            dotTimer += Time.deltaTime;
            if (dotTimer >= dotTickInterval)
            {
                TakeDamage(dotDamage);
                dotTimer = 0f;
            }
        }

        if (!isDoTActive)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (isSlowed)
        {
            rb.velocity *= slowAmount;

            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                isSlowed = false;
                slowDuration = 5f;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Blood"))
            isDoTActive = true;
        if (collision.gameObject.CompareTag("Positive"))
        {
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            RegainHealth(10);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Negative"))
        {
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("AntiDoT"))
        {
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            isDoTActive = false;
            Destroy(collision.gameObject);
            StartCoroutine(ResetDoTAfterSeconds(5f));
        }
        // WIP
        if (collision.gameObject.CompareTag("Ovulation"))
        {
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            Destroy(collision.gameObject);
        }
    }


    private IEnumerator ResetDoTAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isDoTActive = true;
    }

    private void UpdateDoTColor()
    {
        float healthLostPercentage = (maxHealth - currentHealth) / maxHealth;

        Color color = Color.Lerp(Color.red, Color.white, healthLostPercentage);

        color.r = Mathf.Lerp(1f, 0.5f, healthLostPercentage);
        color.g = Mathf.Lerp(0.5f, 0f, healthLostPercentage);
        color.b = Mathf.Lerp(0.5f, 0f, healthLostPercentage);

        GetComponent<SpriteRenderer>().color = color;

        if (Time.frameCount % 2 == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
