using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Meeting : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI meetingText;
    //public NavMeshAgent[] navMeshAgents;
    public NPCInteract npcInteract;
    //public Transform agent;
    Vector3 destination;
    Vector3 rotation;

    [Header("Meeting Settings")]
    public bool meetingDone = false;
    int meetingTextIndex = 0;

    //public Meeting(){

    //}

    // Start is called before the first frame update
    void Start()
    {
        HaveMeeting();
        Debug.Log("Meeting");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(npcInteract.playerInteractionDone && !npcInteract.npcInteractionStarted){
            
        }*/
    }

    void DoDialog(){
        StartCoroutine(Dialog(0)); //0
        StartCoroutine(Dialog(5)); //1
        StartCoroutine(Dialog(15)); //2
        StartCoroutine(Dialog(22)); //3
        StartCoroutine(Dialog(29)); //4
        StartCoroutine(Dialog(36)); //5
        StartCoroutine(Dialog(43)); //6
        StartCoroutine(Dialog(50)); //7
        StartCoroutine(Dialog(55)); //8
        StartCoroutine(Dialog(60)); //9
        StartCoroutine(Dialog(65));
    }

    /*public void WalkToDestination(Vector3 destination, Vector3 rotation){
        navMeshAgents[0].SetDestination(destination);
    }*/

    IEnumerator Dialog(int seconds){
        yield return new WaitForSeconds(seconds);
        if(meetingTextIndex == 0){
            meetingText.text = "";
        }
        else if(meetingTextIndex == 1){
            meetingText.text = "Good morning, everyone, please find a seat and we'll get started.";
        }
        else if(meetingTextIndex == 2){
            meetingText.text = "Thank you for joining us today. If you don't know me, I am the head of HR. I wanted to briefly go over a new HR policy that was recently put into effect.";
        }
        else if(meetingTextIndex == 3){
            meetingText.text = "Recently we've noticed an increase in activities that waste company time, so effective today, we will begin monitoring work email activity and restricting internet use.";
        }
        else if(meetingTextIndex == 4){
            meetingText.text = "Now, I know that might sound like an invasion of privacy, but that isn't the goal, and I'm sure you can understand where we're coming from.";
        }
        else if(meetingTextIndex == 5){
            meetingText.text = "Our overall goal is to boost productivity and minimize distraction. We want to ensure that any non-work related emails only happen during breaks and outside of work hours.";
        }
        else if(meetingTextIndex == 6){
            meetingText.text = "Regarding internet usage, we will be limiting the use of certain websites that could distract from work, such as shopping sites, games, social media, etcetra.";
        }
        else if(meetingTextIndex == 7){
            meetingText.text = "I understand this might feel restrictive, but it's a move towards a more disciplined workspace. Your personal time and privacy outside work hours remain respected.";
        }
        else if(meetingTextIndex == 8){
            meetingText.text = "Thank you all for your time and cooperation. If there are any questions, I will be happy to answer them at the conclusion of this meeting.";
        }
        else if(meetingTextIndex == 9){
            meetingText.text = "Now we will hear a special presentation from our new employee!";
        }
        else{
            meetingText.text = "";
        }

        meetingTextIndex++;

    }

    void HaveMeeting(){
        DoDialog();
    }
}
