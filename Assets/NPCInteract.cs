using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Scripting.APIUpdating;
using System.Threading;
using Unity.VisualScripting;

public class NPCInteract : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI interactText;
    public NavMeshAgent navMeshAgent;
    public Meeting meeting;
    public Transform player;
    public Transform agent;
    Vector3 destination;
    Animator anim;


    [Header("Interaction Settings")]
    public bool playerInteractionDone = false;
    public int interactionStage = 0;
    public int meetingTextIndex = 0;
    public bool npcInteractionStarted = false;
    bool nPCInteractionDone = false;
    public int meetingStage = 0; //0 is player initiates interaction, 1 is NPC initiates interaction.
    public AnimateCharacter animate;

    
    private void Start(){
    
    }

    private void Update(){
        if(npcInteractionStarted && playerInteractionDone){
            NPCInitiates();
            npcInteractionStarted = false;
        }

        if (!navMeshAgent.pathPending){
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    Dialog();
                }
            }
        }

        if(meetingStage == 1 && playerInteractionDone){
            StartCoroutine(MeetingDialog(0)); //0
            StartCoroutine(MeetingDialog(5)); //1
            StartCoroutine(MeetingDialog(15)); //2
            StartCoroutine(MeetingDialog(22)); //3
            StartCoroutine(MeetingDialog(29)); //4
            StartCoroutine(MeetingDialog(36)); //5
            StartCoroutine(MeetingDialog(43)); //6
            StartCoroutine(MeetingDialog(50)); //7
            StartCoroutine(MeetingDialog(55)); //8
            StartCoroutine(MeetingDialog(60)); //9
            StartCoroutine(MeetingDialog(65));
        }

    }  

    public void PlayerInitiates(){
        interactionStage++;
        Dialog();
    }

    public void NPCInitiates(){
        destination = player.position;
        destination.z += 0.75f;
        WalkToPlayer(destination);
        interactionStage++;
    }

    public void Dialog(){
        if(meetingStage == 0 && !playerInteractionDone){
            if(interactionStage == 1){
                interactText.text = "Player is introducing themselves to NPC. Interact with NPC again for a response.";
            }
            else if(interactionStage == 2){
                interactText.text = "NPC: Nice to meet you. The meeting is about to start!";
            }
            else if(interactionStage == 3){
                interactText.text = "";
                interactionStage = 0;
                playerInteractionDone = true;
                meetingStage = 1;
            }
            //Debug.Log(interactText.text);
        }
        
        else if(meetingStage == 2 && !nPCInteractionDone && playerInteractionDone){
            if(interactionStage == 1){
                interactText.text = "NPC: Hey, what did you think about that new HR policy?";
            }
            else if(interactionStage == 2){
                interactText.text = "Player shares their opinion.";
            }
            else if(interactionStage == 3){
                interactText.text = "Hm, okay, interesting. I don't think I agree with that, but to each their own. See you later!";
            }
            else if(interactionStage == 4){
                interactText.text = "";
                interactionStage = 0;
                nPCInteractionDone = true;
                //Debug.Log("End Game");
                EndGame();

            }
            //Debug.Log(interactText.text);
        }

    }

    public void EndGame(){
        Debug.Log("End game");
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    public void WalkToPlayer(Vector3 destination){
        navMeshAgent.SetDestination(destination);
    }

    public IEnumerator Wait(){
        yield return new WaitForSeconds(10);
    }

    IEnumerator MeetingDialog(int seconds){
        yield return new WaitForSeconds(seconds);

        if(meetingTextIndex == 0){
            interactText.text = "";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 1){
            interactText.text = "Good morning, everyone, please find a seat and we'll get started.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 2){
            interactText.text = "Thank you for joining us today. If you don't know me, I am the head of HR. I wanted to briefly go over a new HR policy that was recently put into effect.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 3){
            interactText.text = "Recently we've noticed an increase in activities that waste company time, so effective today, we will begin monitoring work email activity and restricting internet use.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 4){
            interactText.text = "Now, I know that might sound like an invasion of privacy, but that isn't the goal, and I'm sure you can understand where we're coming from.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 5){
            interactText.text = "Our overall goal is to boost productivity and minimize distraction. We want to ensure that any non-work related emails only happen during breaks and outside of work hours.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 6){
            interactText.text = "Regarding internet usage, we will be limiting the use of certain websites that could distract from work, such as shopping sites, games, social media, etcetra.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 7){
            interactText.text = "I understand this might feel restrictive, but it's a move towards a more disciplined workspace. Your personal time and privacy outside work hours remain respected.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 8){
            interactText.text = "Thank you all for your time and cooperation. If there are any questions, I will be happy to answer them at the conclusion of this meeting.";
            meetingTextIndex++;
        }
        else if(meetingTextIndex == 9){
            interactText.text = "Now we will hear a special presentation from our new employee!";
            meetingTextIndex++;
        }
        else{
            interactText.text = "";
            meetingTextIndex = 0;
            meetingStage = 2;
        }

    }
    
}
