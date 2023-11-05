using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private const string SCENE_MENSTRUAL = "CS_Scene_1_Phase";
    private const string SCENE_FOLLICULAR = "CS_Scene_2_Phase";
    private const string SCENE_OVULATION = "CS_Scene_3_Phase";
    private const string SCENE_LUTEAL = "CS_Scene_4_Phase";
    private const string SCENE_ENDING = "CS_Scene_5_Phase";

    [SerializeField] private Image progressBar;
    [SerializeField] private float timePerPhase = 60;
    private int currentPhaseIndex = 0;
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
            currentPhaseIndex++;

            if (currentPhaseIndex >= phases.Length)
            {
                return;
            }

            LoadNextSubScene(phases[currentPhaseIndex]);
        }
    }

    private void LoadNextSubScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    private string[] phases = new string[]
    {
    SCENE_MENSTRUAL,
    SCENE_FOLLICULAR,
    SCENE_OVULATION,
    SCENE_LUTEAL,
    SCENE_ENDING
    };
}
