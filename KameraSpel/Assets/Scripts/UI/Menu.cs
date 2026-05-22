using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    bool isPaused;
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1.0f;
    }
    public void ExitGame()
    {
       Application.Quit();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (isPaused)
            {
                //Sätt igång spelet igen
                UnPauseGame();
            }
            else
            {
                isPaused = true;
                pauseScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            }
        }
      
       
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
