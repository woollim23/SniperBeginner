using UnityEngine;

public class SoundManager : SingletonDontDestory<SoundManager>
{
    [Header("Audio Settings")]
    public AudioSource audioSource;

    private void Awake()
    {
        base.Awake();
        EnsureAudioSource();
    }

    private void EnsureAudioSource()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (audioSource == null)
        {
            return;
        }

        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }        
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (audioSource == null)
        {
            
            return;
        }

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }        
    }

    public void StopBackgroundMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}