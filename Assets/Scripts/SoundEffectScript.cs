
using UnityEngine;


public class SoundEffectScript : MonoBehaviour
{
    AudioSource audioPlayer;

    void Awake()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void Play(AudioClip ac)
    {
        if (ac != null && audioPlayer != null)
        {
            audioPlayer.PlayOneShot(ac);
        }
        else
        {
            Debug.LogError("AudioClip or AudioSource missing!");
        }
    }
}
