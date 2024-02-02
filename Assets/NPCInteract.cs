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
    public Transform player;
    public Transform agent;
    public Transform podiumStandSpot;
    public Transform currentPosition;
    public Transform podium;
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

    public void PlayerInitiates(){
        interactionStage++;
        Dialog();
    }

    public void NPCInitiates(){
        destination = player.position;
        destination.z += 0.75f;
        WalkToDestination(navMeshAgent1, destination);
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

    public void WalkToDestination(NavMeshAgent nma, Vector3 destination){
        nma.SetDestination(destination);
        //RotateToTarget(target);
    }

    public void RotateToTarget(Vector3 target){
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    IEnumerator MeetingDialog(){

        Vector3 cp = currentPosition.position;
        destination = podiumStandSpot.position;
        WalkToDestination(navMeshAgent2, destination);

        interactText.text = "";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);

        interactText.text = "Good morning, everyone, please find a seat and we'll get started.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(10);
        
        interactText.text = "Thank you for joining us today. If you don't know me, I am the head of HR. I wanted to briefly go over a new HR policy that was recently put into effect.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);

        interactText.text = "Recently we've noticed an increase in activities that waste company time, so effective today, we will begin monitoring work email activity and restricting internet use.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);    
     
        interactText.text = "Now, I know that might sound like an invasion of privacy, but that isn't the goal, and I'm sure you can understand where we're coming from.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);    
        
        interactText.text = "Our overall goal is to boost productivity and minimize distraction. We want to ensure that any non-work related emails only happen during breaks and outside of work hours.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);   
        
        interactText.text = "Regarding internet usage, we will be limiting the use of certain websites that could distract from work, such as shopping sites, games, social media, etcetra.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);
        
        interactText.text = "I understand this might feel restrictive, but it's a move towards a more disciplined workspace. Your personal time and privacy outside work hours remain respected.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(7);
        
        interactText.text = "Thank you all for your time and cooperation. If there are any questions, I will be happy to answer them at the conclusion of this meeting.";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(5);
        
        interactText.text = "Now we will hear a special presentation from our new employee!";
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(5);
        
        interactText.text = "";
        meetingStage = 2;

        WalkToDestination(navMeshAgent2, cp);
    }
    
}
