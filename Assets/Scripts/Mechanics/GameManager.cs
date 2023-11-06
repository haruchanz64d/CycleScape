using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public string sceneName;

    [SerializeField] private Image progressBar;
    private float timePerPhase = 75;
    public float TimeRemaining { get; set; }

    public GameManager()
    {
        TimeRemaining = timePerPhase;
    }

    private void Update()
    {
        TimeRemaining -= Time.deltaTime;
        progressBar.fillAmount = TimeRemaining / timePerPhase;

        if (TimeRemaining <= 0f)
        {
            LoadNextSubScene();
        }
    }

    private void LoadNextSubScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
