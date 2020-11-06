using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

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
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            if (text.gameObject.name == "WinnerText")
            {
                text.text = "TEAM " + teamWhoWon + " HAS WON";
            }
        }
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(2);
    }

}