using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuController : MonoBehaviour
{
    //Second element will be shown on initialization
    public enum MenuScreen { NULL, TitleScreen, Menu, Options, CharacterSelection, Credits }
    public MenuScreen currentState = MenuScreen.TitleScreen;
    Dictionary<MenuScreen, GameObject> menuLUT = new Dictionary<MenuScreen, GameObject>();

    void Start()
    {
        GatherMenuScreens();
        InitializeMenu();
    }
    void GatherMenuScreens()
    {
        foreach (MenuScreenTag screen in GetComponents<MenuScreenTag>())
        {
            if (screen.menuScreenType != 0)
            {
                menuLUT.Add(screen.menuScreenType, screen.gameObject);
            }

        }
    }
    void InitializeMenu()
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
    void ChangeMenuState(MenuScreen state)
    {
        foreach (KeyValuePair<MenuScreen, GameObject> screen in menuLUT)
        {
            screen.Value.SetActive(false);
        }
        currentState = state;
        menuLUT[state].SetActive(true);
    }

}
