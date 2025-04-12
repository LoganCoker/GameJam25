using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    [SerializeField] private AudioSource soundFXObject;
    private float masterVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use: SoundFXManager.Instance.PlayAudioClip(audioclip from object, transform (objects transform), 0f-1f, float);
    public void PlayAudioClip(AudioClip audioClip, Transform transform, float volume, float pitch)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume * masterVolume;
        audioSource.pitch = pitch;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    // Use: SoundFXManager.Instance.PlayAudioClip(audioclip from object, transform (objects transform), 0f-1f, float);
    public void PlayAudioClipLoop(AudioClip audioClip, Transform transform, float volume, float pitch)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume * masterVolume;
        audioSource.pitch = pitch;
        audioSource.loop = true;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
    }

    // Use: SoundFXManager.Instance.PlayAudioClip(audioclip from object, transform (objects transform), loop if needed, 0f-1f, minimum distance float, maximum distance float)
    public void PlaySpatialAudioClip(AudioClip clip, Transform transform, bool loop = false, float volume = 1f, float minDistance = 1f, float maxDistance = 20f)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.transform.position = transform.position;
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    // Use: SoundFXManager.Instance.PlayRandomAudioClip(audioclip array from object, transform (objects transform), 0f-1f, float);
    public void PlayRandomAudioClip(AudioClip[] audioClip, Transform transform, float volume, float pitch)
    {
        int randArrayPos = Random.Range(0, audioClip.Length);

        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip[randArrayPos];
        audioSource.volume = volume * masterVolume;
        audioSource.pitch = pitch;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}