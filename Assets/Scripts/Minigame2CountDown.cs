using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame2CountDown : MonoBehaviour 
{

    public Text text;
    public Image Panel;

    private int number = 3;

    Vector3 textScale;
    private float t = 0;
    private float originalAlpha1 = 0f;
    private float originalAlpha2 = 0f;

    private bool c = true;

	void Start () 
	{
        textScale = Vector3.zero;
        text.text = "" + number;
        originalAlpha1 = Panel.color.a;
        originalAlpha2 = text.color.a;
    }
	
	void Update () 
	{
        if (c)
        {           
            if (t >= 1)
            {                
                nextNumber();
            }
            else
            {
                if (number < 1)
                {
                    t += 1 * Time.deltaTime;
                    float alpha1 = Mathf.Lerp(originalAlpha1, 0, t);
                    float alpha2 = Mathf.Lerp(originalAlpha2, 0.0f, t);

                    Panel.color = new Color(Panel.color.r, Panel.color.g, Panel.color.b, alpha1);                 
                    text.color = new Color(text.color.r, text.color.g, text.color.b, alpha2);
                }
                else
                {
                    t += 1 * Time.deltaTime;
                    textScale.x = Mathf.Lerp(0, 1, t);
                    textScale.y = Mathf.Lerp(0, 1, t);
                }                            
            }
            text.transform.localScale = textScale;
        }       
	}

    private void nextNumber()
    {
        t = 0;
        number--;
        if (number < 1)
        {
            text.text = "GO";          
            StartCoroutine(Wait(1f));            
        }
        else
        {
            textScale = Vector3.zero;
            text.text = "" + number;
        }        
    }
    IEnumerator Wait(float time)
    {        
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
        c = false;
    }
}
