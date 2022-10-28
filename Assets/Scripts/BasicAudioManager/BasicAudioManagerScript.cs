using UnityEngine;

public class BasicAudioManagerScript : MonoBehaviour
{

    private AudioSource _2dAudioSource;

    public void Start()
    {

        _2dAudioSource = GetComponent<AudioSource>();

    }

    public void Play2DSound(AudioClip toPlay)
    {

        _2dAudioSource.clip = toPlay;
        _2dAudioSource.Play();

    }

}
