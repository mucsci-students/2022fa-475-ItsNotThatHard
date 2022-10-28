using UnityEngine;

public class DeathCameraScript : MonoBehaviour
{

    [SerializeField] private AudioClip[] _deathScreams;
    
    void Start()
    {

        AudioClip chosenScream = _deathScreams[Random.Range(0, _deathScreams.Length)];
        FindObjectOfType<BasicAudioManagerScript>().Play2DSound(chosenScream);

    }

}
