using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    void Init()
    {
        instance = this;
    }
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {


    }

    public void DisplayPauseMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
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