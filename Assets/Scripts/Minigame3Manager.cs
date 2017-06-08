using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Minigame3Manager : MonoBehaviour {

    public static Minigame3Manager instancia;


    private bool end = false;

    public GameObject EndPannel;
    public Text EndText;

    public GameObject target;
    public GameObject player;
    public List<GameObject> bottles;
    public int prevWins = 0;

    private int Winner = 0;

    public GameObject NpcSpawnPoint;

    public float slowness = 10f;
    public float slowDownTime = 3f;
    private float originalfixedDeltaTime;

    public GameObject EndPlayer;

    public Camera endCamera;

    // Use this for initialization
    void Start () {
        instancia = this;
        endCamera.gameObject.SetActive(false);
        NpcSpawnPoint.transform.Translate(-10 * prevWins, 0, 0);
        endCamera.transform.Translate(-10 * prevWins, 0, 0);
        target.transform.position = NpcSpawnPoint.transform.position;
        EndPannel.SetActive(false);
        originalfixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update () {
        
        if(bottles.Count <= 0) End(1);
	}

    public void End(int winner)
    {
        foreach (var item in bottles)
        {
            if (item != null)
            {
                item.transform.parent = this.transform;
            }
        }
        if (winner == 0)
        {
            Debug.Log("NPCWon!!");
            //player.GetComponent<ExplodingPlayer>().playerCamera.enabled = false;
            Kill();
            endCamera.gameObject.SetActive(true);
            SlowTime();
            setEndCanvas("Better luck next time");
        }
        else
        {
            Debug.Log("NPCWon!!");
            //player.GetComponent<ExplodingPlayer>().playerCamera.enabled = false;            
            SlowTime();
            setEndCanvas("Good Job");
            Destroy(player);
            endCamera.gameObject.SetActive(true);
        }
    }

    public void Kill()
    {        
        GameObject dead = Instantiate(EndPlayer);
        spawnDeadPrefab(target.transform, dead.transform);
        Destroy(target);
        Destroy(player);
    }

    private void spawnDeadPrefab(Transform player, Transform dead)
    {
        for (int i = 0; i < player.childCount; i++)
        {
            dead.transform.GetChild(i).gameObject.transform.position = player.GetChild(i).gameObject.transform.position;
            dead.transform.GetChild(i).gameObject.transform.rotation = player.GetChild(i).gameObject.transform.rotation;
            if (player.GetChild(i).childCount > 0)                
                spawnDeadPrefab(player.GetChild(i), dead.GetChild(i));
        }
    }

    private void setEndCanvas(string winner)
    {
        Cursor.visible = true;
        EndPannel.SetActive(true);
        EndText.text = winner;
    }

    public void SlowTime()
    {
        StartCoroutine(SlowDown());
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
