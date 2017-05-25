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

	void Start () 
	{
        EndPannel.SetActive(false);
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
        ExplodingManager.instancia.changeTimeBakcToNormal();
        StartCoroutine(WaitForSeconds());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueButton()
    {
        Cursor.visible = false;
        ExplodingManager.instancia.changeTimeBakcToNormal();
        StartCoroutine(WaitForSeconds());
        SceneManager.LoadScene(0);
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
}
