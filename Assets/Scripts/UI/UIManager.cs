using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("Canvas Collections")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas onScreenControls;
    [SerializeField] private Canvas tutorialModal;
    [SerializeField] private Canvas pauseCanvas;

    private Player player;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        mainCanvas.enabled = true;
        onScreenControls.enabled = true;
        tutorialModal.enabled = false;
        pauseCanvas.enabled = false;
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

    public void PauseGame(){
        Time.timeScale = 0f;
        player.GetComponent<Player>().enabled = false;
        mainCanvas.enabled = false;
        onScreenControls.enabled = false;
        tutorialModal.enabled = false;
        pauseCanvas.enabled = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        player.GetComponent<Player>().enabled = true;
        mainCanvas.enabled = true;
        onScreenControls.enabled = true;
        tutorialModal.enabled = false;
        pauseCanvas.enabled = false;
    }
}
