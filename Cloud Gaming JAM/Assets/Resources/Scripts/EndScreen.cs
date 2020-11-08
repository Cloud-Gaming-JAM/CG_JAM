using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public static EndScreen instance;
    public GameObject team1Sprites;
    public GameObject team2Sprites;
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void EnableEndScreen(int teamWhoWon)
    {
        gameObject.SetActive(true);
        foreach (TextMeshProUGUI text in GetComponentsInChildren<TextMeshProUGUI>())
        {
            if (text.gameObject.tag == "WinnerText")
            {
                text.text = "TEAM " + teamWhoWon + " HAS WON";
            }
        }
        if (teamWhoWon == 1)
        {
            team1Sprites.SetActive(true);
        }
        else
        {
            team2Sprites.SetActive(true);
        }

    }
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}