using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    new Rigidbody2D rb;

    [Header("DoT")]
    [SerializeField] private bool isDoTActive = false;
    [SerializeField] private float dotDamage = 0.75f;
    [SerializeField] private float dotTickInterval = 1.5f;
    [SerializeField] private float dotTimer = 0f;

    [Header("Slowness")]
    [SerializeField] private bool isSlowed = false;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 5f;

    [Header("Item")]
    private bool hasItem = false;
    private float itemDuration = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (!isDoTActive)
        {
            isDoTActive = true;
            dotTimer = 0f;
        }
    }

    private void Update()
    {
        #region Debug
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TakeDamage(1.0f);
            Debug.Log("Damaged taken by 1.0f");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            isDoTActive = true;
            Debug.Log("DoT activated");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            isDoTActive = false;
            Debug.Log("DoT deactivated");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            isSlowed = true;
            Debug.Log("Slowness activated");
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            isSlowed = false;
            Debug.Log("Slowness deactivated");
        }
        #endregion

        if (currentHealth <= 0f)
        {
            Die();
        }

        if (isDoTActive)
        {
            // Apply bleeding
            dotTimer += Time.deltaTime;
            if (dotTimer >= dotTickInterval)
            {
                currentHealth -= dotDamage;
                dotTimer = 0f;
            }

            // If player had collected anti-bleeding items
            if (hasItem)
            {
                itemDuration -= Time.deltaTime;
                if (itemDuration <= 0f)
                {
                    isDoTActive = false;
                    itemDuration = 5f;
                }
            }
        }

        if (isSlowed)
        {
            // Apply slowness
            rb.velocity *= slowAmount;

            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0f)
            {
                // Remove slowness
                isSlowed = false;
                slowDuration = 5f;
            }
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void PickUpItem()
    {
        hasItem = true;
    }
}
