using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Аудио миксеры")]
    public AudioMixer masterMixer;

    [Header("Аудио источники")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Музыка и звуки")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip buttonClickSound;
    public AudioClip successSound;
    public AudioClip failSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlayClickSound()
    {
        PlaySFX(buttonClickSound);
    }

    public void PlaySuccessSound()
    {
        PlaySFX(successSound);
    }

    public void PlayFailSound()
    {
        PlaySFX(failSound);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void SetMusicVolume(float value)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
