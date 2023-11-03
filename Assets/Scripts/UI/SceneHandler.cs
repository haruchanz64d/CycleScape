using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
