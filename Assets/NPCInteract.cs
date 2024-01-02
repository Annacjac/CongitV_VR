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
    public bool npcInteractionStarted = false;
    bool nPCInteractionDone = false;
    public int interactionMode = 0; //0 is player initiates interaction, 1 is NPC initiates interaction.
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
        if(interactionMode == 0 && !playerInteractionDone){
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
            }
            //Debug.Log(interactText.text);
        }
        else if(interactionMode == 1 && !nPCInteractionDone && playerInteractionDone){
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
    
}
