using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instancia;

    public GameObject GamePannel;
    public Text TurnNumberText;
    public Text TurnNameText;
    public Text CheckpointText;

    public GameObject EndPannel;
    public Text EndText;

    public Button ContinueBtn;
    public Button RestartBtn;

    public GameObject fadeCanvas;
    private Animator fadeAnimator;

    void Start()
    {
        EndPannel.SetActive(false);
        fadeAnimator = fadeCanvas.GetComponent<Animator>();
        instancia = this;
    }

    public void setTunrNumberText(string text)
    {
        TurnNumberText.text = text;
    }
    public void setTunrNameText(string text)
    {
        TurnNameText.text = text;
    }
    public void setTunrCheckpointText(string text)
    {
        CheckpointText.text = text;
    }
    public void setEndTextText(string text)
    {
        EndText.text = text;
    }

    public void RestartScene()
    {
        Cursor.visible = false;
        //StartCoroutine(WaitForSeconds());
        fadeAnimator.SetTrigger("start");
        fadeAnimator.speed = (ExplodingManager.instancia.slowness);
        StartCoroutine(WaitToLoadScene(getAnimationTime("FadeOut"), SceneManager.GetActiveScene().buildIndex));
    }

    public void ContinueButton()
    {
        Cursor.visible = false;        
        //StartCoroutine(WaitForSeconds());
        fadeAnimator.SetTrigger("start");
        fadeAnimator.speed = (ExplodingManager.instancia.slowness);
        StartCoroutine(WaitToLoadScene(getAnimationTime("FadeOut"), SceneTransitionManager.instancia.getMenuIndex()));
    }

    IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(2f);
    }

    public void ActivateEndCanvas()
    {
        Cursor.visible = true;
        EndPannel.SetActive(true);
        GamePannel.SetActive(false);
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
        yield return new WaitForSeconds(time / (ExplodingManager.instancia.slowness));
        ExplodingManager.instancia.changeTimeBakcToNormal();
        SceneManager.LoadScene(sceneIndex);
    }
}