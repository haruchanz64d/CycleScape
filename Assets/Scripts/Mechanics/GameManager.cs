using UnityEngine;

public enum MenstrualPhase
{
    Menstrual,
    Follicular,
    Ovulation,
    Luteal
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timePerPhase = 120f;
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

        if (TimeRemaining <= 0f)
        {
            NextPhase();
        }
    }

    private void NextPhase()
    {
        switch (currentMenstrualPhase)
        {
            // Collect item like wave system
            case MenstrualPhase.Menstrual:
                currentMenstrualPhase = MenstrualPhase.Follicular;
                TimeRemaining = timePerPhase;
                break;
            // Collect item like wave system
            case MenstrualPhase.Follicular:
                currentMenstrualPhase = MenstrualPhase.Ovulation;
                TimeRemaining = timePerPhase;
                break;
            // Just a cutscene
            case MenstrualPhase.Ovulation:
                currentMenstrualPhase = MenstrualPhase.Luteal;
                break;
            // Defense type mini-game
            case MenstrualPhase.Luteal:
                currentMenstrualPhase = MenstrualPhase.Menstrual;
                TimeRemaining = timePerPhase;
                break;
        }
    }
}
