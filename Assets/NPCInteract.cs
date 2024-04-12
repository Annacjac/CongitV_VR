using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.Scripting.APIUpdating;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class NPCInteract : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI interactText;
    public SpeechTimer speechTimer;
    public NavMeshAgent navMeshAgent1; //Louise NPC
    public NavMeshAgent navMeshAgent2; //Joe NPC
    public NavMeshAgent navMeshAgent3; //Other NPCs
    public NavMeshAgent navMeshAgent4; //Other NPCs
    public NavMeshAgent navMeshAgent5; //Other NPCs
    public Animator Joe;  
    public Animator Kate; 
    public Animator Louise; 
    public Animator Leonard; 

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
    public bool hrPresentationStarted = false;
    public bool hrPresentationDone = false;
    public int interactionStage = 0;
    public int meetingTextIndex = 0;
    public bool npcInteractionStarted = false;
    bool nPCInteractionDone = false;
    bool chairReady = false;
    bool throughDoor = false;
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

        interactText.text = "Use the Left Joystick to navigate. Use the Right Trigger to interact. \nNavigate into the room (Left Thumbstick), interact with a character (Right Trigger), and introduce yourself to them.";

         Joe = navMeshAgent2.GetComponent<Animator>();
         Kate = navMeshAgent1.GetComponent<Animator>();
         Louise = navMeshAgent3.GetComponent<Animator>();
         Leonard = navMeshAgent4.GetComponent<Animator>();

        //Use the joystick on either controller to move towards the door and enter the meeting room.
    }

    private void Update(){
        
        //Detects when the player has walked through the door for the first time
        if(player.position.z > -3.4 && !throughDoor){
            throughDoor = true;
            interactText.text = "Take a slow, deep breath and know it's okay to feel nervous. Focus on maintaining eye contact and speaking clearly.\nNow, go say “Hi” and introduce yourself to someone...You've got this!";
        }
        
        //Detects whether it's time for the after-speech NPC to approach the player.
        if(speechTimer.speechDone && !npcInteractionStarted){
            StartCoroutine(NPCInitiates());
        }

        //Starts the actions of the after-speech NPC once the NPC stops walking.
        if (!navMeshAgent1.pathPending && speechTimer.speechDone){
            if (navMeshAgent1.remainingDistance <= navMeshAgent1.stoppingDistance)
            {
                if (!navMeshAgent1.hasPath || navMeshAgent1.velocity.sqrMagnitude == 0f)
                {
                    Kate.SetBool("isWalking",false); //Stops the walking animation
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
                    if(meetingStage == 1 && playerInteractionDone && !hrPresentationDone){
                        Joe.SetBool("isWalking", false); //Stops the walking animation
                        RotateToTarget(navMeshAgent2, podium.position);
                    }
                }
            }
        }

        if(meetingStage == 1 && playerInteractionDone && !hrPresentationStarted){
            StartCoroutine(MeetingDialog());
        }

        if(chairReady){
            sitInChairs(navMeshAgent1, chair2.position, chair5.position, Kate);
            //Stops Kate from moving after reaching their chair
            if (!navMeshAgent1.hasPath || navMeshAgent1.velocity.sqrMagnitude == 0f){
            Kate.SetBool("isWalking", false); 
            }
            sitInChairs(navMeshAgent3, chair5.position, chair2.position, Louise);
            //Stops Louise from moving after reaching their chair
            if (!navMeshAgent3.hasPath || navMeshAgent3.velocity.sqrMagnitude == 0f){
            Louise.SetBool("isWalking", false); 
            }
            sitInChairs(navMeshAgent4, chair4.position, chair1.position, Leonard);
            //Stops leonard from moving after reaching their chair
            if (!navMeshAgent4.hasPath || navMeshAgent4.velocity.sqrMagnitude == 0f){
            Leonard.SetBool("isWalking", false); 
            } 
        }

        if(hrPresentationDone){
            sitInChairs(navMeshAgent2, chair1.position, chair4.position, Joe);
        }

    }  

    //Player interacts with any NPC and starts a conversation
    public void PlayerInitiates(){
        interactionStage++;
        Dialog();
    }

    //NPC walks up to player after the player has given a speech
    public IEnumerator NPCInitiates(){
        Debug.Log("NPC initiates");
        npcInteractionStarted = true;
        interactText.text = "Interact audibly (Hold A Button and verbally respond) with the character who approaches you and asks for your opinion. Respond honestly to their questions. State your opinion respectfully. It's ok to disagree. \nTake a minute to appreciate yourself. Your thoughts and opinions are valid and matter.";
        yield return new WaitForSeconds(4);
        destination = player.position;
        destination.z += 0.75f;
        WalkToDestination(navMeshAgent1, destination,Kate);
        yield return new WaitForSeconds(3);
        Kate.SetBool("isWalking", false); //Stops the walking animation
        AudioManager.instance.Play(npcDisagreePart1);
        interactionStage++;
    }

    //Manages the dialog depending on what meeting stage the player is in
    //Stage 0: Player initiates conversation with an NPC
    //Stage 1: NPC gives presentation on new HR policy
    //Stage 2: NPC initiates with player after player has given the speech
    public void Dialog(){
        if(meetingStage == 0 && !playerInteractionDone){

            Debug.Log("Player Interaction");


            if(interactionStage == 1){
                interactText.text = "Interact (Right Trigger) with the character again for a response.";
            }
            else if(interactionStage == 2){
                interactText.text = "Colleague: Nice to meet you. The meeting is about to start!\nInteract (Right Trigger) again to continue.";
                AudioManager.instance.Play(npcIntroduction);
            }
            else if(interactionStage == 3){
                interactText.text = "Listen to the speech delivered by the presenter. Pay attention to the key points.";
                interactionStage = 0;
                playerInteractionDone = true;
                meetingStage = 1;
            }
            //Debug.Log(interactText.text);
        }
        

        else if(meetingStage == 2 && speechTimer.speechDone){
            //Debug.Log(interactionStage);
            RotateToTarget(navMeshAgent1, player.position);

            if(interactionStage == 1){
                interactText.text = "Colleague: Hey, what did you think about that new HR policy?";
            }
            else if(interactionStage == 2){
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
    public void WalkToDestination(NavMeshAgent nma, Vector3 destination, Animator anims){
        if(anims != null)
        {
            Debug.Log("Walking");
            anims.SetBool("isWalking", true);
        }
        nma.speed = (float)1.5;
        nma.SetDestination(destination);
        
    }

    //Rotates the agent towards a target
    public void RotateToTarget(NavMeshAgent agent, Vector3 target){
        Vector3 direction = (target - agent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, lookRotation, Time.deltaTime * 2);
    }


    //Makes NPCs walk to a chair
    public void sitInChairs(NavMeshAgent npc, Vector3 chair, Vector3 target, Animator anims){
        WalkToDestination(npc, chair, anims);
        RotateToTarget(npc, target);
    }

    //First moves the HR presenter to the podium, then starts his speech.
    //Text stays up for certain amount of time before the next text replaces it.
    IEnumerator MeetingDialog(){

        hrPresentationStarted = true;
        Vector3 cp = currentPosition.position;
        destination = podiumStandSpot.position;
        WalkToDestination(navMeshAgent2, destination, Joe);
        RotateToTarget(navMeshAgent2, podium.position);

        interactText.text = "Listen to the speech delivered by the presenter. Pay attention to the key points.";
        Debug.Log(interactText.text);

        yield return new WaitForSeconds(3);
        chairReady = true;

        yield return new WaitForSeconds(5);
        interactText.text = "Good morning, everyone, please find a seat and we'll get started.";

        AudioManager.instance.Play(hrSpeechPart1);
        Debug.Log(interactText.text);
        yield return new WaitForSeconds(4);
        chairReady = false;
        
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
        
        //Walks to a chair to watch player speech.
        WalkToDestination(navMeshAgent2, chair1.position, Joe);
        RotateToTarget(navMeshAgent2, chair4.position);
        if (!navMeshAgent2.hasPath || navMeshAgent2.velocity.sqrMagnitude == 0f){
            Joe.SetBool("isWalking", false); 
            } 

        interactText.text = "";
        meetingStage = 2;
        hrPresentationDone = true;


        interactText.text =  "Approach the podium to introduce yourself. Interact with the podium (Right Trigger) to start and stop your speech timer.";
       

    }
    
}

