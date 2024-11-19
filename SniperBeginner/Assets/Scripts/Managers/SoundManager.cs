using UnityEngine;

public class SoundManager : SingletonDontDestory<SoundManager>
{
    [Header("Audio Settings")]
    public AudioSource audioSource;

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource�� ������� �ʾҽ��ϴ�.");
            return;
        }

        if (clip != null)
        {
            Debug.Log($"���� ���: {clip.name}");
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("����� AudioClip�� �����ϴ�.");
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
            Debug.LogWarning("����� AudioClip�� �����ϴ�.");
        }
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource�� �������� �ʾҽ��ϴ�.");
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
            Debug.LogWarning("����� ������� AudioClip�� �����ϴ�.");
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