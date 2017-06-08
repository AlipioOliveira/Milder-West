using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
    public GameObject fadeCanvas;
    private Animator fadeAnimator;

    public void Start()
    {
        fadeAnimator = fadeCanvas.GetComponent<Animator>();
    }

    public void Exit()
    {
        fadeAnimator.SetTrigger("start");        
        StartCoroutine(WaitToExit(getAnimationTime("FadeOut")));
    }
    public void Continue()
    {
        fadeAnimator.SetTrigger("start");
        StartCoroutine(WaitToLoadScene(getAnimationTime("FadeOut"), SceneManager.GetActiveScene().buildIndex + 1));
    }

    private float getAnimationTime(string animName)
    {
        RuntimeAnimatorController ac = fadeAnimator.runtimeAnimatorController;    //Get Animator controller
        for (int i = 0; i < ac.animationClips.Length; i++)                 //For all         
            if (ac.animationClips[i].name == animName)        //If it has the same name as your clip            
                return ac.animationClips[i].length;
        return 0;
    }
    IEnumerator WaitToLoadScene(float time, int sceneIndex)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneIndex);
    }
    IEnumerator WaitToExit(float time)
    {
        yield return new WaitForSeconds(time);
        Exit();
    }
}
