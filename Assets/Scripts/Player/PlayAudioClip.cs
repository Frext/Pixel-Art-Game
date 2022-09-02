using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip audioClip, float volumeScale = 0.5f)
    {
        audioSource.PlayOneShot(audioClip, Mathf.Clamp01(volumeScale));
    }
}
