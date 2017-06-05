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

    public GameObject npc;
    public GameObject player;
    public GameObject deadPlayer;

    public GameObject endCamera;

    public GameObject EndPlayer;

    void Start () 
	{        
        instancia = this;
        endCamera.gameObject.SetActive(false);
        originalfixedDeltaTime = Time.fixedDeltaTime;
        EndPannel.SetActive(false);
        if (npc == null)
        {
            npc = Instantiate(SceneTransitionManager.instancia.Minigame2Prefabs[SceneTransitionManager.instancia.getNpcIndex()], NpcSpawnPoint.transform.position, NpcSpawnPoint.transform.rotation);
        }        
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
        setEndCanvas(SceneTransitionManager.instancia.getNpcName() + " is the winner!!!");
        GameObject dead = Instantiate(deadPlayer, player.transform.position, player.transform.rotation);
        //dead.transform.Rotate(Vector3.up, 180);
        dead.transform.position -= new Vector3(0,0.5f,0);
        Destroy(player.gameObject);
        endCamera.gameObject.SetActive(true);
    }

    public void PlayerWon()
    {
        GameObject obj = Instantiate(EndPlayer, player.transform.position - new Vector3(0, 0.9f, 0), player.transform.rotation);
        EndPannel.SetActive(true);
        end = true;
        Debug.Log("PlayerWon!!");
        StartCoroutine(SlowDown());
        setEndCanvas("Congratulations you won!!!");
        Destroy(player.gameObject);
        endCamera.gameObject.SetActive(true);        
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
        SceneManager.LoadScene(SceneTransitionManager.instancia.getMenuIndex());
    }
}
