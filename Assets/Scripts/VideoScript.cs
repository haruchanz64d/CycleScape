using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class VideoScript : MonoBehaviour
{
    public string sceneName;
    VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Play();
        videoPlayer.loopPointReached += CheckOver;
    }

    private void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneName);
    }
}
