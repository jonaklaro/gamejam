using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
  public static SoundManager instance;

  public AudioMixer audioMixer;

  [SerializeField] private AudioClip[] audioClips;

  private AudioSource audioSource;

  private void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
    }

    DontDestroyOnLoad(gameObject);

    audioSource = GetComponent<AudioSource>();
  }

  public void PlaySoundEffect(string clipName)
  {
    AudioClip clip = GetAudioClip(clipName);

    if (clip != null)
    {
      audioSource.clip = clip;
      audioSource.PlayOneShot(clip);
    }
    else
    {
      Debug.LogWarning("Audio clip not found!");
    }
  }

  public void PlayMusic(string clipName)
  {
    AudioClip clip = GetAudioClip(clipName);

    if (clip != null)
    {
      audioSource.clip = clip;
      audioSource.loop = true;
      audioSource.Play();
    }
    else
    {
      Debug.LogWarning("Audio clip not found!");
    }
  }

  public void SetVolume(string paramName, float volume)
  {
    audioMixer.SetFloat(paramName, volume);
  }

  private AudioClip GetAudioClip(string clipName)
  {
    foreach (AudioClip clip in audioClips)
    {
      if (clip.name == clipName)
      {
        return clip;
      }
    }

    return null;
  }
}
