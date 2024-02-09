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
    public NavMeshAgent navMeshAgent1; //NPC that approaches player after speech
    public NavMeshAgent navMeshAgent2; //NPC that gives HR policy presentation
    public NavMeshAgent navMeshAgent3; //Other NPCs
    public NavMeshAgent navMeshAgent4; //Other NPCs
    public NavMeshAgent navMeshAgent5; //Other NPCs
    public Transform player;
    public Transform agent;
    public Transform podiumStandSpot;
    public Transform currentPosition;
    public Transform podium;
    public Transform chair1;
    public Transform chair2;
    public Transform chair3;
    public Transform chair4;
    public Transform chair5;
    public Transform chair6;
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
    public string npcIntroduction;
    public string npcDisagreePart1;
    public string npcDisagreePart2;
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
        //Detects whether it's time for the after-speech NPC to approach the player.
        if(npcInteractionStarted && playerInteractionDone){
            NPCInitiates();
            npcInteractionStarted = false;
        }

        //Starts the actions of the after-speech NPC once the NPC stops walking.
        if (!navMeshAgent1.pathPending){
            if (navMeshAgent1.remainingDistance <= navMeshAgent1.stoppingDistance)
            {
                if (!navMeshAgent1.hasPath || navMeshAgent1.velocity.sqrMagnitude == 0f)
                {
                    RotateToTarget(player.position);
                    Dialog();
                }
            }
        }

        //Starts the actions of the HR Policy presenter after he stops walking.
        if (!navMeshAgent2.pathPending){
            if (navMeshAgent2.remainingDistance <= navMeshAgent2.stoppingDistance)
            {
                if (!navMeshAgent2.hasPath || navMeshAgent2.velocity.sqrMagnitude == 0f)
                {
                    if(meetingStage == 1 && playerInteractionDone){
                        RotateToTarget(podium.position);
                    }
                    else if(meetingStage == 2){
                        RotateToTarget(player.position);
                    }
                }
            }
        }

        if(meetingStage == 1 && playerInteractionDone){
            StartCoroutine(MeetingDialog());
            meetingStage = -1; 
        }

    }  

    //Player interacts with any NPC and starts a conversation
    public void PlayerInitiates(){
        interactionStage++;
        Dialog();
    }

    //NPC walks up to player after the player has given a speech
    public void NPCInitiates(){
        destination = player.position;
        destination.z += 0.75f;
        WalkToDestination(navMeshAgent1, destination);
        interactionStage++;
    }

    //Manages the dialog depending on what meeting stage the player is in
    //Stage 0: Player initiates conversation with an NPC
    //Stage 1: NPC gives presentation on new HR policy
    //Stage 2: NPC initiates with player after player has given the speech
    public void Dialog(){
        if(meetingStage == 0 && !playerInteractionDone){
            if(interactionStage == 1){
                interactText.text = "Player is introducing themselves to NPC. Interact with NPC again for a response.";
            }
            else if(interactionStage == 2){
                interactText.text = "NPC: Nice to meet you. The meeting is about to start!";
                AudioManager.instance.Play(npcIntroduction);
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
                AudioManager.instance.Play(npcDisagreePart1);
            }
            else if(interactionStage == 2){
                interactText.text = "Player shares their opinion.";
            }
            else if(interactionStage == 3){
                interactText.text = "Hm, okay, interesting. I don't think I agree with that, but to each their own. See you later!";
                AudioManager.instance.Play(npcDisagreePart2);
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

    //Changes the scene to the end screen
    public void EndGame(){
        Debug.Log("End game");
        SceneTransitionManager.singleton.GoToSceneAsync(3);
    }

    //Takes a navMeshAgent and a position and sets the Agent's destination to that position,
    //which triggers the Agent to begin moving towards that destination.
    public void WalkToDestination(NavMeshAgent nma, Vector3 destination){
        nma.SetDestination(destination);
    }

    //Should be rotating the NavMeshAgent towards a target, but this is currently not working
    public void RotateToTarget(Vector3 target){
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    //Makes NPCs walk to a chair
    public void sitInChairs(){
        WalkToDestination(navMeshAgent1, chair2.position);
        WalkToDestination(navMeshAgent3, chair4.position);
        WalkToDestination(navMeshAgent4, chair5.position);
    }

    //First moves the HR presenter to the podium, then starts his speech.
    //Text stays up for certain amount of time before the next text replaces it.
    IEnumerator MeetingDialog(){

        Vector3 cp = currentPosition.position;
        destination = podiumStandSpot.position;
        WalkToDestination(navMeshAgent2, destination);

        interactText.text = "";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(3);

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
