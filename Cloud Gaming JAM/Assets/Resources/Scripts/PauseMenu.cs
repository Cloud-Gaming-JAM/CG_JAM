using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !gameObject.activeSelf)
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