using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject deathPanel;
    private PlayerMovement localPlayer;

    public TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (localPlayer == null)
        {
            var players = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);
            foreach (var p in players)
            {
                if (p.isLocalPlayer)
                {
                    localPlayer = p;
                    break;
                }
            }
        }

        if (localPlayer != null && !localPlayer.isDead)
        {
            scoreText.text = $"Size: {localPlayer.size:F1}";
            // dashText.text = ... (for dash feature)
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        PlayerMovement[] players =
            FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None);

        foreach (PlayerMovement player in players)
        {
            if (player.isLocalPlayer)
            {
                scoreText.text = "Size: " + player.size.ToString("F1");
                return;
            }
        }
    }

    public void ShowDeathScreen()
    {
        deathPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}