using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        GameManager.instance.SetGameState(GameState.pause);
        Time.fixedDeltaTime = 0f;
    }

    void OnDisable()
    {
        GameManager.instance.SetGameState(GameState.inGame);
        Time.fixedDeltaTime = 0.02f;
    }

    void Update()
    {


    }

    public void DisplayPauseMenu()
    {
        Debug.Log("AttemptToDisplayPauseMenu");
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
        SceneManager.LoadScene(0);
    }

}