using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour 
{
    private Vector2 mouselook;

    public float sensitivity = 2.8f;

    private GameObject player;

    private float nextRandom = 0;

    private float xSpin = 0;
    private float ySpin = 0;

    private float xSpinD = 0;
    private float ySpinD = 0;

    private float t = 0;

    private bool cursorState = false;

    public float minRangeY = -60;
    public float maxRangeY = 60;
    public float minRangeX = -60;
    public float maxRangeX = 60;

    void Start()
    {
        player = this.transform.parent.gameObject;
        UnityEngine.Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        nextRandom = 0;
        StartCoroutine(WaitToStart(3.5f));
    }

    void Update()
    {
        if (cursorState)
        {            
            Vector2 mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * Time.deltaTime * sensitivity;

            mouselook += mouseChange;
            mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);

            if (Time.time >= nextRandom)
                randomizeRotation();

            t += (1 / nextRandom) * Time.deltaTime;

            xSpinD = Mathf.Lerp(xSpinD, xSpin, t);
            ySpinD = Mathf.Lerp(ySpinD, ySpin, t);

            if (transform.localRotation.y < -90 || transform.localRotation.y > 90)
            {
                transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
                player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);
            }
            else
            {
                transform.localRotation = Quaternion.AngleAxis(-mouselook.y + ySpinD, Vector3.right);
                player.transform.localRotation = Quaternion.AngleAxis(mouselook.x + xSpinD, player.transform.up);
            }            
        }        
    }

    public void setCursorState(bool state)
    {
        cursorState = state;
    }

    private void randomizeRotation()
    {
        xSpin = Random.Range(minRangeX, maxRangeX);
        ySpin = Random.Range(minRangeY, maxRangeY);
        t = 0.0f;
        nextRandom = Time.time + Random.Range(0.5f, 1.0f);
    }

    IEnumerator WaitToStart(float time)
    {
        yield return new WaitForSeconds(time);
        cursorState = true;
        randomizeRotation();
    }
}
