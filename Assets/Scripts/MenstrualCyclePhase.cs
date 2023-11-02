public enum MenstrualPhase
{
    Menstrual,
    Follicular,
    Ovulation,
    Luteal
}

public class MenstrualCyclePhase
{
    public MenstrualPhase currentMenstrualPhase { get; set; }
    public float TimeRemaining { get; set; }

    public MenstrualCyclePhase()
    {
        currentMenstrualPhase = MenstrualPhase.Menstrual;
        TimeRemaining = 300f;
    }

    public void Update(float deltaTime)
    {
        TimeRemaining -= deltaTime;

        if (TimeRemaining <= 0f)
        {
            NextPhase();
        }
    }

    private void NextPhase()
    {
        switch (currentMenstrualPhase)
        {
            case MenstrualPhase.Menstrual:
                currentMenstrualPhase = MenstrualPhase.Follicular;
                TimeRemaining = 300f;
                break;
            case MenstrualPhase.Follicular:
                currentMenstrualPhase = MenstrualPhase.Ovulation;
                TimeRemaining = 300f;
                break;
            case MenstrualPhase.Ovulation:
                currentMenstrualPhase = MenstrualPhase.Luteal;
                break;
            case MenstrualPhase.Luteal:
                currentMenstrualPhase = MenstrualPhase.Menstrual;
                TimeRemaining = 300f;
                break;
        }
    }
}
