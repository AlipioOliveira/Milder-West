using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameMusic : MonoBehaviour 
{

    public InMusicGroup musicExample;
    public InMusicGroup musicExample2;

    void Start()
    {
        int a = Random.Range(0, 2);
        if (a== 0)
        {
            InAudio.Music.Play(musicExample);
            InAudio.Music.SetVolume(musicExample, 0.1f);
        }
        else
        {
            InAudio.Music.Play(musicExample2);
            InAudio.Music.SetVolume(musicExample2, 0.1f);
        }                
    }

    private void OnDestroy()
    {
        InAudio.Music.Stop(musicExample);
        InAudio.Music.Stop(musicExample2);
    }
}
