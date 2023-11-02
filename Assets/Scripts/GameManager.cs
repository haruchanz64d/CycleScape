using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Player player;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player == null)
            return;
        currentAmmoText.SetText(player.GetArrowCount().ToString());
        maxAmmoText.SetText(player.GetMaxArrowCount().ToString());
        int currentHealth = (int)player.GetComponent<HealthSystem>().GetCurrentHealth();
        int maxHealth = (int)player.GetComponent<HealthSystem>().GetMaxHealth();

        currentHealthText.SetText(currentHealth.ToString());
        maxHealthText.SetText(maxHealth.ToString());
    }
}
