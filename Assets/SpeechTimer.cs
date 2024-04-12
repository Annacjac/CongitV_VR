using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeechTimer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;
    public NPCInteract npcInteract;

    [Header ("Timer Settings")]
    public bool isOn = false;
    public float currentTime;
    public bool speechStarted = false;
    public bool speechDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isOn){
            currentTime += Time.deltaTime;
            timerText.text = "Speech Duration: " + currentTime.ToString();
        }
        else{
            currentTime = 0;
            timerText.text = "";
        }

    }

    public void ToggleTimer(){
        if(!speechDone && !isOn && npcInteract.playerInteractionDone && npcInteract.hrPresentationDone){
            isOn = true;
            speechStarted = true;
            npcInteract.interactText.text = "Say hello, tell everyone your name, job title, and what you do in your job. Try to talk for at least 90 seconds, and end the speech by thanking the audience.\nTake your time. Take slow, deep breaths in (count to 4) and out (count to 6). Focus on the message you want to convey rather than other people.";
        }
        else if(isOn && speechStarted){
            isOn = false;
            npcInteract.meetingStage = 2;
            npcInteract.interactionStage = 0;
            speechDone = true;
            npcInteract.interactText.text = "";
        }
    }
}
