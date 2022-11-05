using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxScript : MonoBehaviour
{

    [SerializeField] private AudioClip[] _songs;
    [SerializeField] private float _gapBetweenSongsSec = 2;
    private List<AudioClip> _spool;
    private AudioClip _currentSong;
    private AudioSource _speaker;

    // Start is called before the first frame update
    void Start()
    {

        _spool = new List<AudioClip>(_songs.Shuffle());
        _speaker = GetComponent<AudioSource>();
        Invoke(nameof(LoadNextSong), _gapBetweenSongsSec);

    }

    void LoadNextSong()
    {

        if (_spool.Count == 0) 
        { 
            
            _spool.AddRange(_songs.Shuffle()); 
            
            // Don't allow the same song to be played twice in a row
            if (_spool[0] == _currentSong)
            {

                var temp = _spool[0];
                _spool[0] = _spool[_spool.Count - 1];
                _spool[_spool.Count - 1] = temp;

            }

        }

        _currentSong = _spool[0];
        _spool.Remove(_currentSong);
        var songDuration = _currentSong.length + _gapBetweenSongsSec;

        _speaker.PlayOneShot(_currentSong);
        Invoke(nameof(LoadNextSong), songDuration);

    }

    public void Silence()
    {

        CancelInvoke();
        _speaker.Stop();

    }

}
