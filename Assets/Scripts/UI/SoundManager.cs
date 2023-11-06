using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted;

    private void Start()
    {
        muted = PlayerPrefs.GetInt("muted", 0) == 1;
        DontDestroyOnLoad(this.gameObject);
        if (!muted)
        {
            AudioListener.pause = false;
            AudioSource[] musicSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource musicSource in musicSources)
            {
                musicSource.mute = false;
            }
        }
        else
        {
            AudioListener.pause = true;
            AudioSource[] musicSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource musicSource in musicSources)
            {
                musicSource.mute = true;
            }
        }

        UpdateButtonIcon();
    }

    public void OnButtonPress()
    {
        muted = !muted;
        AudioListener.pause = muted;
        AudioSource[] musicSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource musicSource in musicSources)
        {
            musicSource.mute = muted;
        }

        UpdateButtonIcon();
        Save();
    }
    private void UpdateButtonIcon()
    {
        soundOnIcon.enabled = muted == false;
        soundOffIcon.enabled = muted;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
