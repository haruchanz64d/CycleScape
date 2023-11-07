using UnityEngine;
using UnityEngine.SceneManagement;
public class MoonCounter : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
    }

    private void Update()
    {
        Debug.Log($"Number of Moon Collectible: {healthSystem.MoonCounter().ToString()}");

        if(healthSystem.MoonCounter() >= 10)
        {
            SceneManager.LoadScene("CS_Scene_3_Phase");
        }
    }
}
