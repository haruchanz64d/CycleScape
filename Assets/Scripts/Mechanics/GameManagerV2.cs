using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManagerV2 : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    public GameObject eggObject;
    public GameObject playerObject;

    private float timePerPhase = 75;
    public float TimeRemaining { get; set; }

    public GameManagerV2()
    {
        TimeRemaining = timePerPhase;
    }

    private bool playerIsDead()
    {
        return playerObject.GetComponent<HealthSystem>().GetCurrentHealth() <= 0;
    }

    void Update()
    {
        TimeRemaining -= Time.deltaTime;
        progressBar.fillAmount = TimeRemaining / timePerPhase;

        if (playerIsDead())
        {
            SceneManager.LoadScene("CS_Scene_5_Phase_Bad");
        }

        if (TimeRemaining <= 0f)
        {
            SceneManager.LoadScene("CS_Scene_5_Phase_Good");
        }
    }
}
