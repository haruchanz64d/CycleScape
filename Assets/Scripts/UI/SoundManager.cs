using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundOnIcon;
    [SerializeField] Image soundOffIcon;
    private bool muted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;

        AudioSource[] musicSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource musicSource in musicSources)
        {
            musicSource.mute = muted;
        }
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
    }
    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
