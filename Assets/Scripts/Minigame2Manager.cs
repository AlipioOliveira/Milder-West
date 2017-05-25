using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Minigame2Manager : MonoBehaviour 
{
    public static Minigame2Manager instancia;

    private bool end = false;   

    public GameObject EndPannel;
    public Text EndText;

    public RandomMovement playerMov;

    public GameObject NpcSpawnPoint;

    public float slowness = 10f;
    public float slowDownTime = 3f;
    private float originalfixedDeltaTime;    

    void Start () 
	{
        instancia = this;
        originalfixedDeltaTime = Time.fixedDeltaTime;
        EndPannel.SetActive(false);
        GameObject newNpc = Instantiate(SceneTransitionManager.instancia.Minigame2Prefabs[SceneTransitionManager.instancia.getNpcIndex()], NpcSpawnPoint.transform.position, NpcSpawnPoint.transform.rotation);
    }
	
	void Update () 
	{        
        
	}

    public void NpcWon()
    {
        EndPannel.SetActive(true);
        end = true;
        Debug.Log("NpcWon!!");
        StartCoroutine(SlowDown());
        setEndCanvas("Better Luck next time!!!");
    }

    public void PlayerWon()
    {
        EndPannel.SetActive(true);
        end = true;
        Debug.Log("PlayerWon!!");
        StartCoroutine(SlowDown());
        setEndCanvas("Congratulations you won!!!");
    }

    private void setEndCanvas(string winner)
    {
        Cursor.visible = true;
        playerMov.setCursorState(false);
        EndText.text = winner;
    }

    IEnumerator SlowDown()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

        yield return new WaitForSeconds(slowDownTime * Time.timeScale);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;
    }

    public void changeTimeBakcToNormal()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalfixedDeltaTime;
    }

    public void RestartScene()
    {
        Cursor.visible = false;
        changeTimeBakcToNormal();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
    }

    public void ContinueButton()
    {
        Cursor.visible = false;
        changeTimeBakcToNormal();
        SceneManager.LoadScene(0);
    }
}
