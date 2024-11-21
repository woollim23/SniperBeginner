using UnityEngine;

public class SoundManager : SingletonDontDestory<SoundManager>
{
    [Header("Audio Settings")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip titleBGM;
    public AudioClip gameBGM;
    public AudioClip buttonClickSFX;
    public AudioClip itemPickSFX;
    public AudioClip deadSFX;
    public AudioClip ouchSFX;

    private void Start()
    {
        EnsureAudioSources();
    }

    private void EnsureAudioSources()
    {
        if (bgmSource == null)
        {
            bgmSource = gameObject.AddComponent<AudioSource>();
            bgmSource.loop = true;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip, float volume = 0.15f)
    {
        if (sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (bgmSource == null || clip == null)
        {
            return;
        }

        if (bgmSource.clip == clip && bgmSource.isPlaying)
        {
            return;
        }

        bgmSource.clip = clip;
        bgmSource.volume = 0.5f;
        bgmSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }
}