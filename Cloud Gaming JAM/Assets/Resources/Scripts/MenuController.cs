using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    private static MenuController instance;
    //Second element will be shown on initialization
    public enum MenuScreen { NULL, TitleScreen, Menu, Options, CharacterSelection, Credits }
    public MenuScreen currentState = MenuScreen.TitleScreen;
    public Dictionary<MenuScreen, GameObject> menuLUT = new Dictionary<MenuScreen, GameObject>();

    public GameObject[] imagePlayerJoined;
    void Start()
    {
        instance = this;
        GatherMenuScreens();
        InitializeMenu();
    }
    public void GatherMenuScreens()
    {
        foreach (MenuScreenTag screen in GetComponentsInChildren<MenuScreenTag>())
        {
            if (screen.menuScreenType != MenuScreen.NULL)
            {
                menuLUT.Add(screen.menuScreenType, screen.gameObject);
                Debug.Log(screen.menuScreenType + "has" + screen.gameObject);
            }

        }
    }
    public void InitializeMenu()
    {
        //Hide all menu to make sure multiple screens dont overlap
        foreach (KeyValuePair<MenuScreen, GameObject> screen in menuLUT)
        {
            screen.Value.SetActive(false);
        }
        //Display first menu screen category
        currentState = (MenuScreen)1;
        menuLUT[(MenuScreen)1].SetActive(true);
    }
    public void ChangeMenuState(MenuScreen state)
    {
        foreach (KeyValuePair<MenuScreen, GameObject> screen in menuLUT)
        {
            screen.Value.SetActive(false);
        }
        currentState = state;
        menuLUT[state].SetActive(true);
    }
    public void ChangeMenuState(int state)
    {
        foreach (KeyValuePair<MenuScreen, GameObject> screen in menuLUT)
        {
            screen.Value.SetActive(false);
        }
        currentState = (MenuScreen)state;
        menuLUT[(MenuScreen)state].SetActive(true);
    }

    public void SetActiveJoinPlayerImage(bool active, int id)
    {
        imagePlayerJoined[id].SetActive(active);
        Debug.Log("Set " + id + "to " + active);        
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }
}
