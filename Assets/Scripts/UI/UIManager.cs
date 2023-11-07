using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIManager : MonoBehaviour
{
    [Header("Canvas Collections")]
    public Canvas mainCanvas;
    public Canvas onScreenControls;
    public Canvas pauseCanvas;
    public Canvas modalCanvas;
    public Canvas gameOverCanvas;
    private Player player;

    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI maxHealthText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        Time.timeScale = 0f;
        gameOverCanvas.enabled = false;
        modalCanvas.enabled = true;
        mainCanvas.enabled = false;
        onScreenControls.enabled = false;
        pauseCanvas.enabled = false;
    }

    private void Update()
    {
        if(player.isDead == true)
        {
            ShowDeadScreen();
        }
        if (player == null)
            return;
        int currentHealth = (int)player.GetComponent<HealthSystem>().GetCurrentHealth();
        int maxHealth = (int)player.GetComponent<HealthSystem>().GetMaxHealth();

        currentHealthText.SetText(currentHealth.ToString());
        maxHealthText.SetText(maxHealth.ToString());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ShowDeadScreen()
    {
        Time.timeScale = 0f;
        gameOverCanvas.enabled = true;
        modalCanvas.enabled = false;
        mainCanvas.enabled = false;
        onScreenControls.enabled = false;
        pauseCanvas.enabled = false;
    }

    public void ShowTutorial()
    {
        Time.timeScale = 0f;
        gameOverCanvas.enabled = false;
        modalCanvas.enabled = true;
        mainCanvas.enabled = false;
        onScreenControls.enabled = false;
        pauseCanvas.enabled = true;
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        gameOverCanvas.enabled = false;
        modalCanvas.enabled = false;
        mainCanvas.enabled = false;
        onScreenControls.enabled = false;
        pauseCanvas.enabled = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        gameOverCanvas.enabled = false;
        modalCanvas.enabled = false;
        mainCanvas.enabled = true;
        onScreenControls.enabled = true;
        pauseCanvas.enabled = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CS_Scene_MainMenu");
    }
}
