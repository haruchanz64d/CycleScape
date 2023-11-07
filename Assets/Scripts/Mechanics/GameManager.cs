
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public string sceneName;
    private Player player;
    [SerializeField] private Image progressBar;
    private float timePerPhase = 70;
    public float TimeRemaining { get; set; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
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