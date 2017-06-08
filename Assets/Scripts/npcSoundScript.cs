using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcSoundScript : MonoBehaviour 
{
    public static npcSoundScript instancia;

    public InAudioNode FootstepsSound;
    public InAudioNode ShootSound;

    void Awake () 
	{
        instancia = this;
	}
	
	public void UpdatePosition(Vector3 pos) 
	{
        transform.position = pos;
	}
    public void PlayWalkSound()
    {
        InAudio.Play(gameObject, FootstepsSound);
    } 
    public void PlayShootSound()
    {
        InAudio.Play(gameObject, ShootSound);
    }
}
