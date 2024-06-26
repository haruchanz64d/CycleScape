using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthSystem : MonoBehaviour
{
    private int moonCounter;
    public int MoonCounter() { return moonCounter; }

    // For Bubble Pop
    private const string ANIMATION_POP = "Pop";

    [SerializeField] private float maxHealth = 100f;
    public float GetMaxHealth() { return maxHealth; }
    [SerializeField] private float currentHealth;
    public float GetCurrentHealth() { return currentHealth; }
    [SerializeField] private Image healthBar;
    private Rigidbody2D rb;

    [Header("DoT")]
    public bool isDotActive = false;
    private float dotDamage = 2.25f;
    private float dotTickInterval = 1.25f;
    [SerializeField] private float dotTimer = 0f;

    [Header("HP Drain")]
    public bool isHPDraining = false;
    private float drainAmount = 2.50f;
    private float drainTickInterval = 1.5f;
    private float drainTimer = 0f;

    [Header("Slowness")]
    public bool isSlowed = false;
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
            this.GetComponent<Player>().isDead = true;
            StartCoroutine(Die());
        }

        if (isDotActive)
        {
            UpdateDoTColor();
            dotTimer += Time.deltaTime;
            if (dotTimer >= dotTickInterval)
            {
                TakeDamage(dotDamage);
                dotTimer = 0f;
            }
        }

        if (!isDotActive)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (isSlowed)
        {
            rb.velocity *= slowAmount;
        }

        if(isHPDraining)
        {
            drainTimer += Time.deltaTime;
            if (drainTimer >= drainTickInterval)
            {
                TakeDamage(drainAmount);
                drainTimer = 0f;
            }
        }
    }

    private IEnumerator Die()
    {
        this.GetComponent<Player>().HandleDeathAnimation();
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sperm"))
        {
            TakeDamage(5);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Unused
        if (collision.gameObject.CompareTag("Blood"))
            isDotActive = true;
        if (collision.gameObject.CompareTag("Positive"))
        {
            RegainHealth(Random.Range(1f, 10f));
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
        }
        if (collision.gameObject.CompareTag("Negative"))
        {
            TakeDamage(Random.Range(1f, 10f));
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
        }
        if (collision.gameObject.CompareTag("AntiDoT"))
        {
            if (!isDotActive)
            {
                collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
                StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
                RegainHealth(Random.Range(1.0f, 2.5f));
            }
            else
            {
                float randomNumber = Random.Range(0f, 1f);
                Debug.Log(randomNumber);
                if (randomNumber <= 0.20f)
                {
                    isDotActive = false;
                    collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
                    StartCoroutine(ResetDoTAfterSeconds(5f));
                    StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
                    RegainHealth(Random.Range(1.0f, 2.5f));
                }
                collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
                StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
                RegainHealth(Random.Range(1.0f, 2.5f));
            }
        }
        if (collision.gameObject.CompareTag("Ovulation"))
        {
            collision.gameObject.GetComponent<Animator>().Play(ANIMATION_POP);
            moonCounter++;
            StartCoroutine(DestroyAfterSeconds(collision.gameObject, 0.5f));
        }
    }
    private IEnumerator ResetDoTAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isDotActive = true;
    }

    private IEnumerator DestroyAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
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
