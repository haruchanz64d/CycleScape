using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] Image musicOnIcon;
    [SerializeField] Image musicOffIcon;
    private bool musicMuted = false;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicMuted"))
        {
            PlayerPrefs.SetInt("musicMuted", 0);
            Load();
        }
        UpdateMusicIcon();
        foreach (AudioSource musicSource in FindObjectsOfType<AudioSource>())
        {
            musicSource.mute = musicMuted;
        }
    }

    public void OnButtonPress()
    {
        musicMuted = !musicMuted;

        foreach (AudioSource musicSource in FindObjectsOfType<AudioSource>())
        {
            musicSource.mute = musicMuted;
        }

        UpdateMusicIcon();
    }
    private void UpdateMusicIcon()
    {
        if (musicMuted == false)
        {
            musicOnIcon.enabled = true;
            musicOffIcon.enabled = false;
        }
        else
        {
            musicOnIcon.enabled = false;
            musicOffIcon.enabled = true;
        }
        Save();
    }
    private void Load()
    {
        musicMuted = PlayerPrefs.GetInt("musicMuted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("musicMuted", musicMuted ? 1 : 0);
    }
}
