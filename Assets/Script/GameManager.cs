using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("UI References")]
    [SerializeField]
    private GameObject playerEliminatedPanel;
    public TMP_Text playerSizeText;
    private PlayerMovement localPlayerReference;
    [SerializeField]
    private GameObject winnerPanel;
    [SerializeField]
    private TMP_Text winnerNameText;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FindLocalPlayerIfNeeded();

        UpdatePlayerSizeDisplay();

        //testing the UI 
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowWinnerScreen("Player999");
        }
    }

    // =========================
    // PLAYER LOOKUP
    // =========================

    private void FindLocalPlayerIfNeeded()
    {
        if (localPlayerReference != null)
            return;

        foreach (PlayerMovement player in PlayerMovement.allPlayers)
        {
            if (player != null && player.isLocalPlayer)
            {
                localPlayerReference = player;
                return;
            }
        }
    }

    // =========================
    // UI UPDATES
    // =========================
    private void UpdatePlayerSizeDisplay()
    {
        if (localPlayerReference == null)
            return;

        if (localPlayerReference.currentMatchState != MatchState.Playing)
            return;

        playerSizeText.text =
            $"Size: {localPlayerReference.currentSize:F1}";
    }

    // =========================
    // DEATH SCREEN
    // =========================
    public void ShowPlayerEliminatedScreen()
    {
        playerEliminatedPanel.SetActive(true);
    }

    public void HidePlayerEliminatedScreen()
    {
        playerEliminatedPanel.SetActive(false);
    }
    public void ShowWinnerScreen(string winnerName)
    {
        winnerPanel.SetActive(true);

        winnerNameText.text =
            $"Winner\n{winnerName}";
    }

    // =========================
    // SCENE MANAGEMENT
    // =========================
    public void RestartCurrentGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
    public void StartSpectating()
    {
        HidePlayerEliminatedScreen();
        SpectatorManager.Instance
            .FindBestPlayerToSpectate();
        Camera.main
            .GetComponent<CameraFollow>()
            .EnableSpectateMode();
        localPlayerReference.EnterSpectatorMode();
    }
}