using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameMusic : MonoBehaviour 
{

    public InMusicGroup musicExample;
    public InMusicGroup musicExample2;

    int sound = 0;
    void Start()
    {
        sound = Random.Range(0, 2);
        if (sound == 0)
        {
            InAudio.Music.Play(musicExample);
            
        }
        else
        {
            InAudio.Music.Play(musicExample2);
        }
        InAudio.Music.SetVolume(musicExample, 0.1f);
    }

    //private void OnDestroy()
    //{
    //    if (sound == 0)
    //        InAudio.Music.Stop(musicExample);
    //    else InAudio.Music.Stop(musicExample2);                       
    //}
}
