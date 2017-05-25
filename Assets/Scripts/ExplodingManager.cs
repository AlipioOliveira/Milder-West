using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingManager : MonoBehaviour 
{
    public static ExplodingManager instancia;

    public List<GameObject> objects;       
    private GameObject npc;
    public GameObject player;
    private GameObject Explosive;
    
    private int roud = 0;
    private int Winner = 0;

    public GameObject ExplosionPrefab;
    public GameObject NpcSpawnPoint;
    public float slowness = 10f;
    public float slowDownTime = 3f;

    public Camera endCamera;

    private bool playerHasShot = false;

    public Object EndPlayer;

    public PlayerTrigger EndTrigger;
    public PlayerTrigger WeaponTrigger;

    private float originalfixedDeltaTime = 0;

    void Start () 
	{                
        originalfixedDeltaTime = Time.fixedDeltaTime;
        Explosive = objects[Random.Range(0, objects.Count)];
        Debug.Log(objects.IndexOf(Explosive));
        endCamera.gameObject.SetActive(false);
        instancia = this;
        GameObject newNpc = Instantiate(SceneTransitionManager.instancia.Minigame1Prefabs[SceneTransitionManager.instancia.getNpcIndex()], NpcSpawnPoint.transform.position, NpcSpawnPoint.transform.rotation);
        npc = newNpc;
        NextRound();
	}
	
	void Update () 
	{
        
    }

    public void NextRound()
    {       
        if (Winner == 0)
        {
            roud++;
            if (roud % 2 == 0) //even
            {
                CanvasManager.instancia.setTunrNameText("Its " + SceneTransitionManager.instancia.getNpcName() + " turn to Play.");
                CanvasManager.instancia.setTunrCheckpointText("");                
                player.GetComponent<fpsController>().FreezePlayer(true);
                int index = Random.Range(0, objects.Count);
                if (objects[index] == Explosive)                
                    npc.GetComponent<ExplodingNpc>().isTurn(true, objects[index]);                
                else                
                    npc.GetComponent<ExplodingNpc>().isTurn(false, objects[index]);               
            }
            if (roud % 2 == 1) //odd
            {
                CanvasManager.instancia.setTunrNameText("Its PLAYER turn to Play.");
                CanvasManager.instancia.setTunrCheckpointText("Its Player's time to shoot.");
                player.GetComponent<ExplodingPlayer>().IsTurn();
                player.GetComponent<fpsController>().FreezePlayer(false);
            }
            CanvasManager.instancia.setTunrNumberText("Tunr : " + roud);
        }        
    }

    public void HasWinner()
    {        
        if (roud % 2 == 0) //even        
        {
            Debug.Log("PlayerWon!!");
            Winner = 2;
            player.GetComponent<ExplodingPlayer>().playerCamera.enabled = false;
            Destroy(player.gameObject);
            Instantiate(EndPlayer, player.transform.position - new Vector3(0,0.9f,0), player.transform.rotation);
            endCamera.gameObject.SetActive(true);
            SlowTime();
            setEndCanvas("Congratulations you won!!!");
        }
        if (roud % 2 == 1) //odd
        {            
            Debug.Log("NPCWon!!");
            Winner = 1;
            player.GetComponent<ExplodingPlayer>().playerCamera.enabled = false;
            Destroy(player.gameObject);            
            endCamera.gameObject.SetActive(true);
            SlowTime();
            setEndCanvas("Better luck next time");
        }
    }

    private void setEndCanvas(string winner)
    {
        Cursor.visible = true;
        CanvasManager.instancia.ActivateEndCanvas();
        CanvasManager.instancia.setEndTextText(winner);
    }

    public GameObject getExplosive()
    {
        return Explosive;
    }

    public void SlowTime()
    {
        StartCoroutine(SlowDown());        
    }

    internal void UpdateTrigger()
    {
        if(playerHasShot && EndTrigger.getIsPlayerInside())
        {
            NextRound();
            playerHasShot = false;
            player.GetComponent<fpsController>().FreezePlayer(true);
        }
        else if(WeaponTrigger.getIsPlayerInside() && !playerHasShot)
        {
            ExplodingMinigameRevolver.instacia.setWeaponStatus(true);
        }
        else if (!WeaponTrigger.getIsPlayerInside())
        {
            ExplodingMinigameRevolver.instacia.setWeaponStatus(false);
        }
    }

    public void PlayerShoot()
    {
        playerHasShot = true;
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
}
