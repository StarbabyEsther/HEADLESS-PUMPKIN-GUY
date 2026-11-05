using UnityEngine;

public class MusicScript : MonoBehaviour
{

    AudioSource audioPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void Play(AudioClip ac)
{
    audioPlayer.clip = ac;
    audioPlayer.Play();
}
}