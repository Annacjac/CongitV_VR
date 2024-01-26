using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;

public class PauseMenuScript : MonoBehaviour
{
    //reference to pause menu
    public GameObject pauseMenu;

    public Button resumeButton; //Clickable GUI for Pause Menu's resume button

    public Button exitButton; //Clickable GUI for Pause Menu's exit button

    //determines whether pause menu is active or not.
    //default value is false since player won't load into game with pause menu already activated
    private bool pauseMenuActive = false;

    //private bool pausePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        //pauseMenu.SetActive(true);

        //resumeButton.onClick.AddListener(resumeButton);
        //exitButton.onClick.AddListener(exitButton);
    }

    // Update is called once per frame
    void Update()
    {
        //initializing leftController
        UnityEngine.XR.InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        //Used to collect info on whether menu button on left controller is pressed during every update.
        //Also used to check if pausePressed variable is true, indicating that the menu button was pressed.
        if (leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out bool pausePressed) && pausePressed) PauseOrResumeMenuButton();
    }

    /* 
        Function that either activates the pause menu (pause game and display Pause Menu UI), 
        or deactivates pause menu (unpause game and hide Pause Menu UI).
        Function activates pause menu if pauseMenuActive value is "false" before running this method,
        and deactivates pause menu if pauseMenuActive value is "true" before running this method. 
    */
    // Method only for if you use menu button on the controller to resume the game (as opposed to the "Resume" GUI button)
    public void PauseOrResumeMenuButton()
    {

        //if pause menu is currently active
        if (pauseMenuActive)
        {
            pauseMenu.SetActive(false); //disables the "PauseMenu" game object
            pauseMenuActive = false; //changes value to reflect new current status of pause menu

            //setting this to 1 allows the time in game to pass as fast as real time (basically unpauses the game).
            Time.timeScale = 1;
        }

        //if pause menu is currently NOT active
        else if (!pauseMenuActive)
        {
            pauseMenu.SetActive(true); //enables the "PauseMenu" game object
            pauseMenuActive = true; //changes value to reflect new current status of pause menu

            //setting this to 0 pauses the game (assuming all functions are frame rate independent).
            Time.timeScale = 0;
        }
    }

    /* For when you use the ray on the right VR controller to press the "Resume" GUI button on the pause menu*/
    public void ResumeButton()
    {

    }
    /*Switches scene to the Main Menu scene when the game is paused and the player points their right VR controller's ray
      at the "Exit" menu button and clicks it.*/
    public void ExitButton()
    {

    }
}
