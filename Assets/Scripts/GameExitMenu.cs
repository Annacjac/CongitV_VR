using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExitMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject exitMenu;

    [Header("Main Menu Buttons")]
    public Button mainMenuButton;
    public Button quitButton;

    public List<Button> returnButtons;


    // Start is called before the first frame update
    void Start()
    {
        EnableExitMenu();

        //Hook events
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableExitMenu);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        exitMenu.SetActive(false);
    }

    public void EnableExitMenu()
    {
        exitMenu.SetActive(true);
    }
}
