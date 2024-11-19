using UnityEngine;

public class SoundManager : SingletonDontDestory<SoundManager>
{
    [Header("Audio Settings")]
    public AudioSource audioSource;

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource가 연결되지 않았습니다.");
            return;
        }

        if (clip != null)
        {
            Debug.Log($"사운드 재생: {clip.name}");
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("재생할 AudioClip이 없습니다.");
        }
    }

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position, float volume = 1.0f)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
        else
        {
            Debug.LogWarning("재생할 AudioClip이 없습니다.");
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource가 설정되지 않았습니다.");
            return;
        }

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("재생할 배경음악 AudioClip이 없습니다.");
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