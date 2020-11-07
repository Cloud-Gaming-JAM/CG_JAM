using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    //Second element will be shown on initialization
    public enum MenuScreen { NULL, Menu, Options, CharacterSelection, Credits }
    public MenuScreen currentState = (MenuScreen)1;
    public Dictionary<MenuScreen, GameObject> menuLUT = new Dictionary<MenuScreen, GameObject>();

    public GameObject[] imagePlayerJoined;
    public Sprite[] charNotSelected;
    public Sprite[] charSelected;

    public bool isInScreenPlayerSelection = false;
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
        if (state == MenuScreen.CharacterSelection)
        {
            isInScreenPlayerSelection = true;
        }
        else
        {
            isInScreenPlayerSelection = false;
        }
    }
    public void ChangeMenuState(int state)
    {
        foreach (KeyValuePair<MenuScreen, GameObject> screen in menuLUT)
        {
            screen.Value.SetActive(false);
        }
        currentState = (MenuScreen)state;
        menuLUT[(MenuScreen)state].SetActive(true);
        if (state == 3)
        {
            isInScreenPlayerSelection = true;
        }
        else
        {
            isInScreenPlayerSelection = false;
        }
    }

    public void SetActiveJoinPlayerImage(int id)
    {
        imagePlayerJoined[id].GetComponent<Image>().color = Color.white;
        imagePlayerJoined[id].GetComponent<Image>().sprite = charSelected[id];
    }
    
    public void SetActiveLeavePlayerImage(int id)
    {
        imagePlayerJoined[id - 1].GetComponent<Image>().color = new Color(1,1,1,0.3f);
        imagePlayerJoined[id].GetComponent<Image>().sprite = charNotSelected[id];
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LaunchGame()
    {
        isInScreenPlayerSelection = false;
        GameManager.instance.gameState = GameState.inGame;
        SceneManager.LoadScene(1);
    }
}
