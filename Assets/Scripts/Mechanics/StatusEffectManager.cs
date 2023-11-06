using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Update()
    {
        SetDotActive();
        SetSlowed();
    }

    public void SetDotActive()
    {
        healthSystem.isDotActive = true;
    }

    public void SetSlowed()
    {
        healthSystem.isSlowed = true;
    }
}
