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

    [Header("Audio")]
    public string hrSpeechPart1;
    public string hrSpeechPart2;
    public string hrSpeechPart3;
    public string hrSpeechPart4;
    public string hrSpeechPart5;
    public string hrSpeechPart6;
    public string hrSpeechPart7;
    public string hrSpeechPart8;
    public string hrSpeechPart9;

    
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
            StartCoroutine(MeetingDialog());
            meetingStage = -1; 
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

    IEnumerator MeetingDialog(){

        interactText.text = "";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(5);

        interactText.text = "Good morning, everyone, please find a seat and we'll get started.";
        sitInChairs();
        AudioManager.instance.Play(hrSpeechPart1);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);
        
        interactText.text = "Thank you for joining us today. If you don't know me, I am the head of HR. I wanted to briefly go over a new HR policy that was recently put into effect.";
        AudioManager.instance.Play(hrSpeechPart2);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);

        interactText.text = "Recently we've noticed an increase in activities that waste company time, so effective today, we will begin monitoring work email activity and restricting internet use.";
        AudioManager.instance.Play(hrSpeechPart3);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);    

        interactText.text = "Now, I know that might sound like an invasion of privacy, but that isn't the goal, and I'm sure you can understand where we're coming from.";
        AudioManager.instance.Play(hrSpeechPart4);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(8);    
        
        interactText.text = "Our overall goal is to boost productivity and minimize distraction. We want to ensure that any non-work related emails only happen during breaks and outside of work hours.";
        AudioManager.instance.Play(hrSpeechPart5);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);   
        
        interactText.text = "Regarding internet usage, we will be limiting the use of certain websites that could distract from work, such as shopping sites, games, social media, etcetra.";
        AudioManager.instance.Play(hrSpeechPart6);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);
        
        interactText.text = "I understand this might feel restrictive, but it's a move towards a more disciplined workspace. Your personal time and privacy outside work hours remain respected.";
        AudioManager.instance.Play(hrSpeechPart7);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);
        
        interactText.text = "Thank you all for your time and cooperation. If there are any questions, I will be happy to answer them at the conclusion of this meeting.";
        AudioManager.instance.Play(hrSpeechPart8);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(8);
        
        interactText.text = "Now we will hear a special presentation from our new employee!";
        AudioManager.instance.Play(hrSpeechPart9);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(5);
        
        interactText.text = "";
        meetingStage = 2;

        //Walks to a chair to watch player speech.
        WalkToDestination(navMeshAgent2, chair1.position);
    }
    
}
