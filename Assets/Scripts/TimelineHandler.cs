using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimelineHandler : MonoBehaviour
{
    private string finalScene = "CS_Scene_4_Phase";

    private void Start()
    {
        SceneManager.LoadScene(finalScene);
    }
}
