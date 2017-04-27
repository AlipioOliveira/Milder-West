using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
    public static DialogueManager instancia;

    private List<string> dialogueString;
    private string npcName;

    public GameObject dialoguePanel;

    private Button continueButton, stopButton;
    private Text dialogueText, nameText, shadowText;
    private int dIndex;
    private GameObject interacingWith;

    private float switchLine = 1f;
    public float timeToSwitchLine = 2f;
    [Range(0.5f, 45f)]
    public int LetterPerSecond = 20;
    [Range(1,10)]
    public int LettersAhead = 1;

    private float lineIndex;
    private float nextLineIndex = 1;
    private bool interacting = false;

    public bool checkLineLength = false;
    public int maxLetters = 20;

    private void Awake()
    {
        instancia = this;
    }

    private void Start()
    {
        continueButton = dialoguePanel.transform.FindChild("ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { AceptChalange(); });
        continueButton.enabled = false;
        continueButton.transform.localScale = Vector3.zero;
        stopButton = dialoguePanel.transform.FindChild("StopButton").GetComponent<Button>();
        stopButton.onClick.AddListener(delegate { StopInteraction(); });
        dialogueText = dialoguePanel.transform.FindChild("Text").GetComponent<Text>();
        shadowText = dialoguePanel.transform.FindChild("TextShadow").GetComponent<Text>();
        nameText = dialoguePanel.transform.FindChild("npcName").GetChild(0).GetComponent<Text>();        
        dialoguePanel.SetActive(false);

        dialogueString = new List<string>();
    }

    public void AddNewDialogue(string[] lines, string name, GameObject interactor)
    {        
        lineIndex = 0;
        dIndex = 0;
        switchLine = 1f;
        dialogueString = new List<string>();
        foreach (var item in lines)
        {            
            if (checkLineLength && item.Length >= maxLetters)
            {
                string[] line;
                line = item.Split(' ');
                string l1 = "", l2 = "";
                for (int i = 0; i < line.Length; i++)
                {
                    if (i < line.Length / 2)
                    {
                        l1 += line[i];
                        l1 += " ";
                    }
                    else
                    {
                        l2 += line[i];
                        l2 += " ";
                    }
                }
                dialogueString.Add(l1);
                dialogueString.Add(l2);
            }
            else dialogueString.Add(item);                                    
        }
        npcName = name;
        interacingWith = interactor;
        CreateDialogue();
    }    

    private void Update()
    {           
        if (interacting)
        {            
            lineIndex += LetterPerSecond * Time.deltaTime;         

            if (((int)lineIndex + 1 >= nextLineIndex) && (int)lineIndex < dialogueString[dIndex].Length)
            {               
                dialogueText.text += dialogueString[dIndex][(int)lineIndex];
                shadowText.text = dialogueText.text;

                for (int i = 0; i < LettersAhead; i++)
                {
                    if ((int)lineIndex + i < dialogueString[dIndex].Length - 1)
                    {
                        shadowText.text += dialogueString[dIndex][(int)lineIndex + 1 + i];
                    }
                }
                
                nextLineIndex++;                              
            }
            else if ((int)lineIndex + 1 > dialogueString[dIndex].Length)
            {                
                if (switchLine > 0)
                    switchLine -= (1f / timeToSwitchLine) * Time.deltaTime;
                else if (switchLine <= 0)
                {
                    switchLine = 1f;
                    ContinueDialogue();
                }
            }       
        }
    }

    public void BeguinInteraction()
    {
        interacting = true;
    }

    private void CreateDialogue()
    {
        dialogueText.text = ""/*dialogueString[dIndex]*/;
        shadowText.text = "";
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
    }

    private void ContinueDialogue()
    {
        switchLine = 1;
        lineIndex = 0;

        if (dIndex < dialogueString.Count - 1)
        {
            lineIndex = 0;
            dIndex++;
            dialogueText.text = ""/*dialogueString[dIndex]*/;
            shadowText.text = " ";
            nextLineIndex = 1;
        }
        else
        {
            continueButton.transform.localScale = Vector3.one;
            continueButton.enabled = true;
        }
    }

    private void AceptChalange()
    {
        
        StopInteraction();
        // MUDAR DE CENA     
    }

    private void StopInteraction()
    {
        interacingWith.GetComponent<npc>().StopRotating();
        PlayerControlls.instancia.StopIteraction();
        NPCManager.instancia.StopInteraction();
        Cursor.visible = false;
        interacting = false;
        lineIndex = 0;
        switchLine = 1;
        continueButton.enabled = false;
        continueButton.transform.localScale = Vector3.zero;
        nextLineIndex = 1;
        dialoguePanel.SetActive(false);
    }
}
