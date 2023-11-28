using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SADInfoMenu : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject infoMenu;

    [Header("Info Menu Button")]
    public Button mainMenuButton;

    public List<Button> returnButtons;

    // Start is called before the first frame update
    void Start()
    {
        EnableInfoMenu();

        //Hook events
        mainMenuButton.onClick.AddListener(GoToMainMenu);

        foreach (var item in returnButtons)
        {
            item.onClick.AddListener(EnableInfoMenu);
        }
    }

    public void GoToMainMenu()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    public void HideAll()
    {
        infoMenu.SetActive(false);
    }

    public void EnableInfoMenu()
    {
        infoMenu.SetActive(true);
    }
}
