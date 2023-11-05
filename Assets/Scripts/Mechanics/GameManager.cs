using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public enum MenstrualPhase
{
    Menstrual,
    Follicular,
    Ovulation,
    Luteal
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private float timePerPhase = 10; // Two minutes of gameplay time, I just put 10 for debugging
    public MenstrualPhase currentMenstrualPhase { get; set; }
    public float TimeRemaining { get; set; }

    public GameManager()
    {
        currentMenstrualPhase = MenstrualPhase.Menstrual;
        TimeRemaining = timePerPhase;
    }

    private void Update()
    {
        TimeRemaining -= Time.deltaTime;
        progressBar.fillAmount = TimeRemaining / timePerPhase;
        if (TimeRemaining <= 0f)
        {
            Debug.Log($"Current Phase is {currentMenstrualPhase}");
            NextPhase();
        }
    }

    private void NextPhase()
    {
        switch (currentMenstrualPhase)
        {
            case MenstrualPhase.Menstrual:
                currentMenstrualPhase = MenstrualPhase.Follicular;
                TimeRemaining = timePerPhase;
                break;
            case MenstrualPhase.Follicular:
                currentMenstrualPhase = MenstrualPhase.Ovulation;
                TimeRemaining = timePerPhase;
                break;
            case MenstrualPhase.Ovulation:
                currentMenstrualPhase = MenstrualPhase.Luteal;
                TimeRemaining = timePerPhase;
                break;
            case MenstrualPhase.Luteal:
                currentMenstrualPhase = MenstrualPhase.Menstrual;
                TimeRemaining = timePerPhase;
                break;
        }
    }

    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentMenstrualPhase = MenstrualPhase.Menstrual;
        Time.timeScale = 1f;
    }
}
