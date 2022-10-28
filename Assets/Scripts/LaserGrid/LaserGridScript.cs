using UnityEngine;
using Player = UnityStandardAssets.Characters.FirstPerson.FirstPersonController;

public class LaserGridScript : MonoBehaviour
{

    private AudioSource _zapSource;

    // Start is called before the first frame update
    void Start()
    {

        _zapSource = GetComponent<AudioSource>();

    }

    void OnTriggerEnter(Collider other)
    {

        Player overlappingPlayer = other.gameObject.GetComponentInChildren<Player>();
        if (overlappingPlayer != null)
        {
            
            _zapSource.enabled = true;
            _zapSource.Play();
            
            overlappingPlayer.Kill();

        }

    }

}
