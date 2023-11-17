using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI interactText;
    public NavMeshAgent navMeshAgent;

    [Header("Interaction Settings")]
    public bool playerInteractionDone = false;
    public int interactionStage = 0;
    public bool nPCInteractionDone = false;
    public float rotateTime = 2; 
    public float walkSpeed = 4;
    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    //0 is player initiates interaction, 1 is NPC initiates interaction.
    public int interactionMode = 0; 
    
    private void Start(){
        m_PlayerPosition = Vector3.zero;

        //NavMeshAgent.isStopped = true;
        //NavMeshAgent.speed = 0;
    }

    private void Update(){
    }  

    public void PlayerInitiates(){
        interactionStage++;
        Dialog();
    }

    public void NPCInitiates(){
        //WalkToPlayer();
        //Stop();
        interactionStage++;
        Dialog();
    }

    public void Dialog(){
        if(interactionMode == 0){
            if(interactionStage == 1)
                interactText.text = "Player is introducing themselves to NPC. Interact with NPC again for a response.";
            else if(interactionStage == 2)
                interactText.text = "NPC: Nice to meet you. The meeting is about to start!";
            else{
                interactText.text = "";
                interactionStage = 0;
                playerInteractionDone = true;
            }
            Debug.Log(interactText.text);
        }
        else{
            if(interactionStage == 1)
                interactText.text = "NPC: Hey, what did you think about that new HR policy?";
            else if(interactionStage == 2)
                interactText.text = "Player shares their opinion.";
            else{
                interactText.text = "Hm, okay, interesting. I don't think I agree with that, but to each their own. See you later!";
                interactionStage = 0;
            }
            Debug.Log(interactText.text);
        }
    }

    public void ChooseInteractMode(){
        if(interactionMode == 0){
            PlayerInitiates();
        }
        else{
            NPCInitiates();
        }
    }

    /*void Move(float speed){
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop(){
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    void WalkToPlayer(){
        playerLastPosition = Vector3.zero;
        Move(walkSpeed);
        navMeshAgent.destination = m_PlayerPosition;
    }*/
}
