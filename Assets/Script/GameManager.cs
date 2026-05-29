using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager instance;

    public GameObject deathPanel;

    private void Awake()
    {
        instance = this;
    }

    // show death screen
    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
    }

    // restart game
    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // quit game
    public void QuitGame()
    {
        Application.Quit();
    }
}